import React, { useEffect, useState, useContext } from 'react';
import { AuthContext } from './AuthContext';
import {jwtDecode} from 'jwt-decode';
import Playlist from './Playlist';

const Home = ({ children }) => {
  const [isAdmin, setIsAdmin] = useState(false);
  const { isLoggedIn } = useContext(AuthContext);

  useEffect(() => {
    const token = localStorage.getItem('token');
  
    if (token) {
      // Giải mã token để lấy thông tin từ payload
      const decodedToken = jwtDecode(token);

      // Lấy role từ payload của token
      const userRole = decodedToken.role;

      // Kiểm tra nếu role là 'ADMIN', set state cho isAdmin thành true
      setIsAdmin(userRole === 'ADMIN');
    }
  }, []);

  return (
  
    <div>
      {!isLoggedIn && <div><h1>Chưa đăng nhập</h1></div>}
      {isLoggedIn && (
        <div>
          {isAdmin ? (
            <Playlist/>
          ) : (
            <div><h1>Đây là giao diện của user</h1></div>
          )}
        </div>
      )}
    </div>
  );
}; 

export default Home;




