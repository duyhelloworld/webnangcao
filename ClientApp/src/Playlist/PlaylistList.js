// // PlaylistList.js
// import React, { useState, useEffect } from "react";
// import axios from "axios";

// const PlaylistList = () => {
//   const [playlists, setPlaylists] = useState([]);

//   useEffect(() => {
//     // Gọi API để lấy danh sách playlist
//     var token = localStorage.getItem("token");
//     axios
//       .get("http://localhost:5271/playlist/all/1", {
//         headers: { Authorization: "Bearer" + token },
//       })

//       .then((response) => setPlaylists(response.data))
//       .catch((error) =>
//         console.error("Lỗi khi lấy danh sách playlist:", error)
//       );
//   }, []);

//   return (
//     <div>
//       <h2>Playlists</h2>
//       <ul>
//         {playlists.map((playlist) => (
//           <li key={playlist.id}>
//             <h3>{playlist.playlistName}</h3>
//             <p>Created at: {playlist.createdAt}</p>
//             <p>Private: {playlist.isPrivate ? "Yes" : "No"}</p>
//             <p>Likes: {playlist.likeCount}</p>
//             <p>Reposts: {playlist.repostCount}</p>
//             <p>Description: {playlist.description}</p>
//             <p>Tags: {playlist.tags.join(", ")}</p>
//             {/* Thêm các thông tin khác tùy thuộc vào cấu trúc dữ liệu */}
//           </li>
//         ))}
//       </ul>
//     </div>
//   );
// };

// export default PlaylistList;

//1

// ./src/Playlist/PlaylistList.js
// import React, { useState, useEffect } from "react";
// import axios from "axios";

// const PlaylistList = () => {
//   const [playlists, setPlaylists] = useState([]);
//   const [newPlaylist, setNewPlaylist] = useState({
//     playlistName: "",
//     description: "",
//     // Thêm các trường dữ liệu khác nếu cần
//   });

//   useEffect(() => {
//     // Gọi API để lấy danh sách playlist
//     var token = localStorage.getItem("token");
//     axios
//       .get("http://localhost:5271/playlist/all/1", {
//         headers: { Authorization: "Bearer " + token },
//       })
//       .then((response) => setPlaylists(response.data))
//       .catch((error) =>
//         console.error("Lỗi khi lấy danh sách playlist:", error)
//       );
//   }, []);

//   const handleAddPlaylist = () => {
//     // Gọi API để thêm playlist mới
//     var token = localStorage.getItem("token");
//     axios
//       .post("http://localhost:5271/playlist", newPlaylist, {
//         headers: { Authorization: "Bearer " + token },
//       })
//       .then((response) => {
//         // Cập nhật danh sách playlist sau khi thêm thành công
//         setPlaylists([...playlists, response.data]);
//         // Đặt lại giá trị cho newPlaylist để chuẩn bị thêm playlist mới
//         setNewPlaylist({
//           playlistName: "",
//           description: "",
//           // Đặt lại giá trị cho các trường dữ liệu khác nếu cần
//         });
//       })
//       .catch((error) => console.error("Lỗi khi thêm playlist:", error));
//   };

//   const handleEditPlaylist = (editedPlaylist) => {
//     // Gọi API để cập nhật thông tin playlist
//     var token = localStorage.getItem("token");
//     axios
//       .put(
//         `http://localhost:5271/playlist/${editedPlaylist.id}/information`,
//         editedPlaylist,
//         {
//           headers: { Authorization: "Bearer " + token },
//         }
//       )
//       .then(() => {
//         // Cập nhật danh sách playlist sau khi cập nhật thành công
//         const updatedPlaylists = playlists.map((playlist) =>
//           playlist.id === editedPlaylist.id ? editedPlaylist : playlist
//         );
//         setPlaylists(updatedPlaylists);
//       })
//       .catch((error) =>
//         console.error("Lỗi khi cập nhật thông tin playlist:", error)
//       );
//   };

//   const handleDeletePlaylist = (playlistId) => {
//     // Gọi API để xóa playlist
//     var token = localStorage.getItem("token");
//     axios
//       .delete(`http://localhost:5271/playlist/user/${playlistId}`, {
//         headers: { Authorization: "Bearer " + token },
//       })
//       .then(() => {
//         // Cập nhật danh sách playlist sau khi xóa thành công
//         const updatedPlaylists = playlists.filter(
//           (playlist) => playlist.id !== playlistId
//         );
//         setPlaylists(updatedPlaylists);
//       })
//       .catch((error) => console.error("Lỗi khi xóa playlist:", error));
//   };

//   return (
//     <div>
//       <h2>Playlists</h2>
//       <ul>
//         {playlists.map((playlist) => (
//           <li key={playlist.id}>
//             <h3>{playlist.playlistName}</h3>
//             <p>Created at: {playlist.createdAt}</p>
//             <p>Private: {playlist.isPrivate ? "Yes" : "No"}</p>
//             <p>Likes: {playlist.likeCount}</p>
//             <p>Reposts: {playlist.repostCount}</p>
//             <p>Description: {playlist.description}</p>
//             <p>Tags: {playlist.tags.join(", ")}</p>
//             <button onClick={() => handleEditPlaylist(playlist)}>
//               Edit Playlist
//             </button>
//             <button onClick={() => handleDeletePlaylist(playlist.id)}>
//               Delete Playlist
//             </button>
//           </li>
//         ))}
//       </ul>

