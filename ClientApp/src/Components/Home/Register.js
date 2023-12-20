import React, { useState, useContext } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import '../../Style/register.css'; // Import file CSS
import { AuthContext } from './AuthContext';
import { Link } from 'react-router-dom';

const Register = () => {
  const navigate = useNavigate();
  const {isLoggedIn, setIsLoggedIn} = useContext(AuthContext);
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: ''
  });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post('http://localhost:5271/auth/signup', formData);
      console.log('Response:', response.data); // Xử lý kết quả từ API ở đây

      // Nếu thành công, chuyển hướng đến trang đăng nhập
      if (response.status === 200) {
        navigate('/home'); // Chuyển hướng đến trang đăng nhập
        setIsLoggedIn(true);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  return (
    <>
      {isLoggedIn ? (
        <p>Bạn đã đăng nhập rồi.</p>
      ) : (
        <form onSubmit={handleSubmit} className="register-container">
          <input
            type="text"
            placeholder="Username"
            name="username"
            value={formData.username}
            onChange={handleChange}
            className="register-input"
          />
          <input
            type="email"
            placeholder="Email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            className="register-input"
          />
          <input
            type="password"
            placeholder="Password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            className="register-input"
          />
          <button type="submit" className="register-button">Sign Up</button>
          <p align="center" className="No_account">
              <Link to="/login">Đã có tài khoản?</Link>
          </p>
        </form>
      )}
    </>
  );
  
};

export default Register;
