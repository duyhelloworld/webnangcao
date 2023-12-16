import React, { useEffect, useState } from 'react';
import axios from 'axios';

function PlaylistComponent() {
  const [playlists, setPlaylists] = useState([]);

  useEffect(() => {
    const apiUrl = 'http://localhost:5271/playlist?page=1';

    // Hàm để lấy và cập nhật thông tin playlist
    const getPlaylists = async () => {
      try {
        // Gửi yêu cầu GET đến API
        const response = await axios.get(apiUrl);

        // Lấy dữ liệu từ phản hồi
        const fetchedPlaylists = response.data;

        // Cập nhật state với thông tin playlist
        setPlaylists(fetchedPlaylists);
      } catch (error) {
        // Xử lý lỗi nếu có
        console.error('Error fetching playlists:', error.message);
      }
    };

    // Gọi hàm để lấy và cập nhật thông tin playlist
    getPlaylists();
  }, []); // Dependency array để đảm bảo useEffect chỉ chạy một lần khi component được render

  return (
    <div>
      <h1>Playlists</h1>
      <ul>
        {playlists.map((playlist) => (
          <li key={playlist.id}>
            <strong>ID:</strong> {playlist.id}<br />
            <strong>Playlist Name:</strong> {playlist.playlistName}<br />
            <strong>Author:</strong> {playlist.authorName}<br />
            <strong>Description:</strong> {playlist.description}<br />
            <strong>Tags:</strong> {playlist.tags.join(', ')}<br />
            <hr />
          </li>
        ))}
      </ul>
    </div>
  );
}

export default PlaylistComponent;
