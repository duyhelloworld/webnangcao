import React, { useEffect, useState } from 'react';
import axios from 'axios';

// Tạo một component tên là Home
const GetTrack = () => {
  // Khởi tạo state cho dữ liệu, loading, và error
  const [data, setData] = useState([]);

  const [error, setError] = useState(null);

  // Sử dụng useEffect hook để gọi API khi component được render lần đầu tiên
  useEffect(() => {
    
    // Gọi API bằng axios
    var token = localStorage.getItem("token");
   
    // 
    axios
      .get(' http://localhost:5271/track/all/1',{headers:{Authorization: "Bearer "+ token}})
      .then((response) => {
        // Nếu thành công, lưu dữ liệu vào state
        setData(response.data);
   
   
      })
      .catch((error) => {
        // Nếu có lỗi, lưu lỗi vào state
        setError(error);
        console.log(error);
       
     
      });
  }, []); // Mảng rỗng để chỉ chạy một lần

  // Trả về giao diện người dùng cho component
  return (
    <div>
      <h1>Home</h1>
     
      {/* Hiển thị thông báo lỗi nếu có lỗi */}
      {error && (
        <div>
        {error.response.data.reason}
        </div>
        
        )}
      {/* Hiển thị danh sách dữ liệu nếu có dữ liệu */}
      {data && (
        <ul>
          {data.map((item) => (
            // Sử dụng key prop để định danh duy nhất cho mỗi component con
            <li key={item.id}>
              {/* Hiển thị thông tin của mỗi item theo ý bạn */}
              {item.trackName}: {item.author}:
            
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default GetTrack;

