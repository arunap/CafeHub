import { createRootRoute, createRoute, Outlet } from "@tanstack/react-router";
import MainLayout from "../layouts/MainLayout";
import Home from "../components/Home";

export const rootRoute = createRootRoute({
  component: () => <MainLayout />,
});

export const indexRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "/",
  component: () => <Home />,
});
