import React from "react";
import { Box, Typography, Button } from "@mui/material";
// import { useNavigate } from "react-router-dom";

const NotFound = () => {
  //   const navigate = useNavigate();

  return (
    <Box display="flex" flexDirection="column" justifyContent="center" alignItems="center" bgcolor="#f5f5f5" textAlign="center" p={3}>
      <Typography variant="h1" component="h1" gutterBottom>
        404
      </Typography>
      <Typography variant="h4" component="h2" gutterBottom>
        Page Not Found
      </Typography>
      <Typography variant="body1" gutterBottom>
        Sorry, the page you are looking for does not exist.
      </Typography>
      <Button variant="contained" color="primary" sx={{ mt: 2 }} href="/">
        Go to Home
      </Button>
    </Box>
  );
};
export default NotFound;
// onClick={() => navigate("/")}
