import React, { useEffect } from "react";
import { TextField, Button, Box, Typography, FormControlLabel, RadioGroup, Radio, FormLabel } from "@mui/material";
import { useMutation, useQuery } from "@tanstack/react-query";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import { Link, useNavigate, useParams } from "@tanstack/react-router";
import { useForm, Controller } from "react-hook-form";

import ReusableTextbox from "../FormControls/ReusableTextbox";
import { showNotification } from "../../store/notificationSlice";
import { useDispatch } from "react-redux";

// Define the mutation function
const insertEmployeeToDb = async (newEmployee) => {
  const response = await fetch("http://localhost:9000/api/employee", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newEmployee),
  });

  if (!response.ok) throw new Error("Failed to create Employee");
  return response.json();
};

const updateEmployeeToDb = async ({ id, data }) => {
  const response = await fetch(`http://localhost:9000/api/employee/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });

  if (!response.ok) throw new Error("Failed to update Employee");
  if (response.status === 204) return null; // No content to return
  return response.json();
};

const fetchEmployee = async ({ queryKey }) => {
  const [, employeeId] = queryKey;
  const response = await fetch(`http://localhost:9000/api/Employee/${employeeId}`);
  if (!response.ok) throw new Error("Failed to fetch Employee");
  return response.json();
};

const fetchCafes = async () => {
  const response = await fetch("http://localhost:9000/api/cafes");
  if (!response.ok) throw new Error("Failed to fetch Cafes");
  return response.json();
};

const AddEditEmployee = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { employeeId } = useParams({ strict: false });
  const isEdit = !!employeeId;

  // If in edit mode, fetch the cafe's information and prefill the form
  const { data: cafeData } = useQuery({ queryKey: ["fetchCafes"], queryFn: fetchCafes });
  const { data: employeeItem } = useQuery({ queryKey: ["employee", employeeId], queryFn: fetchEmployee, enabled: isEdit && !!cafeData });

  const { control, handleSubmit, reset } = useForm({
    defaultValues: {
      name: "", // Ensure name is controlled
      emailAddress: "", // Ensure email is controlled
      phoneNumber: "", // Ensure phone number is controlled
      cafeId: "", // Ensure cafeName is controlled
      gender: "0", // Ensure gender is controlled
    },
  });

  // Set the default values once data is fetched
  useEffect(() => {
    debugger;
    if (!isEdit && cafeData) {
      const updatedFormValues = { cafeId: cafeData[0]["id"], name: "", emailAddress: "", phoneNumber: "", gender: "0" };
      reset(updatedFormValues);
    } else if (employeeItem) {
      const updatedFormValues = { cafeId: cafeData.filter((x) => x.name === employeeItem.cafeName)[0]["id"], ...employeeItem };
      reset(updatedFormValues);
    } // Populate the form with the fetched data
  }, [employeeItem, cafeData, reset]);

  // Mutation to submit form data
  const { isLoading, mutate: insertEmployeeMutateFn } = useMutation({
    mutationFn: insertEmployeeToDb,
    onSuccess: () => {
      reset();
      dispatch(showNotification({ message: "Employee registered successfully!", severity: "success", duration: 3000 }));
      navigate({ to: "/employee" });
    },
    onError: (error) => {
      console.log(error);
      dispatch(showNotification({ message: "Error registering employee.", severity: "error", duration: 3000 }));
    },
  });

  const { mutate: updateEmployeeMutateFn } = useMutation({
    mutationFn: updateEmployeeToDb,
    mutationKey: "updateEmployeeToDb",
    onSuccess: () => {
      reset();
      dispatch(showNotification({ message: "Employee updated successfully!", severity: "success", duration: 3000 }));
      navigate({ to: "/employee" });
    },
    onError: (error) => {
      console.log(error);
      dispatch(showNotification({ message: "Error updating Employee.", severity: "error", duration: 3000 }));
    },
  });

  const onSubmit = (data) => {
    debugger;
    if (isEdit) updateEmployeeMutateFn({ id: employeeId, data });
    else insertEmployeeMutateFn(data);
  };

  return (
    <Box sx={{ maxWidth: 400, mx: "auto", mt: 4 }}>
      <Typography variant="h5" gutterBottom>
        {isEdit ? "Update Employee" : "Add New Employee"}
      </Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        {/* Name Field */}
        <ReusableTextbox
          name="name"
          control={control}
          label="Name"
          rules={{
            required: "Name is required",
            minLength: { value: 6, message: "Name must be at least 6 characters" },
            maxLength: { value: 10, message: "Name cannot exceed 10 characters" },
          }}
        />

        {/* Email Field */}
        <ReusableTextbox
          name="emailAddress"
          control={control}
          label="Email"
          rules={{
            required: "Email is required",
            pattern: {
              value: /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/,
              message: "Invalid email address",
            },
          }}
        />

        {/* Phone Number Field */}
        <ReusableTextbox
          name="phoneNumber"
          control={control}
          label="Phone Number"
          rules={{
            required: "Phone number is required",
            pattern: {
              value: /^[89]\d{7}$/,
              message: "Invalid Singapore phone number",
            },
          }}
        />

        {/* Assigned Café Dropdown */}
        <FormControl fullWidth sx={{ my: 2 }}>
          <Controller
            name="cafeId"
            control={control}
            rules={{ required: "Assigned Café is required" }}
            render={({ field }) => (
              <TextField select label="Assign Cafe" {...field} variant="outlined" fullWidth error={!!field.error} helperText={field.error?.message} value={field.value}>
                {cafeData &&
                  cafeData.map((option) => (
                    <MenuItem key={option.id} value={option.id}>
                      {option.name}
                    </MenuItem>
                  ))}
              </TextField>
            )}
          />
        </FormControl>

        {/* Gender Radio Button Group */}
        <FormControl component="fieldset" sx={{ my: 2 }}>
          <FormLabel component="legend">Gender</FormLabel>
          <Controller
            name="gender"
            control={control}
            rules={{ required: "Gender is required" }}
            defaultValue={"male"} // <-- Ensure an initial value is set
            render={({ field }) => (
              <RadioGroup {...field} row value={field.value}>
                <FormControlLabel value="0" control={<Radio />} label="Male" />
                <FormControlLabel value="1" control={<Radio />} label="Female" />
                <FormControlLabel value="2" control={<Radio />} label="Other" />
              </RadioGroup>
            )}
          />
        </FormControl>

        {/* Submit Button */}
        <Box sx={{ my: 2 }}>
          <Button type="submit" variant="contained" color="primary" disabled={isLoading}>
            {isEdit ? "Update Employee" : "Add Employee"}
          </Button>
          <Button variant="outlined" color="secondary" style={{ marginLeft: "20px" }} component={Link} to="/employee">
            Cancel
          </Button>
        </Box>
      </form>
    </Box>
  );
};

export default AddEditEmployee;
