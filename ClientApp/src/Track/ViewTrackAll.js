import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './style.css';

function TrackComponent() {
  const [tracks, setTracks] = useState([]);

  useEffect(() => {
    // Gọi API để lấy danh sách bài hát
    axios.get('http://localhost:5271/track/library')
      .then(response => {
        setTracks(response.data);
      })
      .catch(error => {
        console.error('Error fetching tracks:', error);
      });
  }, []); // useEffect chỉ chạy một lần khi component được render

  return (
    <div className="container">
      <header className="header">
        <h1>Track List</h1>
      </header>
      <ul className="trackList">
        {tracks.map(track => (
          <li key={track.id} className="trackItem">
            <div className="trackDetails">
              <p className="trackName">{track.trackName}</p>
              <p className="author">By: {track.author}</p>
              <p className="description">{track.description}</p>
              <button className="playButton">Play</button>
              <div className="trackCounts">
                <ul className="countsList">
                  <li className="listenCount">Lượt nghe: {track.listenCount}</li>
                  <li className="likeCount">Lượt thích: {track.likeCount}</li>
                  <li className="commentCount">Comment: {track.commentCount}</li>
                </ul>
              </div>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default TrackComponent;