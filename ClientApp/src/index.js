import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
// import Register from "./Components/Register";
// import Login from "./Components/Login";
import UserInfo from "./User/UserInfo";

//import { BrowserRouter, Route, Routes } from "react-router-dom";

// const App = () => {
//   return (
//     <BrowserRouter>
//       <Routes>
//         <Route exact path="/auth/signup" element={Register} />
//         <Route path="/auth/signin" element={Login} />
//       </Routes>
//     </BrowserRouter>
//   );
// };
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <UserInfo />
  </React.StrictMode>
);

// server.js