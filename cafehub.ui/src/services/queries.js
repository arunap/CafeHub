import { useQuery } from "@tanstack/react-query";
import { getCafeByCafeId, getCafes, getCafesByLocation } from "./cafeApi";
import { getEmployeeByEmployeeId, getEmployees, getEmployeesByCafeName } from "./employeeApi";

export const useGetCafesQuery = () => {
  return useQuery({
    queryKey: ["cafes"],
    queryFn: () => getCafes(),
  });
};

// If in edit mode, fetch the cafe's information and prefill the form
export const useGetCafeByCafeIdQuery = (cafeId, isEdit) => {
  return useQuery({
    queryKey: ["cafe", { cafeId }],
    queryFn: () => getCafeByCafeId(cafeId),
    enabled: isEdit && !!cafeId,
  });
};

export const useGetCafesByLocationQuery = (locationFilter) => {
  return useQuery({
    queryKey: ["cafesByLocation", { locationFilter }],
    queryFn: () => getCafesByLocation(locationFilter),
  });
};

export const useGetEmployeesQuery = (cafe) => {
  return useQuery({
    queryKey: ["employees"],
    queryFn: () => (cafe === undefined ? getEmployees() : getEmployeesByCafeName(cafe)),
  });
};

export const useGetEmployeesByCafeNameQuery = (cafeName) => {
  return useQuery({
    queryKey: ["employeesByCafe", { cafeName }],
    queryFn: () => getEmployeesByCafeName(cafeName),
  });
};

export const useGetEmployeesByEmployeeIdQuery = (employeeId, isEnabled) => {
  return useQuery({
    queryKey: ["employeesByEmployeeId", { employeeId }],
    queryFn: () => getEmployeeByEmployeeId(employeeId),
    enabled: !!employeeId && isEnabled,
  });
};
