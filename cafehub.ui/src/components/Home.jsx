import React from "react";
import { Box, Typography, Button } from "@mui/material";

const Home = () => {
  return (
    <Box display="flex" flexDirection="column" justifyContent="center" alignItems="center" textAlign="center" p={3}>
      <Typography variant="h2" component="h1" gutterBottom>
        Welcome to the Cafe Management System
      </Typography>
      <Typography variant="h5" component="h2" gutterBottom>
        Manage your cafes, employees, and more with ease.
      </Typography>
      <Typography variant="body1" gutterBottom>
        Start by exploring the various options available in the navigation menu.
      </Typography>
      <Button variant="contained" color="primary" href="/cafe" sx={{ mt: 3 }}>
        Get Started
      </Button>
    </Box>
  );
};

export default Home;
