import React, { useState, useEffect } from 'react';
import axios from 'axios';

const UserInfo = () => {
  const [userData, setUserData] = useState(null);

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        // var token = localStorage.getItem('token');
        var token = 'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOjEsInJvbGUiOiJBRE1JTiIsInVuaXF1ZV9uYW1lIjoid2VibmFuZ2NhbyIsImVtYWlsIjoid2VibmFuZ2Nhb0BnbWFpbC5jb20iLCJuYmYiOjE3MDIzOTExOTcsImV4cCI6MTcwMzI1NTE5NywiaWF0IjoxNzAyMzkxMTk3LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUyNzEifQ.4WERuD9cSm1wQ9rX_td1bfcDlUUPFlYj-T4pXZi0hJgPbbeqyW7RZGx66VUfmiXTsK3MBLVFj-G0r4um3R11TA';
        const response = await axios.get(
          'http://localhost:5271/user/1',
          { headers: { Authorization: `Bearer ${token}` }}
        ); 
        setUserData(response.data);
      } catch (error) {
        console.error('Error fetching user data:', error);
      }
    };

    fetchUserData();
  }, []);

  return (
    <div>
      {userData ? (
        // Nếu có dữ liệu người dùng, hiển thị thông tin
        <div>
          <h1>User Information</h1>
          <p>ID: {userData.id}</p>
          <p>Username: {userData.userName}</p>
          <p>Email: {userData.email}</p>
          <p>Full Name: {userData.fullName}</p>
          {/* Thêm các thông tin khác nếu cần */}
          <img src={userData.avatar} alt="User Avatar" />
        </div>
      ) : (
        // Nếu không có dữ liệu, có thể hiển thị thông báo hoặc spinner
        <p>Loading ...</p>
      )}
    </div>
  );
};

export default UserInfo;
