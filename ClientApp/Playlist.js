// PlaylistComponent
import React, { useState, useEffect } from "react";
import PlaylistService from "./PlaylistService";

// import { PlaylistComponent } from "./Playlist/PlaylistComponent";

const PlaylistComponent = ({ page, userType }) => {
  const [playlist, setPlaylist] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        let playlistData;

        if (userType === "admin") {
          playlistData = await PlaylistService.getAdminPlaylists();
        } else if (userType === "user") {
          playlistData = await PlaylistService.getUserPlaylists();
        } else {
          playlistData = await PlaylistService.getPlaylist(page);
        }

        setPlaylist(playlistData);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [page, userType]);

  return (
    <div>
      <h1>Danh sách phát</h1>
      {loading && <p>Loading...</p>}
      {error && <p>Error: {error}</p>}
      <ul>
        {playlist.map((item) => (
          <li key={item.id}>{item.name}</li>
        ))}
      </ul>
    </div>
  );
};

export default PlaylistComponent;
