import { createRoute } from "@tanstack/react-router";
import { rootRoute } from "./rootRoute";
import Employee from "../components/Employee/Employee";
import AddEditEmployee from "../components/Employee/AddEditEmployee";

export const employeeRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "/employee",
  component: () => <Employee />,
});

export const employeeCafeRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "/employee/:cafe",
  component: () => <Employee />,
});

export const employeeAddRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "employee/add",
  component: () => <AddEditEmployee />,
});

export const employeeEditRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "employee/$employeeId",
  // component: function Employee() {
  //   return <div className="p-2">Edit employee!</div>;
  // },
  component: () => <AddEditEmployee />,
});
