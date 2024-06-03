import {RouterProvider, createBrowserRouter } from "react-router-dom";
import FoodItemManagement from "./pages/fastfood-magegement";
import HomePage from "./pages/home";

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <HomePage />,
    },
    {
      path: "/fastfood-magegement",
      element: <FoodItemManagement />,
    },
  ]);

  return <RouterProvider router={router} />;
}

export default App;
