import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './style.css';

const UserInfo = () => {
  const [userData, setUserData] = useState(null);

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.get(
          'http://localhost:5271/user',
          { headers: { Authorization: `Bearer ${token}` } },
        );
        setUserData(response.data);
      } catch (error) {
        console.error('Error fetching user data:', error);
      }
    };

    fetchUserData();
  }, []);

  const handleDeleteUser = async (userId) => {
    try {
      const token = localStorage.getItem('token');
      await axios.delete(`http://localhost:5271/user/${userId}`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      setUserData((previousData) =>
        previousData.filter((user) => user.id !== userId),
      );
      alert('User deleted successfully!');
    } catch (error) {
      console.error('Error deleting user:', error);
      alert('An error occurred while deleting the user.');
    }
  };

// 
const handleAddUser = async (e) => {
  e.preventDefault(); // Ngăn trình duyệt tải lại trang

  try {
    const token = localStorage.getItem('token');

    // Tạo formData object để chứa dữ liệu form
    const formData = new FormData();
    formData.append('userName', document.getElementById('userName').value);
    formData.append('email', document.getElementById('email').value);
    formData.append('phoneNumber', document.getElementById('phoneNumber').value);
    formData.append('fullName', document.getElementById('fullName').value);

    // Gửi yêu cầu POST với formData
    await axios.post(
      'http://localhost:5271/user',
      formData,
      { headers: { Authorization: `Bearer ${token}` } },
    );

    // Cập nhật state và hiển thị thông báo thành công
    setUserData((previousData) => [...previousData, formData]);
    alert('User added successfully!');
  } catch (error) {
    console.error('Error adding user:', error);
    alert('An error occurred while adding the user.');
  }
  };


  return (
    <div>
      {userData ? (
        userData.map((user) => (
          <div key={user.id}>
            <h1>User Information</h1>
            <p>ID: {user.id}</p>
            <p>Username: {user.userName}</p>
            <p>Email: {user.email}</p>
            <p>Full Name: {user.fullName}</p>
            {/* Thêm các thông tin khác nếu cần */}
            <img src={`http://localhost:5271/user/avatar/${user.avatar}`} alt="User Avatar" />
            <button onClick={() => handleDeleteUser(user.id)}>Delete</button>
          </div>
        ))
      ) : (
        <p>Loading ...</p>
      )}

        <div class = "form-add">
          <h1>Thêm User mới</h1>
          <form  onSubmit={handleAddUser}>
              <p>Username: </p>
              <input type="text" id="userName" placeholder="Username" />
              <p>Phone Number: </p>
              <input type="text" id="phoneNumber" placeholder="PhoneNumber" />
              <p>Email: </p>
              <input type="email" id="email" placeholder="Email" />
              <p>Full Name: </p>
              <input type="text" id="fullName" placeholder="Full Name" />
              <br/><br/>
              <button class="btn-add" type="submit">Add User</button>
          </form>
        </div>
    </div>
  );
};

export default UserInfo;
