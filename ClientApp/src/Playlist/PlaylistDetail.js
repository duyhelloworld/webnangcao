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
    </div>
  );
};

export default PlaylistDetail;
