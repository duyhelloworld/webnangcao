import React, { useState } from 'react';
import axios from 'axios';

const DeleteComment = ({ commentId, onDelete }) => {
    const [confirmDelete, setConfirmDelete] = useState(false);
    const handleDelete = async () => {
        try {
            // Lấy JWT từ nơi bạn lưu trữ
            const token = localStorage.getItem('token'); // Thay thế bằng cách lấy token từ nơi bạn lưu trữ
            // Gửi request để xóa comment với commentId
            await axios.delete(`http://localhost:5271/comment/${commentId}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

                onDelete(commentId);
        } catch (error) {
            if (error.message.includes('Request failed with status code 404')) {
                alert('Bạn không thể xóa bình luận này!');
              } else
            console.error('Error deleting comment:', error);
        }
    };
    const handleToggleConfirm = () => {
        setConfirmDelete(!confirmDelete);
    };

    return (
        <div className="container">
            {confirmDelete ? (
                <div className='container'>
                    <div className='row'>
                        <h5>Bạn có chắc muốn xóa bình luận này ??</h5>
                    </div>
                    <div className='row'>
                        <div className='col-2'>
                            <button className="btn btn-secondary btn-sm" onClick={handleDelete}>Có</button>
                        </div>
                        <div className='col-2'>
                            <button className="btn btn-secondary btn-sm" onClick={handleToggleConfirm}>Không</button>
                        </div>
                    </div>
                </div>
            ) : (
                <button className="btn btn-secondary" onClick={handleToggleConfirm}>Xóa bình luận</button>
            )}
        </div>
    );
};

export default DeleteComment;
