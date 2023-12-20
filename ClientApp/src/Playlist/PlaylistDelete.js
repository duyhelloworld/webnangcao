import React, {useEffect, useState} from "react";
import axios from "axios";

const PlaylistDelete = ({ playlistId, onDelete }) => {
  const [confirmDelete, setConfirmDelete] = useState(false);
  const handleDelete = () => {
    var token =
      "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOjEsInJvbGUiOiJBRE1JTiIsInVuaXF1ZV9uYW1lIjoid2VibmFuZ2NhbyIsImVtYWlsIjoid2VibmFuZ2Nhb0BnbWFpbC5jb20iLCJuYmYiOjE3MDI4ODgzMjEsImV4cCI6MTcwMzc1MjMyMSwiaWF0IjoxNzAyODg4MzIxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUyNzEifQ.sECLGa03iYvonOaKS5AM7zCRkzjwBM9DnxG7pCpqizDL4iPtz9SUZPG5qQoRV27VNo477fjO6Q3fortPVRI0AA";
    axios
      .delete(`http://localhost:5271/playlist/${playlistId}`, {
        headers: { Authorization: "Bearer " + token },
      })
      .then(() => {
        // Gọi hàm onDelete để thông báo parent component về việc xóa thành công
        onDelete(playlistId);
        console.log(`Playlist with ID ${playlistId} deleted successfully.`);
        alert("Xóa thành công");
      })
      .catch((error) => {
        if (error.response.status === 401) alert("Người dùng chưa đăng nhập");
      });
  };

  useEffect(() => {
    
  })

  return (
    <div>
      {confirmDelete ? (
          <div>
            Ban muon xoa playlist nay khong?
          </div>
        ) : (
          <button onClick={handleDelete}>Delete Playlist</button>
      )}
    </div>
  );
};

export default PlaylistDelete;
