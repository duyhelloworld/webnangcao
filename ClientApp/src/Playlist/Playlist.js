import React, { useState, useEffect } from "react";
import axios from "axios";
import PlaylistEdit from "./PlaylistEdit";
import PlaylistDelete from "./PlaylistDelete";
import "./PlaylistList.css"
const Playlist = () => {
  const [playlists, setPlaylists] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      var token = localStorage.getItem("token");
    try {
      var response = await axios.get("http://localhost:5271/playlist/all/1", { headers: { Authorization: "Bearer" + token }});
        setPlaylists(response.data);
    } catch (error) {
      console.error("Lỗi khi lấy danh sách playlist:", error)
    }}

    fetchData();
  }, []);

  const handleEditPlaylist = (newPlaylist) => {
    setPlaylists((prevPlaylist) => [...prevPlaylist, newPlaylist]);
  }

  const handleDeletePlaylist = (playlistId) => {
    setPlaylists((prevPlaylist) => prevPlaylist.filter(p => p.id !== playlistId));
  }

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
            <p>Tags: {playlist.tags}</p>
            <PlaylistEdit playlist={playlist} onEdit={handleEditPlaylist} />
            <PlaylistDelete playlistId={playlist.id} onDelete={handleDeletePlaylist} />
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Playlist;
