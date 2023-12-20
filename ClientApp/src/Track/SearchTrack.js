import React, { useState } from 'react';
import axios from 'axios';

function SearchTrack() {
    const [searchTerm, setSearchTerm] = useState('');
    const [searchResults, setSearchResults] = useState([]);

    const handleSearch = async () => {
        try {
            // Gọi API để tìm kiếm bài hát với token
            const token = localStorage.getItem('token');
            const response = await axios.get(`http://localhost:5271/track/search?keyword=${searchTerm}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            setSearchResults(response.data);
        } catch (error) {
            if (error.message.includes('Request failed with status code 400')) {
                alert('Không thể tìm kiếm nếu để trống !');
            } else
                console.error('Error searching tracks:', error);
        }
    };

    return (
        <div className="container mt-4 py-4">
            <h2 className="text-center">Tìm kiếm bài hát</h2>
            <div className="input-group mb-3">
                <input
                    type="text"
                    className="form-control"
                    placeholder="Nhập tên bài hát..."
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                />
                <button className="btn btn-outline-primary" type="button" onClick={handleSearch}>
                    Tìm kiếm
                </button>
            </div>

            <ul className="list-group">
                {searchResults.map((track) => (
                    <li key={track.id} className="list-group-item">
                        <h5 className="trackName">{track.trackName}</h5>
                        <p className="author my-1">By: {track.author}</p>
                        <p className="description my-1">{track.description}</p>
                        <button className="btn btn-primary playButton">Play</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default SearchTrack;
