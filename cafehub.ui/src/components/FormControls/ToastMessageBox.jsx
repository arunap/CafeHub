import React, { useState } from "react";
import { Button, Snackbar, Alert } from "@mui/material";

const ToastMessageBox = ({ open, handleClose, message, severity }) => {
  return (
    <Snackbar open={open} autoHideDuration={6000} onClose={handleClose} anchorOrigin={{ vertical: "top", horizontal: "right" }}>
      <Alert onClose={handleClose} severity={severity} sx={{ width: "100%" }}>
        <b> {message}</b>
      </Alert>
    </Snackbar>
  );
};

export default ToastMessageBox;
