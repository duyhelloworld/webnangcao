import React, { useEffect, useState, useContext } from 'react';
import { AuthContext } from './AuthContext';
import {jwtDecode} from 'jwt-decode';
import ViewTrackAll from '../../Track/ViewTrackAll';

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
  
    <div className='container-fluid'>
      {!isLoggedIn && <div><h1>Chưa đăng nhập</h1></div>}
      {isLoggedIn && (
        <div>
          {isAdmin ? (
            <h1>Đây là giao diện admin</h1>
          ) : (
            <div>
            <a className="btn btn-success view" href= '/playlist'>xem playlist</a>
            <ViewTrackAll/> 
           </div>
            
            
            
          )}
        </div>
      )}
    </div>
  );
}; 

export default Home;




