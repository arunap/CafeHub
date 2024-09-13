import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createCafe, deleteCafe, updateCafe, uploadCafeLogo } from "./cafeApi";
import { useDispatch } from "react-redux";
import { showNotification } from "../store/notificationSlice";
import { createEmployee, deleteEmployee, updateEmployee } from "./employeeApi";

// Mutation to submit form data
export const useCreateCafeFn = () => {
  const queryClient = useQueryClient();
  const dispatch = useDispatch();

  return useMutation({
    mutationFn: (data) => createCafe(data),
    onSuccess: () => {
      dispatch(showNotification({ message: "Cafe registered successfully!", severity: "success", duration: 3000 }));
    },
    onError: (error) => dispatch(showNotification({ message: "Error registering cafe.", severity: "error", duration: 3000 })),
    // onSettled: async (_, error) => {
    //   if (error) console.log(error);
    //   else await queryClient.invalidateQueries({ queryKey: ["cafes"] });
    // },
  });
};

export const useUpdateCafeFn = () => {
  const dispatch = useDispatch();

  return useMutation({
    mutationFn: ({ id, data }) => updateCafe({ id, data }),
    onSuccess: () => {
      dispatch(showNotification({ message: "Cafe updated successfully!", severity: "success", duration: 3000 }));
    },
    onError: (error) => {
      console.log(error);
      dispatch(showNotification({ message: "Error updating cafe.", severity: "error", duration: 3000 }));
    },
  });
};

export const useUploadCageLogoFn = () => {
  return useMutation({
    mutationFn: (data) => {
      console.log(data);
      return uploadCafeLogo(data);
    },
    onSuccess: (data) => {
      console.log(JSON.stringify(data));
    },
    onError: (err) => console.log("file upload error: ", err),
  });
};

export const useDeleteCafeFn = () => {
  const dispatch = useDispatch();
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id) => deleteCafe(id),
    onSuccess: () => dispatch(showNotification({ message: `Cafe deleted successfully!`, severity: "success", duration: 3000 })),
    onError: (error) => dispatch(showNotification({ message: `Error deleting cafe.`, severity: "error", duration: 3000 })),
    onSettled: () => queryClient.invalidateQueries({ queryKey: ["cafes"] }),
  });
};

export const useCreateEmployeeFn = () => {
  const dispatch = useDispatch();
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data) => createEmployee(data),
    onSuccess: () => {
      dispatch(showNotification({ message: "Employee registered successfully!", severity: "success", duration: 3000 }));
    },
    onError: (error) => {
      console.log(error);
      dispatch(showNotification({ message: "Error registering employee.", severity: "error", duration: 3000 }));
    },
    //  onSettled: () => queryClient.invalidateQueries({ queryKey: ["employees"] }),
  });
};

export const useUpdateEmployeeFn = () => {
  const dispatch = useDispatch();
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }) => updateEmployee({ id, data }),
    onSuccess: () => {
      dispatch(showNotification({ message: "Employee updated successfully!", severity: "success", duration: 3000 }));
    },
    onError: (error) => {
      console.log(error);
      dispatch(showNotification({ message: "Error updating Employee.", severity: "error", duration: 3000 }));
    },
    //onSettled: () => queryClient.invalidateQueries({ queryKey: ["employees"] }),
  });
};

export const useDeleteEmployeeFn = () => {
  const queryClient = useQueryClient();
  const dispatch = useDispatch();
  return useMutation({
    mutationFn: (id) => deleteEmployee(id),
    onSuccess: () => dispatch(showNotification({ message: `Employee deleted successfully!`, severity: "success", duration: 3000 })),
    onError: (error) => dispatch(showNotification({ message: `Error deleting employee.`, severity: "error", duration: 3000 })),
    onSettled: () => queryClient.invalidateQueries({ queryKey: ["employees"] }),
  });
};
