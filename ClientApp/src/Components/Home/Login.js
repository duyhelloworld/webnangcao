import React, { useContext, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import '../../Style/login.css'

import {AuthContext} from './AuthContext';





  function Login()  {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();
    const {isLoggedIn, setIsLoggedIn} = useContext(AuthContext);
    

    
    
    const handleLogin = async () => {
      try {
        setError(''); // Reset error state before each login attempt
    
        if (!username || !password) {
          setError('Vui lòng nhập đủ tài khoản và mật khẩu!');
          return;
        }
    
        const response = await axios.post('http://localhost:5271/auth/signin', {
          username,
          password,
        });
    
        if (response.data && response.data.isSucceed) {
          // Các hành động khi đăng nhập thành công
          localStorage.setItem('token', response.data.data);
          navigate('/home');
          setIsLoggedIn(true);
          
        
        } else {
          // setError('Đăng nhập không thành công. Vui lòng thử lại sau.');
        }
      } catch (error) {
        if (error.response && error.response.status === 404) {
          setError('Không tìm thấy tài khoản');
        } else {
          setError('An error occurred. Please try again later.');
        }
        console.log(error);
      }
    };
    return (
      <div className="login-container">
        {isLoggedIn ? (
          <p align="center">You are already logged in!</p>
        ) : (
          <div className="login-form">
            <h2 className="login-form_title">Sign In</h2>
            <input
              className="login-form_input"
              type="text"
              placeholder="Username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
            <input
              className="login-form_input"
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <button className="login-form__button" onClick={handleLogin}>
              Login
            </button>
            {error && <p className="error-message">{error}</p>}
            <p align="center" className="No_account">
              <Link to="/register">No Account? Register here</Link>
            </p>
          </div>
        )}
      </div>
    );
    
  }

export default Login;
