import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./components/layout";
// import AdminAccountManagement from "./pages/accountuser-management";
import Category from "./pages/category-management";
import FoodItemManagement from "./pages/fastfood-magegement";
import HomePage from "./pages/home";
import Login from "./pages/login";
import PaymentFailure from "./pages/payment/paymentFail";
import PaymentSuccess from "./pages/payment/paymentSuccess";
import UserAccount from "./pages/profolio";
import Register from "./pages/register";
import ShoppingCart from "./pages/shoppingcart/ShoppingCart";
import UserFeedback from "././pages/feedback/UserFeedback";
import ShipperAccountManagement from "./pages/accountuser-management/ShipperAccountManagement";
import AddShipper from "./pages/accountuser-management/AddShipper";
import AdminAccountManagement from "./pages/accountuser-management/AdminAccountManagement";
import Dashboard from "./pages/dashboard-management";

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
        // {
        //   path: "/category-management",
        //   element: <Category />,
        // },
        {
          path: "/accountuser-management",
          element: <AdminAccountManagement />,
        },
        {
          path: "/view-feedback",
          element: <UserFeedback />,
        },
        {
          path: "/shipper-account-management",
          element: <ShipperAccountManagement />,
        },
        {
          path: "/add-shipper",
          element: <AddShipper />,
        },
        {
          path: "/login",
          element: <Login />,
        },
        {
          path: "/register",
          element: <Register />,
        },
        {
          path: "/shoppingcart",
          element: <ShoppingCart />,
        },
        {
          path: "/paymentSuccess",
          element: <PaymentSuccess />,
        },
        {
          path: "/paymentFail",
          element: <PaymentFailure />,
        },
        {
          path: "/profolio",
          element: <UserAccount />,
        },
      ],
    },
    {
      path: "/dashboard",
      element: <Dashboard />,
      children: [
        {
          path: "/dashboard/category",
          element: <Category />,
        },
      ],
    },
    // {
    //   path: "/profolio",
    //   element: <UserAccount />,
    // },
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
