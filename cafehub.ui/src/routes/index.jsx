import { createRouter } from "@tanstack/react-router";
import { cafeAddRoute, cafeEditRoute, cafeRoute } from "./CafeRoutes";
import { employeeAddRoute, employeeEditRoute, employeeRoute, employeeCafeRoute } from "./EmployeeRoutes";
import { indexRoute, rootRoute } from "./rootRoute";
import NotFound from "../components/NotFound";

const routeTree = rootRoute.addChildren([indexRoute, cafeRoute, cafeEditRoute, cafeAddRoute, employeeRoute, employeeAddRoute, employeeEditRoute, employeeCafeRoute]);

const router = createRouter({
  routeTree,
  defaultNotFoundComponent: NotFound,
});

export default router;
