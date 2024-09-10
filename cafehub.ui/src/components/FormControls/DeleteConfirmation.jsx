import React from "react";
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Button } from "@mui/material";
import { useMutation } from "@tanstack/react-query";
import { showNotification } from "../../store/notificationSlice";
import { useDispatch } from "react-redux";

const deleteEntity = async ({ id, entity }) => {
  const response = await fetch(`http://localhost:9000/api/${entity}/${id}`, {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
  });

  if (!response.ok) throw new Error(`Failed to update ${entity}`);
  if (response.status === 204) return null; // No content to return
  return response.json();
};

const DeleteConfirmation = ({ open, handleClose, entity, entityId, entityType }) => {
  const dispatch = useDispatch();
  const { mutate: deleteEntityMutateFn } = useMutation({
    mutationFn: deleteEntity,
    mutationKey: "deleteEntity",
    onSuccess: () => dispatch(showNotification({ message: `${entityType} deleted successfully!`, severity: "success", duration: 3000 })),
    onError: (error) => dispatch(showNotification({ message: `Error deleting ${entityType}.`, severity: "error", duration: 3000 })),
    onSettled: () => handleClose(),
  });

  const handleDelete = () => {
    deleteEntityMutateFn({ id: entityId, entity: entityType });
  };

  return (
    <Dialog open={open} onClose={handleClose} aria-labelledby="delete-confirmation-title" aria-describedby="delete-confirmation-description">
      <DialogTitle id="delete-confirmation-title">Delete {entityType.charAt(0).toUpperCase() + entityType.slice(1)}</DialogTitle>
      <DialogContent>
        <DialogContentText id="delete-confirmation-description">Are you sure you want to delete this {entityType}? This action cannot be undone.</DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="primary">
          Cancel
        </Button>
        <Button onClick={handleDelete} color="secondary" variant="contained">
          Delete
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default DeleteConfirmation;
