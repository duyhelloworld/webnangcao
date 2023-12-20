import React, { useState, useEffect } from "react";
// import axios from "axios";

const PlaylistEdit = ({ playlist, onEdit }) => {
  const [isEditing, setIsEditing] = useState(false);
  const [editedPlaylist, setEditedPlaylist] = useState({
    playlistName: "",
    createdAt: "",
    isPrivate: false,
    likeCount: 0,
    repostCount: 0,
    description: "", // Đảm bảo description không phải là null
    tags: [],
  });

  useEffect(() => {
    // Update state with playlist data when it's available
    if (playlist) {
      // Đảm bảo giá trị description không phải là null
      setEditedPlaylist({
        ...playlist,
        description: playlist.description || "", // Nếu là null, sẽ được thay thế bằng chuỗi rỗng
      });
    }
  }, [playlist]);

  const handleInputChange = (e) => {
    // Update the editedPlaylist state as the user inputs data
    setEditedPlaylist({
      ...editedPlaylist,
      [e.target.name]:
        e.target.type === "checkbox"
          ? e.target.checked
          : e.target.name === "tags"
          ? e.target.value.split(", ")
          : e.target.value,
    });
  };

  const handleSaveChanges = () => {
    // Pass the editedPlaylist data to the parent component for updating
    onEdit(editedPlaylist);
    setIsEditing(false);
  };

  return (
    <div>
      {isEditing ? (
        <form>
          <label>
            Playlist Name:
            <input
              type="text"
              name="playlistName"
              value={editedPlaylist.playlistName}
              onChange={handleInputChange}
            />
          </label>
          <br />
          {/* Add other input fields for createdAt, isPrivate, likeCount, repostCount, etc. */}
          <label>
            Description:
            <textarea
              name="description"
              value={editedPlaylist.description || ""}
              // Đảm bảo giá trị không phải là null
              onChange={handleInputChange}
            />
          </label>
          <br />
          <label>
            Tags:
            <input
              type="text"
              name="tags"
              value={editedPlaylist.tags.join(", ")}
              onChange={handleInputChange}
            />
          </label>
          <br />
          <button type="button" onClick={handleSaveChanges}>
            Save Changes
          </button>
        </form>
      ) : (
        <button onClick={handleSaveChanges}>Edit</button>
      )}
    </div>
  );
};

export default PlaylistEdit;
