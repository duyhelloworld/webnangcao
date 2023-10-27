INSERT INTO Categories (Name, Description)
VALUES
    (N'Pop', N'Bản nhạc Pop phổ biến'),
    (N'Rock', N'Bản nhạc Rock nổi tiếng'),
    (N'Rap', N'Bản nhạc Rap thời thượng'),
    (N'Dance', N'Bản nhạc Dance sôi động'),
    (N'Jazz', N'Bản nhạc Jazz mượt mà'),
    (N'Classical', N'Bản nhạc Classical truyền thống'),
    (N'Electronic', N'Bản nhạc Electronic hiện đại'),
    (N'Country', N'Bản nhạc Country đồng quê'),
    (N'Blues', N'Bản nhạc Blues cảm xúc'),
    (N'Indie', N'Bản nhạc Indie độc lập');

INSERT INTO Roles (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES
    (N'1', N'Admin', N'ADMIN', N'abcdef123456'),
    (N'2', N'SuperAdmin', N'SUPERADMIN', N'ghijkl789012'),
    (N'3', N'User', N'USER', N'mnopqr345678');

INSERT INTO Tracks (Name, Directory, UploadAt)
VALUES
    (N'7 Years', N'/Assets/musics/"7 Years.mp3"', '2023-10-27 08:00:00'),
    (N'Buồn thì cứ khóc đi', N'/Assets/musics/"Buon Thi Cu Khoc Di.mp3"', '2023-10-27 08:15:00'),
    (N'Đã lỡ yêu em nhiều', N'/Assets/musics/"Da Lo Yeu Em Nhieu.mp3"', '2023-10-27 08:30:00'),
    (N'Nandemonaiya ', N'/Assets/musics/"Nandemonaiya.mp3"', '2023-10-27 08:45:00'),
    (N'Rap chậm thôi', N'/Assets/musics/"Rap Cham Thoi.mp3"', '2023-10-27 09:00:00'),
    (N'Thủ Đô Cypher', N'/Assets/musics/"Thu Do Cypher.mp3"', '2023-10-27 09:15:00');

INSERT INTO RoleClaims (RoleId, ClaimType, ClaimValue)
VALUES
    (N'1', N'CanEditComment ', N'true'),
    (N'1', N'CanAddComment', N'true'),
    (N'2', N'CanAddTrack', N'false'),
    (N'2', N'CanEditTrack', N'false'),
    (N'3', N'CanDeleteUser', N'false'),
    (N'3', N'CanDeleteTrack', N'false');

INSERT INTO Track_Categories (TrackId, CategoryId)
VALUES
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4),
    (5, 5),
    (6, 6),
    (7, 7),
    (8, 8),
    (9, 9),
    (10, 10);

INSERT INTO Comments (Content, CommentAt, LastEditAt, TrackId, UserId)
VALUES
    (N'Hay', '2023-10-27 08:10:00', '2023-10-27 08:15:00', 1, N'admin'),
    (N'Tuyệt vời', '2023-10-27 08:20:00', '2023-10-27 08:25:00', 2, N'superadmin'),
    (N'hayvail', '2023-10-27 08:30:00', '2023-10-27 08:35:00', 3, N'user1'),
    (N'dme bọn bắt kì', '2023-10-27 08:40:00', '2023-10-27 08:45:00', 4, N'user2'),
    (N'rap chậm thôi', '2023-10-27 08:50:00', '2023-10-27 08:55:00', 5, N'user3'),
    (N'justatee mãi đỉnh', '2023-10-27 09:00:00', '2023-10-27 09:05:00', 6, N'user4'),
    (N'the most favour song in my life', '2023-10-27 09:10:00', '2023-10-27 09:15:00', 7, N'user5'),
    (N'Thank you, lynk lee', '2023-10-27 09:20:00', '2023-10-27 09:25:00', 8, N'user6');

INSERT INTO Playlists (Name, CreatedAt, LastUpdatedAt, Description, ArtWorkDirectory, CreateUserId, Tags)
VALUES
    (N'Playlist 1', '2023-10-27 08:00:00', '2023-10-27 08:15:00', N),
    