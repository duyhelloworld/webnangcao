import React, { useState } from 'react';

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
      console.log(response);// Hiển thị thông báo thành công
    } catch (error) {
      
      console.log(error);
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
      
    </div>
  );
}




export default Login;

