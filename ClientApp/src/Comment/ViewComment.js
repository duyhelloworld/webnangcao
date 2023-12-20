import React, { useState, useEffect } from 'react';
import axios from 'axios';
import DeleteComment from './DeleteComment';
import CommentAdd from './CommentAdd';
import './style.css';

const ViewComment = ({ trackId }) => {
  const [comments, setComments] = useState([]);

  useEffect(() => {
    const fetchComments = async () => {
      try {
        // Lấy JWT từ nơi bạn lưu trữ

        const token = localStorage.getItem('token'); // Thay thế bằng cách lấy token từ nơi bạn lưu trữ

        // Gửi request để lấy danh sách comment của bài hát có trackId
        const response = await axios.get(`http://localhost:5271/comment/track/${trackId}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        // Cập nhật state với dữ liệu comments từ response
        setComments(response.data);
      } catch (error) {
        console.error('Error fetching comments:', error);
      }
    };

    // Gọi hàm để fetch comments khi component được mount
    fetchComments();
  }, [trackId]);

  const handleDeleteComment = (deletedCommentId) => {
    setComments((prevComments) => prevComments.filter(comment => comment.id !== deletedCommentId));
  };

  const handleAddComment = async (newComment) => {
    setComments((prevComments) => [...prevComments, newComment]);
  };

  return (
    <div>
      <div className='container-fluid py-4'>
        <div>
          <h3>Bình luận bài hát: </h3>
          <CommentAdd trackId={trackId} onAddComment={handleAddComment} />
        </div>

        <div className='py-4'>
          <h5>Bình luận: </h5>
          <ul className="list-group">
            {comments.map((comment) => (
              <div className="card mb-3" key={comment.id}>
                <div className="card-body">
                  <p className='fs-5'>{comment.content}</p>
                  <p>Được tạo vào: {comment.createdAt}</p>
                  <p>Người bình luận: {comment.userId}</p>
                  <DeleteComment commentId={comment.id} onDelete={handleDeleteComment} />
                </div>
              </div>
            ))}
          </ul>
        </div>

      </div>
    </div>
  );
};

export default ViewComment;
