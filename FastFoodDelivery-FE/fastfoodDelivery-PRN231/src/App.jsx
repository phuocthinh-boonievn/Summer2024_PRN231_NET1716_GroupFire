import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./components/layout";
import AdminAccountManagement from "./pages/accountuser-management";
import FoodItemManagement from "./pages/fastfood-magegement";
import HomePage from "./pages/home";
import Login from "./pages/login";

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      children: [
        {
          path: "/",
          element: <HomePage />,
        },
        {
          path: "/fastfood-magegement",
          element: <FoodItemManagement />,
        },
        {
          path: "/accountuser-management",
          element: <AdminAccountManagement />,
        },
        {
          path: "/login",
          element: <Login />,
        },
      ],
    },

    // {
    //   path: "/",
    //   element: <HomePage />,
    // },
    // {
    //   path: "/fastfood-magegement",
    //   element: <FoodItemManagement />,
    // },
    // {
    //   path: "/login",
    //   element: <Login />,
    // },
  ]);

  return <RouterProvider router={router} />;
}

export default App;
