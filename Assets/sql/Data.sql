-- Active: 1698578533477@@127.0.0.1@1433@webnangcao
INSERT INTO Categories (Name, Description)
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
    (N'7 Years', N'/Assets/musics/"7 Years.mp3"', 'Lukas Graham', N'/Assets/images/"7 Years.jpg"'),
    (N'Buồn thì cứ khóc đi', N'/Assets/musics/"Buon Thi Cu Khoc Di.mp3"', 'Mr.Siro', N'/Assets/images/"Buon Thi Cu Khoc Di.jpg"'),
    (N'Đã lỡ yêu em nhiều', N'/Assets/musics/"Da Lo Yeu Em Nhieu.mp3"', 'JustaTee', N'/Assets/images/"Da Lo Yeu Em Nhieu.jpg"'),
    (N'Nandemonaiya ', N'/Assets/musics/"Nandemonaiya.mp3"', '1012', N'/Assets/images/"Nandemonaiya.jpg"'),
    (N'Rap chậm thôi', N'/Assets/musics/"Rap Cham Thoi.mp3"', 'MCK', N'/Assets/images/"Rap Cham Thoi.jpg"'),
    (N'Thủ Đô Cypher', N'/Assets/musics/"Thu Do Cypher.mp3"', 'MCK, LowG', N'/Assets/images/"Thu Do Cypher.jpg"');

INSERT INTO Track_Category (TrackId, CategoryId)
VALUES
    (1, 7),
    (2, 1),
    (2, 6),
    (3, 4),
    (4, 5),
    (5, 3),
    (6, 3);

INSERT INTO UserTrackActions ([Id], [UserId], [TrackId], [ActionType], [ActionAt])
VALUES ('1', 1, 1, 'UPLOAD', '2023-10-10'),
        ('2', 1, 2, 'LIKE', '2023-10-10'),
        ('3', 2, 1, 'LIKE', '2023-10-11');

INSERT INTO Comments ([Content], [CommentAt], [LastEditAt], [TrackId], [UserId]) 
VALUES ('Hay quá', '2023-10-10 09:10:12', '2023-10-10 09:10:12', 1, '1'),
('Tuyệt vời ạ', '2023-10-10 09:11:42', '2023-10-10 09:12:30', 1, '1'), 
('This song is make my childhoods back. Thank you sir!', '2023-10-10 10:24:20', '2023-10-11 09:30:11', 2, '1'), 
('Bài này hay quá, yeah yeah', '2023-10-10 10:11:20', '2023-10-12 11:11:01', 1, '2'), 
('Nhạc này còn hơi kén người nghe quá bro', '2023-10-10 19:50:12', '2023-10-12 20:11:21', 1, '1');



INSERT INTO [Roles] ([Id], [Name], [NormalizedName])
VALUES ('1111', 'Admin', 'admin'),
       ('2222', 'SuperAdmin', 'superadmin'),
       ('3333', 'User', 'user');

INSERT INTO [Users] 
       ([Id], [UserName], [NormalizedUserName], [FullName], [Email], [NormalizedEmail], [EmailConfirmed], [SecurityStamp], [ConcurrencyStamp], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnabled], [AccessFailedCount])
VALUES ('1', 'webnangcao','webnangcao', N'Web nâng cao', 'webnangcao@gmail.com', 'webnangcao@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('2', 'superwebnangcao', 'superwebnangcao ', N'Super Admin', 'superwebnangcao@gmail.com', 'superwebnangcao@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('3', 'hiep', 'hiep', N'Hiệp', 'hiep8am@gmail.com', 'hiep8am@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('4', 'duy', 'duy', N'Duy', 'codedaovoiduy@gmail.com', 'codedaovoiduy@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('5', 'quang', 'quang', N'Duy Quang', 'mail1@gmail.com', 'mail1@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('6', 'youzo', 'quan', N'Hoàng Quân', 'mail2@gmail.com', 'mail2@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('7', 'chien', 'chien', N'Xuân Chiến', 'mail3@gmail.com', 'mail3@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0);

INSERT INTO [UserRoles] ([UserId], [RoleId])
VALUES ('1', '1111'),
       ('2', '2222'),
       ('3', '3333'),
       ('4', '3333');

INSERT INTO RoleClaims (RoleId, ClaimType, ClaimValue)
VALUES
    (N'1111', N'CanEditComment ', N'true'),
    (N'1111', N'CanAddComment', N'true'),
    (N'2222', N'CanAddTrack', N'false'),
    (N'2222', N'CanEditTrack', N'false'),
    (N'3333', N'CanDeleteUser', N'false'),
    (N'3333', N'CanDeleteTrack', N'false');