import React, { useEffect, useState } from "react";
import { useQueryClient } from "@tanstack/react-query";
import { Box, Button, Typography, TextField, IconButton, Avatar } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { AgGridReact } from "ag-grid-react";
import { Link } from "@tanstack/react-router";

import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

import DeleteConfirmation from "../FormControls/DeleteConfirmation";
import { useGetCafesByLocationQuery } from "../../services/queries";
import { CAFE_LOGO_URL } from "../../common/global";

const Cafe = () => {
  // set location filter
  const [locationFilter, setLocationFilter] = React.useState("");
  const [cafeLocation, setCafeLocation] = React.useState("");

  // set delete confirmation pop up settings
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [entityId, setEntityId] = useState(null);
  const [entityType, setEntityType] = useState("");

  const queryClient = useQueryClient();

  const handleOpenDialog = (id, type) => {
    setEntityId(id);
    setEntityType(type);
    setIsDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setIsDialogOpen(false);
    setEntityId(null);
    setEntityType("");

    queryClient.invalidateQueries({ queryKey: ["cafesByLocation"] });
  };

  // Debounced API call function
  useEffect(() => {
    const delayDebounceFn = setTimeout(() => {
      setCafeLocation(locationFilter);
    }, 1000); // 500ms debounce

    return () => clearTimeout(delayDebounceFn);
  }, [locationFilter]);

  const { data: cafes, isLoading: isLoading, error: error } = useGetCafesByLocationQuery(cafeLocation);

  const columns = [
    // { headerName: "Logo", field: "logo", autoHeight: true, cellRenderer: (params) => <img src={params.value} alt="logo" style={{ height: "50px" }} />, flex: 1 },
    {
      headerName: "Logo",
      field: "logo",
      autoHeight: true,
      cellRenderer: (params) => (
        <Avatar
          src={`${CAFE_LOGO_URL}/${params.data.logo}`}
          alt={params.value}
          sx={{
            bgcolor: "#f0f0f0",
            width: 40,
            height: 40,
            borderRadius: "50%",
            margin: "5px",
          }}
        />
      ),
      flex: 1,
    },
    { headerName: "Name", field: "name", flex: 2 },
    { headerName: "Description", field: "description", flex: 4 },
    {
      headerName: "Employees",
      field: "employees",
      cellRenderer: (params) => (
        <Button variant="text" component={Link} to={`/employee?cafe=${params.data.name}`}>
          {params.data.employees}
        </Button>
      ),
      flex: 1,
    },
    { headerName: "Location", field: "location", flex: 2 },
    {
      headerName: "Actions",
      field: "actions",
      flex: 1,
      cellRenderer: (params) => (
        <>
          <IconButton color="primary" component={Link} to={`/cafe/${params.data.id}`}>
            <EditIcon />
          </IconButton>
          <IconButton color="secondary" onClick={() => handleOpenDialog(params.data.id, "cafe")}>
            <DeleteIcon />
          </IconButton>
        </>
      ),
    },
  ];

  return (
    <div style={{ width: "100%", height: "100vh" }}>
      <DeleteConfirmation open={isDialogOpen} handleClose={handleCloseDialog} entityId={entityId} entityType={entityType} />
      <Typography variant="h4">Cafe List</Typography>
      <Box display="flex" justifyContent="space-between" alignItems="center" style={{ margin: "10px 0px" }}>
        <Button variant="contained" color="primary" component={Link} to="/cafe/add">
          Add New Cafe
        </Button>
        <TextField label="Filter by Location" variant="outlined" value={locationFilter} onChange={(e) => setLocationFilter(e.target.value)} size="small" />
      </Box>

      {isLoading ? (
        <Typography>Loading...</Typography>
      ) : error ? (
        <Typography color="error">Failed to load cafes.</Typography>
      ) : (
        <Box className="ag-theme-alpine" style={{ height: "500px", width: "100%" }}>
          <AgGridReact
            domLayout="autoHeight"
            rowData={cafes}
            columnDefs={columns}
            defaultColDef={{ flex: 1, minWidth: 100, resizable: true }}
            pagination={true}
            paginationPageSize={10}
          />
        </Box>
      )}
    </div>
  );
};

export default Cafe;
