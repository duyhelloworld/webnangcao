import React, { useState } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios';

function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async () => {
    try {
      // eslint-disable-next-line
      const response = await axios.post('http://localhost:5271/auth/signin', {
        username,
        password,
      });
      toast.success('Login successful'); // Hiển thị thông báo thành công
    } catch (error) {
      console.error('Error logging in:', error);
      toast.error('Login failed'); // Hiển thị thông báo thất bại
    }
  };

  return (
    <div>
      <input
        type="text"
        placeholder="Username"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />
      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button onClick={handleLogin}>Login</button>
      <ToastContainer /> {}
    </div>
  );
}




export default Login;

