// index.js
import React from "react";
import ReactDOM from "react-dom";
import PlaylistList from "./Playlist/PlaylistList";
import PlaylistSearch from "./Playlist/PlaylistSearch";

const App = () => {
  return (
    <div>
      <PlaylistList />
      <PlaylistSearch />
    </div>
  );
};

ReactDOM.render(<App />, document.getElementById("root"));
