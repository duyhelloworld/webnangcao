import React, { useState } from 'react';
import axios from 'axios';
// import { useParams } from 'react-router-dom';

const CommentAdd = ({trackId}) => {
  const [content, setContent] = useState('');
  // let { trackId } = useParams(); 

  const handleAddComment = async () => {
    try {
        if (content.trim() === '') {
            alert('Comment không để trống nhé');
            return;
          }
      // Lấy JWT từ nơi bạn lưu trữ
      const token = localStorage.getItem('token');// Thay thế bằng cách lấy token từ nơi bạn lưu trữ

      // Gửi request để thêm comment
      const response = await axios.post(
        `http://localhost:5271/comment/track/${trackId}`,
        {
          Content: content,
          TrackId: trackId, // Bổ sung trackId vào request body
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json',
          },
        }
      );

      // In response từ server (có thể xử lý hoặc thông báo thành công)
      console.log('Comment added:', response.data);

      // Reset nội dung sau khi thêm comment thành công
      setContent('');
    } catch (error) {
      console.error('Error adding comment:', error);
    }
  };

    
  return (
    <div className="container-fluid py-4 border border-4">
      <h5>Thêm bình luận của bạn: </h5>
      <textarea className='form-control'
        value={content}
        onChange={(e) => setContent(e.target.value)}
        placeholder="Nhập vào đây..."
      />
      <br/><button className="btn btn-primary" onClick={handleAddComment}>Thêm bình luận</button>
    </div>  
  );
};

export default CommentAdd;
