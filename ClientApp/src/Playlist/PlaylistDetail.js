// // PlaylistDetail.js
// import React, { useState, useEffect } from "react";
// import axios from "axios";

// const PlaylistDetail = ({ playlistId }) => {
//   const [playlistDetail, setPlaylistDetail] = useState(null);

//   useEffect(() => {
//     if (playlistId) {
//       axios
//         .get(`http://localhost:5271/playlist/${playlistId}`)
//         .then((response) => setPlaylistDetail(response.data))
//         .catch((error) =>
//           console.error("Lỗi khi lấy chi tiết playlist:", error)
//         );
//     }
//   }, [playlistId]);

//   return (
//     <div>
//       {playlistDetail && (
//         <div>
//           <h2>{playlistDetail.playlistName}</h2>
//           <p>{playlistDetail.description}</p>
//           {/* Hiển thị các thông tin khác của playlist */}
//         </div>
//       )}
//     </div>
//   );
// };

// export default PlaylistDetail;

// PlaylistDetail.js
import React from "react";

const PlaylistDetail = ({ playlist }) => {
  if (!playlist) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <h2>{playlist.playlistName}</h2>
      <p>Created at: {playlist.createdAt}</p>
      <p>Private: {playlist.isPrivate ? "Yes" : "No"}</p>
      <p>Likes: {playlist.likeCount}</p>
      <p>Reposts: {playlist.repostCount}</p>
      <p>Description: {playlist.description}</p>
      <p>Tags: {playlist.tags.join(", ")}</p>
      {/* Add other details based on your data structure */}
    </div>
  );
};

export default PlaylistDetail;
