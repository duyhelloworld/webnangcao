import React, { useState, useEffect } from "react";
import axios from "axios";
import AudioPlayer from 'react-h5-audio-player';
import 'react-h5-audio-player/lib/styles.css';

const Player = () => {
  const [trackData, setTrackData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get("http://localhost:5271/track/4");
        setTrackData(response.data);
        setLoading(false);
      } catch (error) {
        setError(error.response.data);
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  return (
    <div>
      <div>
        {error ? (
          <p className={"loi-bootstrap"}>{error}</p>
        ) : loading ? (
          <p>Loading ...</p>
        ) : (
          <div>
            <p>{trackData.trackName}</p>
            <AudioPlayer
              //autoPlay
              src={`http://localhost:5271/track/media/${trackData.fileName}`}
            />
          </div>
        )}
      </div>
    </div>
  );
};

export default Player;
