import { BASE_API_URL } from "./../common/global";

// Define the mutation function
export const createEmployee = async (newEmployee) => {
  const response = await fetch(`${BASE_API_URL}/employee`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newEmployee),
  });

  if (!response.ok) throw new Error("Failed to create Employee");
  return response.json();
};

export const updateEmployee = async ({ id, data }) => {
  const response = await fetch(`${BASE_API_URL}/employee/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });

  if (!response.ok) throw new Error("Failed to update Employee");
  if (response.status === 204) return null; // No content to return
  return response.json();
};

export const getEmployees = async () => {
  const response = await fetch(`${BASE_API_URL}/Employees`);
  if (!response.ok) throw new Error("Failed to fetch Employees");
  return response.json();
};

export const getEmployeesByCafeName = async (cafeName) => {
  const response = await fetch(`${BASE_API_URL}/Employees?cafe=${cafeName}`);
  if (!response.ok) throw new Error("Failed to fetch Employees");
  return response.json();
};

export const getEmployeeByEmployeeId = async (id) => {
  const response = await fetch(`${BASE_API_URL}/Employee/${id}`);
  if (!response.ok) throw new Error("Failed to fetch Employee");
  return response.json();
};

export const deleteEmployee = async ({ id }) => {
  const response = await fetch(`http://localhost:9000/api/employee/${id}`, {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
  });

  if (!response.ok) throw new Error(`Failed to delete employee`);
  if (response.status === 204) return null; // No content to return
  return response.json();
};
