import { Link } from "@tanstack/react-router";
import React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import Container from "@mui/material/Container";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import AdbIcon from "@mui/icons-material/Adb";

// const NavigationBar = () => {
//   return (
//     <nav>
//       <ul>
//         <li>
//           <Link to="/cafe">Cafe</Link>
//         </li>
//         <li>
//           <Link to="/cafe/add">Add Cafe</Link>
//         </li>
//         <li>
//           <Link to="/cafe/1/edit">Edit Cafe</Link>
//         </li>
//         <li>
//           <Link to="/employee">Employee</Link>
//         </li>
//         <li>
//           <Link to="/employee/add">Add Employee</Link>
//         </li>
//         <li>
//           <Link to="/employee/2/edit">edit Employee</Link>
//         </li>
//         <li>
//           <Link to="/about">About Us</Link>
//         </li>
//       </ul>
//     </nav>
//   );
// };

const NavigationBar = () => {
  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          Cafe Management System
        </Typography>
        <Button color="inherit" component={Link} to="/">
          Home
        </Button>
        <Button color="inherit" component={Link} to="/cafe">
          Cafe
        </Button>
        <Button color="inherit" component={Link} to="/employee">
          Employee
        </Button>
      </Toolbar>
    </AppBar>
  );
};

export default NavigationBar;