//       <h2>Add New Playlist</h2>
//       <div>
//         <label>Playlist Name:</label>
//         <input
//           type="text"
//           value={newPlaylist.playlistName}
//           onChange={(e) =>
//             setNewPlaylist({ ...newPlaylist, playlistName: e.target.value })
//           }
//         />
//       </div>
//       <div>
//         <label>Description:</label>
//         <input
//           type="text"
//           value={newPlaylist.description}
//           onChange={(e) =>
//             setNewPlaylist({ ...newPlaylist, description: e.target.value })
//           }
//         />
//       </div>
//       {/* Thêm các trường dữ liệu khác nếu cần */}
//       <button onClick={handleAddPlaylist}>Add Playlist</button>
//     </div>
//   );
// };

// export default PlaylistList;

//2

// PlaylistList.js

import React, { useState, useEffect } from "react";
import axios from "axios";
import "./PlaylistList.css";
import PlaylistDelete from "./PlaylistDelete";

const PlaylistList = () => {
  const [playlists, setPlaylists] = useState([]);
  const [newPlaylist, setNewPlaylist] = useState({
    playlistName: "",
    description: "",
    // Thêm các trường dữ liệu khác nếu cần
  });

  useEffect(() => {
    // Gọi API để lấy danh sách playlist
    var token = localStorage.getItem("token");
    axios
      .get("http://localhost:5271/playlist/all/1", {
        headers: { Authorization: "Bearer " + token },
      })
      .then((response) => setPlaylists(response.data))
      .catch((error) =>
        console.error("Lỗi khi lấy danh sách playlist:", error)
      );
  }, []);

  const handleAddPlaylist = () => {
    // Gọi API để thêm playlist mới
    var token = localStorage.getItem("token");
    axios
      .post("http://localhost:5271/playlist", newPlaylist, {
        headers: { Authorization: "Bearer " + token },
      })
      .then((response) => {
        // Cập nhật danh sách playlist sau khi thêm thành công
        setPlaylists([...playlists, response.data]);
        // Đặt lại giá trị cho newPlaylist để chuẩn bị thêm playlist mới
        setNewPlaylist({
          playlistName: "",
          description: "",
          // Đặt lại giá trị cho các trường dữ liệu khác nếu cần
        });
      })
      .catch((error) => console.error("Lỗi khi thêm playlist:", error));
  };

  const handleEditPlaylist = (editedPlaylist) => {
    // Gọi API để cập nhật thông tin playlist
    var token = localStorage.getItem("token");
    axios
      .put(
        `http://localhost:5271/playlist/${editedPlaylist.id}/information`,
        editedPlaylist,
        {
          headers: { Authorization: "Bearer " + token },
        }
      )
      .then(() => {
        // Cập nhật danh sách playlist sau khi cập nhật thành công
        const updatedPlaylists = playlists.map((playlist) =>
          playlist.id === editedPlaylist.id ? editedPlaylist : playlist
        );
        setPlaylists(updatedPlaylists);
      })
      .catch((error) =>
        console.error("Lỗi khi cập nhật thông tin playlist:", error)
      );
  };

  const handleDeletePlaylist = (deletedPlaylistId) => {
    // Gọi API để xóa playlist
    var token = localStorage.getItem("token");
    axios
      .delete(`http://localhost:5271/playlist/user/${deletedPlaylistId}`, {
        headers: { Authorization: "Bearer " + token },
      })
      .then(() => {
        // Cập nhật danh sách playlist sau khi xóa thành công
        const updatedPlaylists = playlists.filter(
          (playlist) => playlist.id !== deletedPlaylistId
        );
        setPlaylists(updatedPlaylists);
      })
      .catch((error) => console.error("Lỗi khi xóa playlist:", error));
  };

  return (
    <div>
      <h2>Playlists</h2>
      <ul>
        {playlists.map((playlist) => (
          <li key={playlist.id}>
            <h3>{playlist.playlistName}</h3>
            <p>Created at: {playlist.createdAt}</p>
            <p>Private: {playlist.isPrivate ? "Yes" : "No"}</p>
            <p>Likes: {playlist.likeCount}</p>
            <p>Reposts: {playlist.repostCount}</p>
            <p>Description: {playlist.description}</p>
            <p>Tags: {playlist.tags.join(", ")}</p>
            <button onClick={() => handleEditPlaylist(playlist)}>
              Edit Playlist
            </button>
            <PlaylistDelete
              playlistId={playlist.id}
              onDelete={handleDeletePlaylist}
            />
          </li>
        ))}
      </ul>

      <h2>Add New Playlist</h2>
      <div>
        <label>Playlist Name:</label>
        <input
          type="text"
          value={newPlaylist.playlistName}
          onChange={(e) =>
            setNewPlaylist({ ...newPlaylist, playlistName: e.target.value })
          }
        />
      </div>
      <div>
        <label>Description:</label>
        <input
          type="text"
          value={newPlaylist.description}
          onChange={(e) =>
            setNewPlaylist({ ...newPlaylist, description: e.target.value })
          }
        />
      </div>
      {/* Thêm các trường dữ liệu khác nếu cần */}
      <button onClick={handleAddPlaylist}>Add Playlist</button>
    </div>
  );
};

export default PlaylistList;
