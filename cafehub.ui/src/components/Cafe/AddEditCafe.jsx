import { Box, Button, FormControl, FormLabel, Typography } from "@mui/material";
import React, { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "@tanstack/react-router";
import { Input } from "@mui/material";
import ReusableTextbox from "../FormControls/ReusableTextbox";
import { useForm, Controller } from "react-hook-form";
import { useDispatch } from "react-redux";
import { useGetCafeByCafeIdQuery } from "../../services/queries";
import { useCreateCafeFn, useUpdateCafeFn, useUploadCageLogoFn } from "../../services/mutations";

const AddEditCafe = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { cafeId } = useParams({ strict: false });
  const isEdit = !!cafeId;
  // If in edit mode, fetch the cafe's information and prefill the form
  const { data: cafeItem } = useGetCafeByCafeIdQuery(cafeId, isEdit);

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
  const createMutateFn = useCreateCafeFn();
  const updateMutateFn = useUpdateCafeFn();
  const uploadCageLogoFn = useUploadCageLogoFn();

  const insertCafeDataToApi = async (data) => {
    if (isEdit) await updateMutateFn.mutateAsync({ id: cafeId, data });
    else await createMutateFn.mutateAsync(data);
    navigate({ to: "/cafe" });
  };
  const [file, setFile] = useState(null);
  const [fileName, setFileName] = useState(null);
  const handleFileChange = (e) => {
    setFile(e.target.files[0]);
    setFileName(e.target.files[0].name);
  };

  const onSubmit = async (formData) => {
    if (file && fileName) {
      const data = await uploadCageLogoFn.mutateAsync({ file, fileName });
      insertCafeDataToApi({ ...formData, logo: data.imageUrl });
    } else insertCafeDataToApi(formData);
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
          <Controller name="logo" control={control} render={({ field }) => <Input type="file" onChange={handleFileChange} inputProps={{ accept: "image/*" }} />} />
        </FormControl>
        {/* <FormLabel>Upload Profile Picture</FormLabel>
        <input type="file" onChange={handleFileChange} /> */}

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
