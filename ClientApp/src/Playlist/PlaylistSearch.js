// // PlaylistSearch.js
// import React, { useState } from "react";
// import axios from "axios";

// const PlaylistSearch = () => {
//   const [keyword, setKeyword] = useState("");
//   const [searchResults, setSearchResults] = useState([]);

//   const handleSearch = () => {
//     axios
//       .get(`http://localhost:5271/playlist/search?keyword=${keyword}`)
//       .then((response) => setSearchResults(response.data))
//       .catch((error) => console.error("Lỗi khi tìm kiếm playlist:", error));
//   };

//   return (
//     <div>
//       <input
//         type="text"
//         value={keyword}
//         onChange={(e) => setKeyword(e.target.value)}
//       />
//       <button onClick={handleSearch}>Tìm kiếm</button>

//       {Array.isArray(searchResults) && searchResults.length > 0 && (
//         <ul>
//           {searchResults.map((result) => (
//             <li key={result.id}>{result.playlistName}</li>
//           ))}
//         </ul>
//       )}
//     </div>
//   );
// };

// export default PlaylistSearch;

// PlaylistSearch.js
// ./src/Playlist/PlaylistSearch.js
import React, { useState } from "react";
import axios from "axios";

const PlaylistSearch = () => {
  // const [searchKeyword, setSearchKeyword] = useState("");
  const [searchResults, setSearchResults] = useState([]);
  const [keyword, setKeyword] = useState("");

  const handleSearch = () => {
    // Gọi API để tìm kiếm playlist
    var token = localStorage.getItem("token");
    axios
      .get(`http://localhost:5271/playlist/search?keyword=${keyword}`, {
        headers: { Authorization: "Bearer " + token },
      })
      .then((response) => setSearchResults(response.data))
      .catch((error) => console.error("Lỗi khi tìm kiếm playlist:", error));
  };

  return (
    <div>
      <input
        type="text"
        value={keyword}
        onChange={(e) => setKeyword(e.target.value)}
      />
      <button onClick={handleSearch}>Tìm kiếm</button>

      {Array.isArray(searchResults) && searchResults.length > 0 && (
        <ul>
          {searchResults.map((result) => (
            <li key={result.id}>{result.playlistName}</li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default PlaylistSearch;
