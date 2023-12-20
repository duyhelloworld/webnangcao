import React, { useState } from "react";
import axios from "axios";

const PlaylistRepost = ({ playlistId, token }) => {
  const [reposted, setReposted] = useState(false);
  const [error, setError] = useState(null);

  const handleRepostPlaylist = () => {
    // Kiểm tra trạng thái đã repost để tránh gửi yêu cầu thừa
    if (!reposted) {
      axios
        .post(`http://localhost:5271/playlist/${playlistId}/repost`, null, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then(() => {
          console.log("Playlist repost thành công!");
          setReposted(true);
        })
        .catch((error) => {
          console.error("Lỗi khi repost playlist:", error);
          setError("Không thể thực hiện repost playlist.");
        });
    }
  };

  return (
    <div>
      <h2>Playlist Repost</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}
      {!reposted ? (
        <button type="button" onClick={handleRepostPlaylist}>
          Repost Playlist
        </button>
      ) : (
        <p>Playlist đã được repost.</p>
      )}
    </div>
  );
};

export default PlaylistRepost;
