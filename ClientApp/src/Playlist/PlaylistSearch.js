// PlaylistSearch
import React, { useState } from "react";
import PlaylistService from "./PlaylistService";

const PlaylistSearch = () => {
  const [searchResults, setSearchResults] = useState([]);
  const [keyword, setKeyword] = useState("");

  const handleSearch = async () => {
    try {
      const results = await PlaylistService.search(keyword);
      setSearchResults(results);
    } catch (error) {
      console.error("Error searching playlists:", error);
    }
  };

  return (
    <div>
      <input
        type="text"
        value={keyword}
        onChange={(e) => setKeyword(e.target.value)}
      />
      <button onClick={handleSearch}>Search</button>

      <ul>
        {searchResults.map((result) => (
          <li key={result.id}>{result.name}</li>
        ))}
      </ul>
    </div>
  );
};

export default PlaylistSearch;
