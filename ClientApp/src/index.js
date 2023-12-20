// index.js
import React from "react";
import { createRoot } from "react-dom/client";
import Playlist from "./Playlist/Playlist";
import PlaylistSearch from "./Playlist/PlaylistSearch";
const App = () => {
  return (
    <div>
      <Playlist />
      <PlaylistSearch />
    </div>
  );
};

createRoot(document.getElementById("root")).render(<App />);
