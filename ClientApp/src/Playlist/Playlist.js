// // App.js
// import React from "react";
// import PlaylistList from "./PlaylistList";

// const Playlist = () => {
//   return (
//     <div>
//       <h1>My Playlist App</h1>
//       <PlaylistList />
//     </div>
//   );
// };

// export default Playlist;

// Playlist.js
import React, { useState, useEffect } from "react";
import axios from "axios";
import PlaylistItem from "./PlaylistItem";

const Playlist = () => {
  const [playlists, setPlaylists] = useState([]);

  useEffect(() => {
    // Gọi API để lấy danh sách playlist
    var token = localStorage.getItem("token");
    axios
      .get("http://localhost:5271/playlist/all/1", {
        headers: { Authorization: "Bearer" + token },
      })
      .then((response) => setPlaylists(response.data))
      .catch((error) =>
        console.error("Lỗi khi lấy danh sách playlist:", error)
      );
  }, []);

  const handleEditPlaylist = (playlist) => {
    // Xử lý sự kiện sửa playlist
    console.log("Edit playlist:", playlist);
  };

  const handleDeletePlaylist = (playlistId) => {
    // Xử lý sự kiện xóa playlist
    console.log("Delete playlist with ID:", playlistId);
  };

  return (
    <div>
      <h2>Playlists</h2>
      <ul>
        {playlists.map((playlist) => (
          <PlaylistItem
            key={playlist.id}
            playlist={playlist}
            onEdit={handleEditPlaylist}
            onDelete={handleDeletePlaylist}
          />
        ))}
      </ul>
    </div>
  );
};

export default Playlist;
