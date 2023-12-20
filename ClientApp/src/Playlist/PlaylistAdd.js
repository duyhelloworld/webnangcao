import React, { useState } from "react";
import Axios from "axios";

const PlaylistAdd = () => {
  var jwt =
    "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOjUsInJvbGUiOiJVU0VSIiwidW5pcXVlX25hbWUiOiJxdWFuZyIsImVtYWlsIjoibWFpbDFAZ21haWwuY29tIiwibmJmIjoxNzAyODkyNTc5LCJleHAiOjE3MDM3NTY1NzksImlhdCI6MTcwMjg5MjU3OSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MjcxIn0.QUjWdCvi-XHOV7cxidMQqAPOpSc2IUcnDqCL-LbDPlmGml9SYunirF41q5VWLh-BDNk_CRx046ryykrwu7p08g";
  const [name, setName] = useState("");
  const [isPrivate, setIsPrivate] = useState(false);
  const [trackIds] = useState([1, 2, 5, 6]);
  const [artwork, setArtwork] = useState(null);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append(
      "model",
      JSON.stringify({
        Name: name,
        IsPrivate: isPrivate,
        TrackIds: trackIds,
      })
    );
    formData.append("artwork", artwork);

    await Axios.post("http://localhost:5271/playlist", formData, {
      headers: {
        Authorization: `Bearer ${jwt}`,
      },
    });

    alert("Thêm playlist thành công!");
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        placeholder="Tên playlist"
        onChange={(e) => setName(e.target.value)}
        value={name}
      />
      <input
        type="checkbox"
        checked={isPrivate}
        onChange={(e) => setIsPrivate(e.target.checked)}
      />
      <input
        type="file"
        id="artwork"
        onChange={(e) => setArtwork(e.target.files[0])}
      />
      <button type="submit">Thêm</button>
    </form>
  );
};

export default PlaylistAdd;
