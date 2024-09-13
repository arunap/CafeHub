import React, { useEffect } from "react";
import { TextField, Button, Box, Typography, FormControlLabel, RadioGroup, Radio, FormLabel } from "@mui/material";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import { Link, useNavigate, useParams } from "@tanstack/react-router";
import { useForm, Controller } from "react-hook-form";

import ReusableTextbox from "../FormControls/ReusableTextbox";
import { useDispatch } from "react-redux";
import { useGetCafesQuery, useGetEmployeesByEmployeeIdQuery } from "../../services/queries";
import { useCreateEmployeeFn, useUpdateEmployeeFn } from "../../services/mutations";

const AddEditEmployee = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { employeeId } = useParams({ strict: false });
  const isEdit = !!employeeId;

  // If in edit mode, fetch the cafe's information and prefill the form
  const { data: cafeData, isLoading } = useGetCafesQuery();
  const { data: employeeItem } = useGetEmployeesByEmployeeIdQuery(employeeId, isEdit && !!cafeData);

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
    if (!isEdit && cafeData) {
      const updatedFormValues = { cafeId: cafeData[0]["id"], name: "", emailAddress: "", phoneNumber: "", gender: "0" };
      reset(updatedFormValues);
    } else if (employeeItem) {
      const updatedFormValues = { cafeId: cafeData.filter((x) => x.name === employeeItem.cafeName)[0]["id"], ...employeeItem };
      reset(updatedFormValues);
    } // Populate the form with the fetched data
  }, [employeeItem, cafeData, reset]);

  // Mutation to submit form data
  const createMutateFn = useCreateEmployeeFn();
  const updateMutateFn = useUpdateEmployeeFn();

  const onSubmit = async (data) => {
    if (isEdit) {
      await updateMutateFn.mutateAsync({ id: employeeId, data });
    } else {
      await createMutateFn.mutateAsync(data);
    }
    navigate({ to: "/employee" });
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
              value: /^\+65\d{8}$/,
              message: "Phone number must be a valid Singapore number starting with +65 and followed by 8 digits.",
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
