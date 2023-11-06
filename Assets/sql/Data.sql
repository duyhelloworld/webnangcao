-- Active: 1698578533477@@127.0.0.1@1433@webnangcao
INSERT INTO [Categories] (Name, Description)
VALUES
    (N'Pop', N'Nhạc Pop (Popular) hợp với mọi người'),
    (N'Remix', N'Những bản mix hay nhất'),
    (N'Rap', N'Những track Rap huyền thoại'),
    (N'R&B', N'Các ca khúc R&B đặc sắc'),
    (N'EDM', N'Những bản EDM hiện đại'),
    (N'Balad', N'Những bản Balad "tình" nhất'), 
    (N'Indie', N'Nơi các Indier toả sáng');

INSERT INTO Tracks (Name, Location, Artwork , Description)
VALUES
    (N'7 Years', N'/Assets/musics/"7 Years.mp3"', 'Lukas Graham', N'/Assets/images/7-Years.jpg'),
    (N'Buồn thì cứ khóc đi', N'/Assets/musics/"Buon Thi Cu Khoc Di.mp3"', 'Mr.Siro', N'/Assets/images/Buon-Thi-Cu-Khoc-Di.jpg'),
    (N'Đã lỡ yêu em nhiều', N'/Assets/musics/"Da Lo Yeu Em Nhieu.mp3"', 'JustaTee', N'/Assets/images/Da-Lo-Yeu-Em-Nhieu.jpg'),
    (N'Nandemonaiya ', N'/Assets/musics/"Nandemonaiya.mp3"', '1012', N'/Assets/images/Nandemonaiya.jpg'),
    (N'Rap chậm thôi', N'/Assets/musics/"Rap Cham Thoi.mp3"', 'MCK', N'/Assets/images/Rap-Cham-Thoi.jpg'),
    (N'Thủ Đô Cypher', N'/Assets/musics/"Thu Do Cypher.mp3"', 'MCK, LowG', N'/Assets/images/Thu-Do-Cypher.jpg');

INSERT INTO Tracks_Categories (TrackId, CategoryId)
VALUES
    (1, 7),
    (2, 1),
    (2, 6),
    (3, 4),
    (4, 5),
    (5, 3),
    (6, 3);

INSERT INTO [Users] ([UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [SecurityStamp], [ConcurrencyStamp], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnabled], [AccessFailedCount])
VALUES ('webnangcao','webnangcao', 'webnangcao@gmail.com', 'webnangcao@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('superwebnangcao', 'superwebnangcao ', 'superwebnangcao@gmail.com', 'superwebnangcao@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('hiep', 'hiep', 'hiep8am@gmail.com', 'hiep8am@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('duy', 'duy', 'codedaovoiduy@gmail.com', 'codedaovoiduy@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('quang', 'quang', 'mail1@gmail.com', 'mail1@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('youzo', 'quan', 'mail2@gmail.com', 'mail2@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('chien', 'chien','mail3@gmail.com', 'mail3@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0);

INSERT INTO UserTrackActions ([Id], [UserId], [TrackId], [ActionType], [ActionAt])
VALUES ('384-43423-243', 1, 1, 'UPLOAD', '2023-10-10 09:10:10'),
        ('210-543-1243', 1, 2, 'LIKE', '2023-10-10 09:12:42'),
        ('549e8645-123', 1, 2, 'PLAY', '2023-10-10 09:12:45'),
        ('32393gijf-324', 2, 1, 'LIKE', '2023-10-11 09:10:12'),
        ('384-43423-244', 2, 3, 'UPLOAD', '2023-10-11 10:10:10'),
        ('210-543-1244', 3, 2, 'LIKE', '2023-10-12 09:12:42'),
        ('549e8645-124', 3, 2, 'PLAY', '2023-10-12 09:12:45'),
        ('32393gijf-325', 4, 1, 'LIKE', '2023-10-13 09:10:12');

INSERT INTO Comments ([Content], [CommentAt], [LastEditAt], [TrackId], [UserId]) 
VALUES ('Hay quá', '2023-10-10 09:10:12', '2023-10-10 09:10:12', 2, 1),
('Tuyệt vời ạ', '2023-10-10 09:11:42', '2023-10-10 09:12:30', 2, 1), 
('This song is make my childhoods back. Thank you sir!', '2023-10-10 10:24:20', '2023-10-11 09:30:11', 2, 2), 
('Bài này hay quá, yeah yeah', '2023-10-10 10:11:20', '2023-10-12 11:11:01', 1, 1), 
('Nhạc này còn hơi kén người nghe quá bro', '2023-10-10 19:50:12', '2023-10-12 20:11:21', 2 , 3);

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