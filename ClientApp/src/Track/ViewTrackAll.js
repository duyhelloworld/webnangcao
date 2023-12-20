import React, { useState, useEffect } from 'react';
import axios from 'axios';
import SearchTrack from './SearchTrack';

function ViewTrackAll() {
  const [tracks, setTracks] = useState([]);
  const token = localStorage.getItem('token');

  useEffect(() => {
    const fetchTracks = async () => {
      try {
        // Gọi API để lấy danh sách bài hát với token
        const response = await axios.get('http://localhost:5271/track/library', {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        setTracks(response.data);
      } catch (error) {
        console.error('Error fetching tracks:', error);
      }
    };

    // Chỉ gọi fetchTracks khi có token
    if (token) {
      fetchTracks();
    }
  }, [token]);

  return (
    <div className="container">
  <header className="header py-3">
    <h1 className="text-center">Bài hát trong Track List</h1>
  </header>

  <SearchTrack />
  <ul className="list-group trackList">
    {tracks.map((track) => (
      <li key={track.id} className="list-group-item trackItem">
        <div className="trackDetails d-flex justify-content-between">
          <div>
            <h5 className="trackName">{track.trackName}</h5>
            <p className="author my-1">By: {track.author}</p>
            <p className="description my-1">{track.description}</p>
          </div>
          <div className="d-flex">
            <button className="btn btn-primary playButton">Play</button>
            <div className="trackCounts ms-3">
              <ul className="list-unstyled countsList">
                <li className="listenCount">Lượt nghe: {track.listenCount}</li>
                <li className="likeCount">Lượt thích: {track.likeCount}</li>
                <li className="commentCount">Comment: {track.commentCount}</li>
              </ul>
            </div>
          </div>
        </div>
      </li>
    ))}
  </ul>
</div>

  );
}

export default ViewTrackAll;
