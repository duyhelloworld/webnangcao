-- Active: 1695612038229@@127.0.0.1@1433@webnangcao
INSERT INTO Categories (Name, Description)
VALUES
    (N'Pop', N'Nhạc Pop (Popular) hợp với mọi người'),
    (N'Remix', N'Những bản mix hay nhất'),
    (N'Rap', N'Những track Rap huyền thoại'),
    (N'R&B', N'Các ca khúc R&B đặc sắc'),
    (N'EDM', N'Những bản EDM hiện đại'),
    (N'Balad', N'Những bản Balad "tình" nhất'), 
    (N'Indie', N'Nơi các Indier toả sáng');

INSERT INTO Tracks (Name, Directory, UploadAt)
VALUES
    (N'7 Years', N'/Assets/musics/"7 Years.mp3"', '2023-10-27 08:00:00'),
    (N'Buồn thì cứ khóc đi', N'/Assets/musics/"Buon Thi Cu Khoc Di.mp3"', '2023-10-27 08:15:00'),
    (N'Đã lỡ yêu em nhiều', N'/Assets/musics/"Da Lo Yeu Em Nhieu.mp3"', '2023-10-27 08:30:00'),
    (N'Nandemonaiya ', N'/Assets/musics/"Nandemonaiya.mp3"', '2023-10-27 08:45:00'),
    (N'Rap chậm thôi', N'/Assets/musics/"Rap Cham Thoi.mp3"', '2023-10-27 09:00:00'),
    (N'Thủ Đô Cypher', N'/Assets/musics/"Thu Do Cypher.mp3"', '2023-10-27 09:15:00');

INSERT INTO Track_Categories (TrackId, CategoryId)
VALUES
    (1, 7),
    (2, 1),
    (2, 6),
    (3, 4),
    (4, 5),
    (5, 3),
    (6, 3);

INSERT INTO [Roles] ([Id], [Name], [NormalizedName])
VALUES ('1111', 'Admin', 'Quản trị viên hệ thống'),
       ('2222', 'SuperAdmin', 'Quản trị viên cao cấp'),
       ('3333', 'User', 'Người dùng');

INSERT INTO [Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnabled], [AccessFailedCount])
VALUES ('1', 'webnangcao', 'Web nâng cao', 'webnangcao@gmail.com', 'webnangcao@gmail.com', 'True', 'password', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('2', 'superwebnangcao', 'Super Admin', 'superwebnangcao@gmail.com', 'superwebnangcao@gmail.com', 'True', 'password', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('3', 'hiep', 'Hiệp', 'hiep8am@gmail.com', 'hiep8am@gmail.com', 'True', 'password', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('4', 'duy', 'Duy', 'codedaovoiduy@gmail.com', 'codedaovoiduy@gmail.com', 'True', 'password', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0);

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