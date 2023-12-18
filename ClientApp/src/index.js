import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Register from "./Components/Register";
import Login from "./Components/Login";
import UserInfo from "./User/UserInfo";
import TrackPlayer from "./Track/TrackPlayer";
import Home from './Components/Home';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    {/* <BrowserRouter>
      <Routes>
        <Route exact path="/" element={Home} />
        <Route exact path="/auth/signup" element={Register} />
        <Route path="/auth/signin" element={Login} />
        <Route path="/user/info" element={UserInfo} />
        <Route path="/track/play" element={TrackPlayer} />
      </Routes>
    </BrowserRouter> */}
    <UserInfo />
  </React.StrictMode>
);

// server.js