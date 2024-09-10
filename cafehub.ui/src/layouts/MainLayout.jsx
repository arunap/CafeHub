import { Outlet } from "@tanstack/react-router";
import React from "react";
import NavigationBar from "./NavigationBar";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import NotificationBanner from "../components/FormControls/NotificationBanner";

const MainLayout = () => {
  return (
    <>
      <NavigationBar />
      <NotificationBanner />
      <Container maxWidth={false}>
        <Box sx={{ my: 4 }}>
          <Box>
            <Outlet />
          </Box>
        </Box>
      </Container>
    </>

    // <Box display="flex" flexDirection="column" minHeight="100vh" overflow="hidden">
    //   <NavigationBar />
    //   <Container
    //     maxWidth="lg"
    //     sx={{
    //       flexGrow: 1,
    //       display: "flex",
    //       flexDirection: "column",
    //       justifyContent: "center",
    //       alignItems: "center",
    //       p: 3,
    //     }}>
    //     <Outlet />
    //   </Container>
    // </Box>
  );
};

export default MainLayout;
