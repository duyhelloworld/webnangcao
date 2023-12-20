import React, {useEffect, useState} from "react";
import axios from "axios";

const PlaylistDelete = ({ playlistId, onDelete }) => {
  const [confirmDelete, setConfirmDelete] = useState(false);
  const handleDelete = () => {
    var token = localStorage.getItem("token");
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
