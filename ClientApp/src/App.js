import React from 'react';
import {  Route, Routes, BrowserRouter } from "react-router-dom";
import Navigation from './Components/Navigation';
import Home from './Components/Home';
import Register from './Components/Register';
import Login from './Components/Login';
import GetTrack from './Components/Test';
import './Style/link.css';
const App = () => {
  return (
    <BrowserRouter>
      <div>
        <Navigation/>
        <Routes>
          <Route path="/register" Component={Register}></Route>
          <Route path="/" Component={Home}></Route>
          <Route path="/login" Component={Login}></Route>
          <Route path="/test" Component={GetTrack}></Route>
        </Routes>
      </div>
    </BrowserRouter>
  );
};
export default App;