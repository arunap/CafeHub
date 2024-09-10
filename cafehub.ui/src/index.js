import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import "./App.css";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

import router from "./routes";
import { RouterProvider } from "@tanstack/react-router";
import { Provider } from "react-redux";
import { store } from "./store/store";

const rootElement = document.getElementById("root");

const queryClient = new QueryClient();

if (!rootElement.innerHTML) {
  const root = ReactDOM.createRoot(rootElement);
  root.render(
    <React.StrictMode>
      <Provider store={store}>
        <QueryClientProvider client={queryClient}>
          <RouterProvider router={router} />
        </QueryClientProvider>
      </Provider>
    </React.StrictMode>
  );
}
