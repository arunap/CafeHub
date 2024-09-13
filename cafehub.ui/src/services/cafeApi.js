import { BASE_API_URL } from "./../common/global";

// API call to fetch cafes
export const getCafes = async () => {
  const response = await fetch(`${BASE_API_URL}/cafes`);
  if (!response.ok) throw new Error("Failed to fetch cafes");
  return response.json();
};

// fetch cafe by location
export const getCafesByLocation = async (location) => {
  const response = await fetch(`${BASE_API_URL}/cafes?location=${location}`);
  if (!response.ok) throw new Error("Failed to fetch cafes");
  return response.json();
};

export const getCafeByCafeId = async (id) => {
  const response = await fetch(`${BASE_API_URL}/cafe/${id}`);
  if (!response.ok) throw new Error("Failed to fetch cafe");
  return response.json();
};

// insert a cafe
export const createCafe = async (newCafe) => {
  const response = await fetch(`${BASE_API_URL}/cafe`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newCafe),
  });

  if (!response.ok) throw new Error("Failed to create Cafe");
  return response.json();
};

// update a cafe
export const updateCafe = async ({ id, data }) => {
  debugger;
  const response = await fetch(`${BASE_API_URL}/cafe/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });

  if (!response.ok) throw new Error("Failed to update Cafe");
  if (response.status === 204) return null; // No content to return
  return response.json();
};

export const deleteCafe = async ({ id }) => {
  const response = await fetch(`http://localhost:9000/api/cafe/${id}`, {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
  });

  if (!response.ok) throw new Error(`Failed to delete cafe`);
  if (response.status === 204) return null; // No content to return
  return response.json();
};

export const uploadCafeLogo = async ({ file, fileName }) => {
  const formData = new FormData();
  formData.append("formFile", file);
  formData.append("fileName", fileName);

  const response = await fetch(`${BASE_API_URL}/cafe/UploadCafePhoto`, {
    method: "POST",
    body: formData,
  });
  if (!response.ok) throw new Error("Error in file uploading...");
  return response.json();
};
