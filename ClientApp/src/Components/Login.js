import React, { useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';

function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();
  
 
 
  const handleLogin = async () => {
    try {
     
      const response = await axios.post('http://localhost:5271/auth/signin', {
        username,
        password,
      });

      if (response.status === 200) {
        
        localStorage.setItem('token', response.data.data);
        console.log(response);
        navigate('/track/library');
      }
    } catch (error) {
      
      
      console.log(error);
    }
  };

  return (
    <div className="login-container">
  <div className="login-form">
    <h2 className="login-form__title">Sign In</h2>
    <input
      className="login-form__input"
      type="text"
      placeholder="Username"
      value={username}
      onChange={(e) => setUsername(e.target.value)}
    />
    <input
      className="login-form__input"
      type="password"
      placeholder="Password"
      value={password}
      onChange={(e) => setPassword(e.target.value)}
    />
    <button className="login-form__button" onClick={handleLogin}>
      Login
    </button>
  </div>
  <div className="No_account">
    <p align="center"><Link to="/register">No Account? Register here</Link></p> 
  </div>
</div>
  );
}


export default Login;


