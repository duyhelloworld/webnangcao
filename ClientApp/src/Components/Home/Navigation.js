import React, { useEffect, useContext } from 'react';
import { NavLink } from 'react-router-dom';
import '../../Style/link.css';
import Search from './Search';
import { Outlet } from "react-router-dom";
import { AuthContext } from './AuthContext';

const Navigation = () => {
  const { isLoggedIn, setIsLoggedIn } = useContext(AuthContext);

  useEffect(() => {
    const token = localStorage.getItem('token');

    if (token) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, [setIsLoggedIn]);

  const handleLogout = () => {
    localStorage.removeItem('token');
    setIsLoggedIn(false);
  };
  
  return (
    <>
      <nav>
        <ul className="link-list">
          <li>
            <NavLink to="/home">
              {' '}
              <img src="/logo192.png" alt="Home" className="nav-image" />
            </NavLink>
          </li>
          <li>
            <Search></Search>
          </li>
          
          <li className='login-logout'>
            {!isLoggedIn && <NavLink to="/login">Login</NavLink>}
            {isLoggedIn && (
              <button className='bttlogout' onClick={handleLogout}>Logout</button>
            )}
          </li>
         
        </ul>
      </nav>
      <div>
        <Outlet />
      </div>
    </>
  );
};

export default Navigation;
