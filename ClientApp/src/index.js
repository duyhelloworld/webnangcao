import React from "react";
import {createRoot} from 'react-dom/client';
import "./index.css";
import { createBrowserRouter } from "react-router-dom";
import { RouterProvider } from "react-router-dom";
// import Navigation from './Components/Home/Navigation';
import Home from './Components/Home/Home';
import Register from './Components/Home/Register';
import Login from './Components/Home/Login';
// import Footer from "./Components/Home/Footer";
import { AuthProvider } from "./Components/Home/AuthContext";
import HelpContent from "./Components/Content/HelpContent";
// import AboutUs from "./Page/Aboutus";
// import Help from "./Page/Help";
import Layout from "./Page/layout";
import AboutUsContent from "./Components/Content/AboutUsContent";





const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        path: "/home",
        element: <Home />,
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
        path: "/aboutus",
        element: <AboutUsContent />, // Thay thế Footer thành component riêng cho trang About Us
      },
      {
        path: "/help",
        element: <HelpContent />, // Thay thế Footer thành component riêng cho trang Help
      },
      {
        path: "/"
      }

    ],
  }
])




createRoot(document.getElementById('root')).render(
  <React.StrictMode>
   <AuthProvider>
    <RouterProvider router ={router}/>
    </AuthProvider>
  </React.StrictMode>
);
// server.js

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
