import React from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import Register from './Components/Register';
import Login from './Components/Login';

import { BrowserRouter, Route, Routes } from 'react-router-dom';

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route exact path="/" element={<h1>Home</h1>} />
        <Route exact path="/auth/signup" element={Register} />
        <Route path="/auth/signin" element={Login} />
      </Routes>
    </BrowserRouter>
  );
};

createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
// server.js