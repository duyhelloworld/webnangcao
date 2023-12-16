import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
//import Login from './Login';
import TrackComponent from './Track/ViewTrackAll'; 
import reportWebVitals from './reportWebVitals';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <TrackComponent />
  </React.StrictMode>
);
// server.js



// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
