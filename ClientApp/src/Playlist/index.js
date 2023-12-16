import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import PlaylistComponent from "./Playlist/Playlist"; // Sửa lại đường dẫn import
// import reportWebVitals from "./reportWebVitals";

const root = document.getElementById("root");

// Render PlaylistComponent component with specific props (page and userType)
ReactDOM.createRoot(document.getElementById("playlist-root")).render(
  <React.StrictMode>
    <PlaylistComponent page={1} userType="admin" />
  </React.StrictMode>
);

// Nếu bạn muốn đo lường hiệu suất trong ứng dụng của mình, hãy truyền một hàm
// để ghi lại kết quả (ví dụ: reportWebVitals(console.log))
// hoặc gửi đến một điểm cuộc phân tích. Tìm hiểu thêm tại: https://bit.ly/CRA-vitals
// reportWebVitals();
