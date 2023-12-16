import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import Register from "./Components/Register";
import Login from "./Components/Login";

import {  Route, Router, Routes } from "react-router-dom";

const App = () => {
  return (
    <Router>
      <Routes>
        <Route index path="/auth/signup" element={Register} />
        <Route path="/auth/signin" element={Login} />
      </Routes>
    </Router>
  );
};

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById("root")
);
// server.js

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
