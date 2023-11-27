-- Active: 1698044165228@@127.0.0.1@1433@webnangcao
INSERT INTO [Categories] (Name, Description)
VALUES
    (N'Pop', NULL),
    (N'Remix', N'Những bản mix hay nhất'),
    (N'Rap', N'Những track Rap huyền thoại'),
    (N'R&B', N'Các ca khúc Rock & Ballad đặc sắc'),
    (N'EDM', N'Những bản EDM hiện đại'),
    (N'Balad', N'Những bản Balad tình nhất'), 
    (N'Indie', N'Nơi các Indier toả sáng');
INSERT INTO [Users] ([UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [SecurityStamp], [ConcurrencyStamp], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnabled], [AccessFailedCount])
VALUES ('webnangcao','webnangcao', 'webnangcao@gmail.com', 'webnangcao@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('superwebnangcao', 'superwebnangcao', 'superwebnangcao@gmail.com', 'superwebnangcao@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('hiep', 'hiep', 'hiep8am@gmail.com', 'hiep8am@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('duy', 'duy', 'codedaovoiduy@gmail.com', 'codedaovoiduy@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('quang', 'quang', 'mail1@gmail.com', 'mail1@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('youzo', 'quan', 'mail2@gmail.com', 'mail2@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('chien', 'chien','mail3@gmail.com', 'mail3@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0);

INSERT INTO Tracks (Name, FileName, Description, Artwork, AuthorId, UploadAt, IsPrivate, ListenCount, LikeCount, CommentCount)
VALUES
    (N'7 Years', N'7 Years.mp3', 'Lukas Graham', N'7-Years.jpg', 4, '2023-10-10 09:10:10', 'true', 0, 0, 2),
    (N'Buồn thì cứ khóc đi', N'Buon Thi Cu Khoc Di.mp3', 'Lynk Lee', N'Buon-Thi-Cu-Khoc-Di.jpg', 4, '2023-10-10 09:11:01', 'false', 0, 1, 2),
    (N'Đã lỡ yêu em nhiều', N'Da Lo Yeu Em Nhieu.mp3', 'JustaTee', N'Da-Lo-Yeu-Em-Nhieu.jpg', 4, '2023-10-10 09:12:12', 'true', 0, 0, 0),
    (N'Nandemonaiya ', N'Nandemonaiya.mp3', '1012', N'default-artwork.jpg', 4, '2023-10-10 09:13:32', 'false', 0, 0, 0),
    (N'Rap chậm thôi', N'Rap Cham Thoi.mp3', 'MCK', N'Rap-Cham-Thoi.jpg', 7, '2023-06-21 10:14:12', 'false', 0, 0, 0),
    (N'Thủ Đô Cypher', N'Thu Do Cypher.mp3', 'MCK, LowG', N'Thu-Do-Cypher.jpg', 4, '2023-10-10 09:40:12', 'false', 0, 0, 0);

INSERT INTO Tracks_Categories (TrackId, CategoryId)
VALUES
    (1, 7),
    (2, 1),
    (3, 6),
    (3, 4),
    (4, 5),
    (5, 3),
    (6, 3);


INSERT INTO LikeTrack ( [UserId], [TrackId])
VALUES (1, 1),
        (1, 2),
        (1, 2),
        (2, 1),
        (2, 3),
        (3, 2),
        (3, 2),
        (4, 1);

INSERT INTO Comments ([Content], [CommentAt], [IsEdited], [TrackId], [UserId]) 
VALUES ('Hay quá', '2023-10-10 09:10:12', 'true', 2, 1),
('Tuyệt vời ạ', '2023-10-10 09:11:42', 'false', 2, 1), 
('This song is make my childhoods back. Thank you sir!', '2023-10-10 10:24:20', 'false', 2, 2), 
('Bài này hay quá, yeah yeah', '2023-10-10 10:11:20', 'false', 1, 1), 
('Nhạc này còn hơi kén người nghe quá bro', '2023-10-10 19:50:12', 'true', 2 , 3);

INSERT INTO [Roles] ([Name], [NormalizedName])
VALUES ('Admin', 'admin'),
       ('SuperAdmin', 'superadmin'),
       ('User', 'user');

INSERT INTO [UserRoles] ([UserId], [RoleId])
VALUES (2, 1),
       (2, 2),
       (3, 3),
       (4, 3),
       (5, 3),
       (6, 3),
       (7, 3);
INSERT INTO [Playlists] (Name, CreatedAt, IsPrivate, AuthorId, Description, ArtWork, Tags, LikeCount, RepostCount)
VALUES 
        ('TopBXH', '2023-10-11 10:11:00', 'false', 1, 'Playlist thịnh hành', 'default-artwork.jpg', '#BXH, #Top', 0, 0),
        ('TopMoiNhat', '2023-10-11 10:11:00', 'false', 1, 'Playlist  mới nhất', 'default-artwork.jpg', '#Top, #MoiNhat', 0, 10),
        ('NhacCuaHiep', '2023-10-11 10:11:00', 'true', 3, 'Playlist theo gu hiep', 'default-artwork.jpg', NULL, 1, 1),
        ('NhacCuaDuy', '2023-10-11 10:11:00', 'false', 4, 'Playlist theo gu duy', 'default-artwork.jpg', NULL, 1, 10),
        ('NhacCuaQuang', '2023-10-11 10:11:00', 'true', 5, 'Playlist theo gu quang', 'default-artwork.jpg', NULL,1, 0),
        ('NhacCuaYouzo', '2023-10-11 10:11:00', 'true', 6, 'Playlist theo gu youzo', 'default-artwork.jpg', NULL, 0, 0),
        ('NhacCuaChien', '2023-10-11 10:11:00', 'true', 7, 'Playlist theo gu chien', 'default-artwork.jpg', NULL, 1, 2);
INSERT INTO LikePlaylist (UserId, PlaylistId)
VALUES 
        (1, 8),
        (1, 9),
        (1, 10),
        (1, 4),
        (1, 5),
        (1, 6),
        (1, 7),
        (2, 8),
        (2, 9),
        (2, 10),
        (2, 4),
        (2, 5),
        (2, 6),
        (2, 7),
        (3, 8),
        (3, 9),
        (3, 10),
        (3, 4),
        (3, 5),
        (3, 6),
        (3, 7),
        (4, 8),
        (4, 9),
        (4, 10),
        (4, 4),
        (4, 5),
        (4, 6),
        (4, 7),
        (5, 8),
        (5, 9),
        (5, 10),
        (5, 4),
        (5, 5),
        (5, 6),
        (5, 7),
        (6, 8),
        (6, 9),
        (6, 10),
        (6, 4),
        (6, 5),
        (6, 6),
        (6, 7),
        (7, 8),
        (7, 9),
        (7, 10),
        (7, 4),
        (7, 5),
        (7, 6),
        (7, 7);
INSERT INTO [Tracks_Playlists] (PlaylistId, TrackId)
VALUES 
        (8, 1),
        (8, 2),
        (8, 5),
        (8, 6),
        (9, 1),
        (9, 2),
        (9, 3),
        (10, 2),
        (10, 3),
        (4, 2),
        (4, 3),
        (4, 4),
        (4, 5),
        (4, 6),
        (5, 6),
        (6, 1),
        (7, 1),
        (7, 6); 
