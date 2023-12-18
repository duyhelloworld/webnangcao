// Components/Navigation.js

import React from 'react';
import { Link } from 'react-router-dom';


const Navigation = () => {

 return(
  <nav>
    <ul className="link-list">
    
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
  );
};

export default Navigation;