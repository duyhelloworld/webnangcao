// PlaylistDelete.js

import React from "react";
import axios from "axios";

const PlaylistDelete = ({ playlistId, onDelete }) => {
  const handleDelete = () => {
    // Gọi API để xóa playlist
    // var token = localStorage.getItem("token");
    var token =
      "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOjEsInJvbGUiOiJBRE1JTiIsInVuaXF1ZV9uYW1lIjoid2VibmFuZ2NhbyIsImVtYWlsIjoid2VibmFuZ2Nhb0BnbWFpbC5jb20iLCJuYmYiOjE3MDI4ODgzMjEsImV4cCI6MTcwMzc1MjMyMSwiaWF0IjoxNzAyODg4MzIxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUyNzEifQ.sECLGa03iYvonOaKS5AM7zCRkzjwBM9DnxG7pCpqizDL4iPtz9SUZPG5qQoRV27VNo477fjO6Q3fortPVRI0AA";
    axios
      .delete(`http://localhost:5271/playlist/user/${playlistId}`, {
        headers: { Authorization: "Bearer " + token },
      })
      .then(() => {
        // Gọi hàm onDelete để thông báo parent component về việc xóa thành công
        onDelete(playlistId);
        console.log(`Playlist with ID ${playlistId} deleted successfully.`);
      })
      .catch((error) => {
        if (error.response.status == 401) alert("Người dùn chưa đăng nhập");
      });
  };

  return <button onClick={handleDelete}>Delete Playlist</button>;
};

export default PlaylistDelete;
