import React, { useState, useEffect } from "react";
import axios from "axios";
import AudioPlayer from 'react-h5-audio-player';
import 'react-h5-audio-player/lib/styles.css';
import './style.css';

const Player = () => {
  const [trackData, setTrackData] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get("http://localhost:5271/track/2");
        setTrackData(response.data);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching track data", error);
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  return (
    <div>
            {loading ? (
                <p>Loading...</p>
            ) : (
                <body>
                <div>    
                    
                    <h1>{trackData.trackName}</h1>
                    <p>{trackData.description}</p>
                    <p>Lượt nghe: {trackData.listenCount} listens</p>
                    <p>{trackData.commentCount} comments</p>
                    <button>Thích</button>
                    <button>Bình luận</button>
      
                    <div class="play-music">     
                        <AudioPlayer
                            //autoPlay
                            src={`http://localhost:5271/track/media/${trackData.fileName}`}
                            onPlay={e => console.log("onPlay")}
                            // other props here
                        />
                    </div>
                </div>
                </body> 
            )}    
    </div>
  );
};

export default Player;
