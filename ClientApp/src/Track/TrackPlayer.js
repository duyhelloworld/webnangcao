import React, { useState, useEffect } from "react";
import axios from "axios";
import AudioPlayer from 'react-h5-audio-player';
import 'react-h5-audio-player/lib/styles.css';
import ViewComment from "../Comment/ViewComment";
import { useParams } from "react-router-dom";

const TrackPlayer = (props) => {
  const [trackData, setTrackData] = useState(null);
  const [loading, setLoading] = useState(true);
  const trackId = useParams().trackId;
  // const track = props.track;

  const Listen = async (trackId) => {
    const token = localStorage.getItem('token');
    try {
      const response = await axios.put(`http://localhost:5271/track/listen/${trackId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        }
      });
      if (response.status === 200) {
        return;
      }
    } catch (error) {
      console.error(error);
    }
  }

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(`http://localhost:5271/track/${trackId}`);
        setTrackData(response.data);
        setLoading(false);
      } catch (error) {
        if (error.response.status === 403) {
          alert(error.response.data);
          setLoading(false);
          console.log(error);
          return;
        }
        console.error("Error fetching track data", error);
        setLoading(false);
      }
    };

    fetchData();
  }, [trackId]);

  return (
    <div>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <div className="container-xl">
          <div>
            <div>
              <div className="py-4">
                {console.log(trackData)}
                <h1>{trackData.trackName}</h1>
                <h5>Trình bày: {trackData.description}</h5>
                <div className="py-2">
                  <div className="row">
                    <div className="col-2">
                      <p>Lượt nghe: {trackData.listenCount} listens</p>
                    </div>
                    <div className="col-2">
                      <p>Lượt thích: {trackData.likeCount} likes</p>
                    </div>
                  </div>
                  <p>{trackData.commentCount} comments</p>
                  <button className="btn btn-danger">Thích</button>
                </div>
              </div>

              <div>
                <AudioPlayer
                  //autoPlay
                  src={`http://localhost:5271/track/media/${trackData.fileName }`}
                  onPlay={e => Listen(trackData.id )}
                // other props here
                />
              </div>
            </div>
            <div>
              <ViewComment trackId={trackData.id} />
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default TrackPlayer;
