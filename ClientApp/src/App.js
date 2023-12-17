import React from 'react';
import {  Route, Routes, Link, BrowserRouter } from "react-router-dom";

import Home from './Components/Home';
import Register from './Components/Register';
import Login from './Components/Login';

const App = () => {
  return (
    <BrowserRouter>
      <div>
        <nav>
          <ul>
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/register">Register</Link>
            </li>
            <li>
                <Link to="/login">Login</Link>
            </li>
          </ul>
        </nav>
        <Routes>
          <Route path="/register" Component={Register}></Route>
          <Route path="/" Component={Home}></Route>
          <Route path="/login" Component={Login}></Route>

        </Routes>
      </div>
    </BrowserRouter>
  );
};
export default App;