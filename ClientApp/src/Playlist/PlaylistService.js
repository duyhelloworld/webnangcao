import axios from "axios";

const API_URL = "http://localhost:5271/playlist";

const PlaylistService = {
  search: async (keyword) => {
    const response = await axios.get(`${API_URL}/search?keyword=${keyword}`);
    return response.data;
  },

  getPlaylist: async (page) => {
    const response = await axios.get(`${API_URL}?page=${page}`);
    return response.data;
  },

  getPlaylistById: async (playlistId) => {
    const response = await axios.get(`${API_URL}/${playlistId}`);
    return response.data;
  },

  getAdminPlaylists: async () => {
    const response = await axios.get(`${API_URL}/admin/all`);
    return response.data;
  },

  getUserPlaylists: async () => {
    const response = await axios.get(`${API_URL}/user/all`);
    return response.data;
  },

  likePlaylist: async (playlistId) => {
    const response = await axios.put(`${API_URL}/${playlistId}/like`);
    return response.data;
  },

  repostPlaylist: async (playlistId) => {
    const response = await axios.post(`${API_URL}/${playlistId}/repost`);
    return response.data;
  },

  createPlaylist: async (playlistData) => {
    const response = await axios.post(`${API_URL}`, playlistData, {
      headers: {
        "Content-Type": "application/json",
      },
    });
    return response.data;
  },

  updatePlaylistInformation: async (playlistId, informationData) => {
    const response = await axios.put(
      `${API_URL}/${playlistId}/information`,
      informationData,
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    return response.data;
  },

  deletePlaylist: async (playlistId, userType) => {
    const response = await axios.delete(`${API_URL}/${userType}/${playlistId}`);
    return response.data;
  },
};

export default PlaylistService;
