import { createRoute } from "@tanstack/react-router";
import { rootRoute } from "./rootRoute";
import Cafe from "../components/Cafe/Cafe";
import AddEditCafe from "../components/Cafe/AddEditCafe";

export const cafeRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "/cafe",
  component: Cafe,
});

export const cafeAddRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "cafe/add",
  component: () => <AddEditCafe />,
});

export const cafeEditRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "cafe/$cafeId",
  // component: function Cafe() {
  //   return <div className="p-2">Edit Cafe!</div>;
  // },
  component: () => <AddEditCafe />,
});
