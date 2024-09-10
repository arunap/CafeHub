import React from "react";
import TextField from "@mui/material/TextField";
import { Controller } from "react-hook-form";

// const ReusableTextbox = ({ label, value, onChange, error, helperText, ...props }) => {
//   return <TextField label={label} value={value} onChange={onChange} error={!!error} helperText={helperText || error} fullWidth {...props} margin="normal" variant="outlined" />;
// };

// export default ReusableTextbox;

const ReusableTextbox = ({ name, control, label, rules, ...props }) => {
  return (
    <Controller
      name={name}
      control={control}
      rules={rules}
      render={({ field, fieldState: { error } }) => (
        <TextField
          {...field}
          label={label}
          error={!!error}
          helperText={error ? error.message : ""}
          variant="outlined"
          fullWidth
          margin="normal"
          {...props}
        />
      )}
    />
  );
};

export default ReusableTextbox;
