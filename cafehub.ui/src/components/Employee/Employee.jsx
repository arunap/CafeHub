import React, { useState } from "react";
import { Box, Button, IconButton, Typography } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { AgGridReact } from "ag-grid-react";
import { Link } from "@tanstack/react-router";

import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

import DeleteConfirmation from "../FormControls/DeleteConfirmation";
import { useGetEmployeesQuery } from "../../services/queries";

const Employee = () => {
  const searchParams = new URLSearchParams(window.location.search);
  const params = Object.fromEntries(searchParams.entries());
  const cafeNameParam = params["cafe"];

  // Conditionally execute the appropriate query hook
  const { data: employees, error, isLoading } = useGetEmployeesQuery(cafeNameParam);

  // set delete confirmation pop up settings
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [entityId, setEntityId] = useState(null);
  const [entityType, setEntityType] = useState("");

  const handleOpenDialog = (id, type) => {
    setEntityId(id);
    setEntityType(type);
    setIsDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setIsDialogOpen(false);
    setEntityId(null);
    setEntityType("");
  };

  const columnDefs = [
    { headerName: "Employee ID", field: "id", sortable: true, filter: true, flex: 1 },
    { headerName: "Name", field: "name", sortable: true, filter: true, flex: 1 },
    { headerName: "Email", field: "emailAddress", sortable: true, filter: true, flex: 1 },
    { headerName: "Phone Number", field: "phoneNumber", sortable: true, filter: true, flex: 1 },
    { headerName: "Days Worked", field: "daysWorked", sortable: true, filter: true, flex: 1 },
    { headerName: "Café Name", field: "cafeName", sortable: true, filter: true, flex: 1 },
    {
      headerName: "Actions",
      field: "actions",
      flex: 1,
      cellRenderer: (params) => (
        <>
          <IconButton color="primary" component={Link} to={`/employee/${params.data.id}`}>
            <EditIcon />
          </IconButton>
          <IconButton color="secondary" onClick={() => handleOpenDialog(params.data.id, "employee")}>
            <DeleteIcon />
          </IconButton>
        </>
      ),
    },
  ];

  return (
    <div style={{ width: "100%", height: "100vh" }}>
      <DeleteConfirmation open={isDialogOpen} handleClose={handleCloseDialog} entityId={entityId} entityType={entityType} />
      <Typography variant="h4">Employee List</Typography>
      <Box display="flex" justifyContent="space-between" alignItems="center" style={{ margin: "10px 0px" }}>
        <Button variant="contained" color="primary" component={Link} to="/employee/add">
          Add New Employee
        </Button>
      </Box>

      {isLoading ? (
        <Typography>Loading...</Typography>
      ) : error ? (
        <Typography color="error">Failed to load employees.</Typography>
      ) : (
        <Box className="ag-theme-alpine" style={{ height: "500px", width: "100%" }}>
          <AgGridReact
            columnDefs={columnDefs}
            defaultColDef={{ flex: 1, minWidth: 100, resizable: true }}
            rowData={employees}
            domLayout="autoHeight"
            pagination={true}
            paginationPageSize={10}
          />
        </Box>
      )}
    </div>
  );
};

export default Employee;
