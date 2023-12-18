import React, { useState, useEffect } from 'react';
import axios from 'axios';

const UserInfo = () => {
  const [userData, setUserData] = useState(null);

  useEffect(() => {
    // Hàm để lấy thông tin người dùng khi component được mount
    var token = localStorage.getItem("token");

    const fetchUserData = async () => {
      try {
        const response = await axios.get('http://localhost:5271/user',{headers:{Authorization: "Bearer "+ token}}); 
        setUserData(response.data);
      } catch (error) {
        console.error('Error fetching user data:', error);
      }
    };

    fetchUserData();
  }, []); // [] đảm bảo rằng hàm useEffect chỉ chạy một lần khi component được mount

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
