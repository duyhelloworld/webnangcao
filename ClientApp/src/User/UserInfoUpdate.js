import React, { useState, useEffect } from 'react';
import axios from 'axios';

const UserProfile = () => {

  const [user, setUser] = useState({
    id: null,
    userName: '',
    email: '',
    phoneNumber: '',
    fullName: '',
    avatar: '',
  });

  const [isEditing, setIsEditing] = useState(false);
  const [avatar, setAvatar] = useState(null);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setUser((prevUser) => ({
      ...prevUser,
      [name]: value,
    }));
  };

  const handleFileChange = (e) => {
    setAvatar(e.target.files[0]);
  };

  const fetchUserData = async () => {
    try {
      // const token = 'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOjQsInJvbGUiOiJVU0VSIiwidW5pcXVlX25hbWUiOiJkdXkiLCJlbWFpbCI6ImNvZGVkYW92b2lkdXlAZ21haWwuY29tIiwibmJmIjoxNzAyOTczNjgyLCJleHAiOjE3MDM4Mzc2ODIsImlhdCI6MTcwMjk3MzY4MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MjcxIn0.F8S7QiJsTt5It3iplVY_x6V9mqVhaZQ74zloWWy5-oomcLcA590PYHHUYNscSIex_Sbbn_zeaBCs7syH0PelXA';
      const token = localStorage.getItem('token');
      const response = await axios.get('http://localhost:5271/user/info', { headers: { Authorization: `Bearer ${token}` } });
      setUser(response.data);
    } catch (error) {
      console.error('Error fetching user data:', error);
    }
  };

  useEffect(() => {
    fetchUserData();
  }, []);

  const handleUpdate = async () => {
    try {
      // const token = 'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOjQsInJvbGUiOiJVU0VSIiwidW5pcXVlX25hbWUiOiJkdXkiLCJlbWFpbCI6ImNvZGVkYW92b2lkdXlAZ21haWwuY29tIiwibmJmIjoxNzAyOTczNjgyLCJleHAiOjE3MDM4Mzc2ODIsImlhdCI6MTcwMjk3MzY4MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MjcxIn0.F8S7QiJsTt5It3iplVY_x6V9mqVhaZQ74zloWWy5-oomcLcA590PYHHUYNscSIex_Sbbn_zeaBCs7syH0PelXA';
      const token = localStorage.getItem('token');
      // Gửi dữ liệu cập nhật lên server
      const formData = new FormData();
      formData.append("model", JSON.stringify({
        id: user.id,
        UserName: user.userName,
        Email: user.email,
        PhoneNumber: user.phoneNumber,
        FullName: user.fullName
      }
      ));
      formData.append("avatar", avatar);
      const response = await axios.put('http://localhost:5271/user/', formData, { headers: { Authorization: `Bearer ${token}` } });
      console.log('Update successful:', response.data);
      setIsEditing(false);
      fetchUserData();
    } catch (error) {
      alert(error.response.data);
      console.error('Error updating user:', error);
    }
  };

  useEffect(() => {
  }, []);

  return (
    <div className="bg-secondary text-light container">

      <h1>User Profile</h1>
      {isEditing ? (
        <div className="">

          <div>
            <div class="row mb-3">
              <label class="col-sm-2 col-form-label">Username:</label>
              <label class="col-sm-2 col-form-label">{user.userName}</label>
            </div>
            <div class="row mb-3">
              <label class="col-sm-2 col-form-label">Email: </label>
              <div class="col-sm-3">
                <input type="email" name="email" value={user.email} onChange={handleInputChange} class="form-control" id="inputEmail3" />
              </div>
            </div>
            <div class="row mb-3">
              <label class="col-sm-2 col-form-label">PhoneNumber: </label>
              <div class="col-sm-3">
                <input type="text" name="phoneNumber" value={user.phoneNumber} onChange={handleInputChange} class="form-control" id="inputEmail3" />
              </div>
            </div>
            <div class="row mb-3">
              <label class="col-sm-2 col-form-label">PhoneNumber: </label>
              <div class="col-sm-3">
                <input type="text" name="phoneNumber" value={user.phoneNumber} onChange={handleInputChange} class="form-control" id="inputEmail3" />
              </div>
            </div>
            <div class="row mb-3">
              <label class="col-sm-2 col-form-label">FullName: </label>
              <div class="col-sm-3">
                <input type="text" name="fullName" value={user.fullName} onChange={handleInputChange} class="form-control" id="inputEmail3" />
              </div>
            </div>
            <div class="row mb-3">
              <label class="col-sm-2 col-form-label">Avatar:</label>
              <div class="col-sm-3">
                <input type='file' name='artwork' onChange={handleFileChange} />
              </div>
            </div>
            {/* Thêm các trường thông tin khác cần cập nhật */}
            <button onClick={handleUpdate}>Update</button>
          </div>
        </div>

      ) : (
        <div>

          <p>ID: {user.id}</p>
          <p>Username: {user.userName}</p>
          <p>Email: {user.email}</p>
          <p>Phone Number: {user.phoneNumber}</p>
          <p>Full Name: {user.fullName}</p>
          <img src={`http://localhost:5271/user/avatar/${user.avatar}`} />
          {/* Hiển thị các trường thông tin khác */}
          <button className="btn btn-success" onClick={() => setIsEditing(true)}>Edit</button>
        </div>
      )}
    </div>
  );
};

export default UserProfile;
