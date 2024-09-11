import { Box, Button, FormControl, FormLabel, Typography } from "@mui/material";
import React, { useEffect } from "react";
import { Link, useNavigate, useParams } from "@tanstack/react-router";
import { Input } from "@mui/material";
import ReusableTextbox from "../FormControls/ReusableTextbox";
import { useMutation, useQuery } from "@tanstack/react-query";
import { useForm, Controller } from "react-hook-form";
import { useDispatch } from "react-redux";
import { showNotification } from "../../store/notificationSlice";

// Define the mutation function
const insertCafeToDb = async (newCafe) => {
  const response = await fetch("http://localhost:9000/api/cafe", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newCafe),
  });

  if (!response.ok) throw new Error("Failed to create Cafe");
  return response.json();
};

const updateCafeToDb = async ({ id, data }) => {
  const response = await fetch(`http://localhost:9000/api/cafe/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });

  if (!response.ok) throw new Error("Failed to update Cafe");
  if (response.status === 204) return null; // No content to return
  return response.json();
};

const fetchCafe = async ({ queryKey }) => {
  const [, cafeId] = queryKey;
  const response = await fetch(`http://localhost:9000/api/cafe/${cafeId}`);
  if (!response.ok) throw new Error("Failed to fetch cafe");
  return response.json();
};

const AddEditCafe = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { cafeId } = useParams({ strict: false });
  const isEdit = !!cafeId;

  // If in edit mode, fetch the cafe's information and prefill the form
  const { data: cafeItem } = useQuery({ queryKey: ["cafe", cafeId], queryFn: fetchCafe, enabled: isEdit });

  const { control, handleSubmit, reset } = useForm({
    defaultValues: { name: "", description: "", logo: "", location: "" },
  });

  // Set the default values once data is fetched
  useEffect(() => {
    if (cafeItem) {
      reset(cafeItem); // Populate the form with the fetched data
    }
  }, [cafeItem, reset]);

  // Mutation to submit form data
  const { mutate: insertCafeMutateFn } = useMutation({
    mutationFn: insertCafeToDb,
    mutationKey: "insertCafeToDb",
    onSuccess: () => {
      reset();
      dispatch(showNotification({ message: "Cafe registered successfully!", severity: "success", duration: 3000 }));
    },
    onError: (error) => dispatch(showNotification({ message: "Error registering cafe.", severity: "error", duration: 3000 })),
  });

  const { mutate: updateCafeMutateFn } = useMutation({
    mutationFn: updateCafeToDb,
    mutationKey: "updateCafeToDb",
    onSuccess: () => {
      reset();
      dispatch(showNotification({ message: "Cafe registered successfully!", severity: "success", duration: 3000 }));
      navigate({ to: "/cafe" });
    },
    onError: (error) => {
      console.log(error);
      dispatch(showNotification({ message: "Error registering cafe.", severity: "error", duration: 3000 }));
    },
  });

  const onSubmit = (data) => {
    if (isEdit) updateCafeMutateFn({ id: cafeId, data });
    else insertCafeMutateFn(data);
  };

  return (
    <Box sx={{ maxWidth: 400, mx: "auto", mt: 4 }}>
      <Typography variant="h5" gutterBottom>
        {isEdit ? "Update Cafe" : "Add New Cafe"}
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

        {/* Description Field */}
        <ReusableTextbox
          name="description"
          control={control}
          label="Description"
          multiline
          rows={4}
          rules={{
            required: "Description is required",
            maxLength: { value: 256, message: "Description cannot exceed 256 characters" },
          }}
        />

        {/* Image Upload Field */}
        <FormControl fullWidth style={{ marginTop: "20px" }}>
          <FormLabel>Upload Profile Picture</FormLabel>
          <Controller
            name="logo"
            control={control}
            render={({ field }) => <Input type="file" onChange={(e) => field.onChange(e.target.files[0])} inputProps={{ accept: "image/*" }} />}
          />
        </FormControl>

        {/* Location Field */}
        <ReusableTextbox
          name="location"
          control={control}
          label="Location"
          rules={{
            required: "Location is required",
          }}
        />
        <Box sx={{ my: 2 }}>
          <Button type="submit" variant="contained" color="primary">
            {isEdit ? "Update Cafe" : "Add Cafe"}
          </Button>
          <Button variant="outlined" color="secondary" style={{ marginLeft: "20px" }} component={Link} to="/cafe">
            Cancel
          </Button>
        </Box>
      </form>
    </Box>
  );
};

export default AddEditCafe;
