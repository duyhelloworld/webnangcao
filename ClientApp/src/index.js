// index.js
import React from "react";
import "./index.css";
import {createRoot} from 'react-dom/client';
import "./index.css";
import { createBrowserRouter } from "react-router-dom";
import { RouterProvider } from "react-router-dom";
import Home from './Components/Home/Home';
import Register from './Components/Home/Register';
import Login from './Components/Home/Login';
import ViewTrackAll from "./Track/ViewTrackAll";
import { AuthProvider } from "./Components/Home/AuthContext";
import HelpContent from "./Components/Content/HelpContent";

import Layout from "./Page/layout";
import AboutUsContent from "./Components/Content/AboutUsContent";
import 'bootstrap/dist/css/bootstrap.min.css';
import TrackPlayer from "./Track/TrackPlayer";




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
        path: "/track/library",
        element: <ViewTrackAll/>
      },
      {
       path: "/track/media/:trackId",
       element: <TrackPlayer/>
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