// PlaylistItem.js
import React from "react";

const PlaylistItem = ({ playlist, onEdit, onDelete }) => {
  return (
    <li key={playlist.id}>
      <h3>{playlist.playlistName}</h3>
      <p>Created at: {playlist.createdAt}</p>
      <p>Private: {playlist.isPrivate ? "Yes" : "No"}</p>
      <p>Likes: {playlist.likeCount}</p>
      <p>Reposts: {playlist.repostCount}</p>
      <p>Description: {playlist.description}</p>
      <p>Tags: {playlist.tags.join(", ")}</p>
      <button onClick={() => onEdit(playlist)}>Edit</button>
      <button onClick={() => onDelete(playlist.id)}>Delete</button>
    </li>
  );
};

export default PlaylistItem;
