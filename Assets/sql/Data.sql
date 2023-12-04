-- Active: 1698044165228@@127.0.0.1@1433@webnangcao
INSERT INTO Categories (Name, Description)
VALUES
('Rock', 'Nhạc rock là một thể loại nhạc có nguồn gốc từ Hoa Kỳ vào cuối những năm 1940 và đầu những năm 1950. Nhạc rock thường được kết hợp với các nhạc cụ điện tử như guitar điện, bass điện và trống điện.'),
('Pop', 'Nhạc pop là một thể loại nhạc phổ biến với giai điệu dễ nghe và lời bài hát dễ nhớ. Nhạc pop thường được kết hợp với các nhạc cụ như guitar, trống, piano và keyboard.'),
('Hip hop', 'Nhạc hip hop là một thể loại nhạc có nguồn gốc từ Hoa Kỳ vào cuối những năm 1970. Nhạc hip hop thường được kết hợp với các nhạc cụ như trống, DJ và rap.'),
('R&B', 'Nhạc R&B là một thể loại nhạc kết hợp giữa nhạc pop và nhạc blues. Nhạc R&B thường được kết hợp với các nhạc cụ như guitar, piano, trống và giọng hát.'),
('Country', 'Nhạc đồng quê là một thể loại nhạc có nguồn gốc từ Hoa Kỳ vào cuối những năm 1800. Nhạc đồng quê thường được kết hợp với các nhạc cụ như guitar, banjo, mandolin và fiddle.'),
('Classical', 'Nhạc cổ điển là một thể loại nhạc có nguồn gốc từ châu Âu vào thế kỷ 17. Nhạc cổ điển thường được kết hợp với các nhạc cụ như violin, cello, piano và dàn nhạc giao hưởng.'),
('Jazz', 'Nhạc jazz là một thể loại nhạc có nguồn gốc từ Hoa Kỳ vào đầu những năm 1900. Nhạc jazz thường được kết hợp với các nhạc cụ như kèn saxophone, kèn trumpet, piano và trống.'),
('Latin', 'Nhạc Latin là một thể loại nhạc có nguồn gốc từ châu Mỹ Latin. Nhạc Latin thường được kết hợp với các nhạc cụ như guitar, bongos, timbales và maracas.'),
('Electronic', 'Nhạc điện tử là một thể loại nhạc sử dụng các nhạc cụ điện tử. Nhạc điện tử thường được kết hợp với các nhạc cụ như synthesizer, drum machine và sequencer.'),
('Metal', 'Nhạc metal là một thể loại nhạc có nguồn gốc từ Hoa Kỳ vào cuối những năm 1960. Nhạc metal thường được kết hợp với các nhạc cụ như guitar điện, bass điện, trống và giọng hát mạnh mẽ.'),
('Folk', 'Nhạc folk là một thể loại nhạc truyền thống thường được hát với guitar hoặc các nhạc cụ dân gian khác. Nhạc folk thường được kết hợp với các bài hát về tình yêu, thiên nhiên và cuộc sống.'),
('Opera', 'Opera là một thể loại nhạc kịch có nguồn gốc từ Ý vào thế kỷ 16. Opera thường được kết hợp với các ca sĩ, vũ công và dàn nhạc.'),
('Musical', 'Musical là một thể loại nhạc kịch có nguồn gốc từ Hoa Kỳ vào thế kỷ 19. Musical thường được kết hợp với các ca sĩ, vũ công, dàn nhạc và kịch bản.'),
('Soundtrack', 'Soundtrack là một album nhạc được sáng tác cho một bộ phim hoặc chương trình truyền hình. Soundtrack thường được kết hợp với các bài hát gốc và các bài hát cover.'),
('World music', 'World music là một thể loại nhạc bao gồm các thể loại nhạc từ khắp nơi trên thế giới. World music thường được kết hợp với các nhạc cụ và phong cách hát từ các nền văn hóa khác nhau.');

INSERT INTO [Users] ([UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [SecurityStamp], [ConcurrencyStamp], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnabled], [AccessFailedCount])
VALUES ('webnangcao','webnangcao', 'webnangcao@gmail.com', 'webnangcao@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('superwebnangcao', 'superwebnangcao', 'superwebnangcao@gmail.com', 'superwebnangcao@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('hiep', 'hiep', 'hiep8am@gmail.com', 'hiep8am@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('duy', 'duy', 'codedaovoiduy@gmail.com', 'codedaovoiduy@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('quang', 'quang', 'mail1@gmail.com', 'mail1@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('youzo', 'quan', 'mail2@gmail.com', 'mail2@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0),
       ('chien', 'chien','mail3@gmail.com', 'mail3@gmail.com', 'True', 'security_stamp', 'concurrency_stamp', 'False', 'False', 'False', 0);
INSERT INTO Tracks (Name, FileName, Artwork, UploadAt, IsPrivate, ListenCount, LikeCount, CommentCount, Description, AuthorId)
VALUES
('Bài hát 1', '1.mp3', 'default-artwork.jpg', '2023-10-10 09:10:12', 'false', 0, 0, 0, 'Bài hát 1', 1),
('Bài hát 2', '2.mp3', 'default-artwork.jpg', '2023-10-10 09:10:12', 'false', 0, 0, 0, 'Bài hát 2', 2),
('Bài hát 3', '3.mp3', 'default-artwork.jpg', '2023-10-10 09:10:12', 'false', 0, 0, 0, 'Bài hát 3', 3),
('Bài hát 4', '4.mp3', 'default-artwork.jpg', '2023-10-10 09:10:12', 'false', 0, 0, 0, 'Bài hát 4', 4),
('Bài hát 5', '5.mp3', 'default-artwork.jpg', '2023-10-10 09:10:12', 'false', 0, 0, 0, 'Bài hát 5', 5),
('Bài hát 6', '6.mp3', 'default-artwork.jpg', '2023-10-10 09:10:12', 'false', 0, 0, 0, 'Bài hát 6', 6),
('Bài hát 7', '7.mp3', 'default-artwork.jpg', '2023-10-10 09:10:12', 'false', 0, 0, 0, 'Bài hát 7', 7),
('Bài hát 8', '8.mp3', 'default-artwork.jpg', '2023-10-10 09:10:12', 'false', 0, 0, 0, 'Bài hát 8', 1),
('Bài hát 9', '9.mp3', 'default-artwork.jpg', '2023-10-10 09:10:12', 'false', 0, 0, 0, 'Bài hát 9', 2),
('Bài hát 10', '10.mp3', 'default-artwork.jpg', '2023-10-10 09:10:12', 'false', 0, 0, 0, 'Bài hát 10', 3);



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
        (2, 1),
        (2, 3),
        (3, 2),
        (4, 1);

INSERT INTO Comments ([Content], [CommentAt], [IsEdited], [TrackId], [UserId], [IsReported]) 
VALUES ('Hay quá', '2023-10-10 09:10:12', 'true', 2, 1, 0),
('Tuyệt vời ạ', '2023-10-10 09:11:42', 'false', 2, 1, 0),
('This song is make my childhoods back. Thank you sir!', '2023-10-10 10:24:20', 'false', 2, 2, 0),
('Bài này hay quá, yeah yeah', '2023-10-10 10:11:20', 'false', 1, 1, 0),
('Nhạc này còn hơi kén người nghe quá bro', '2023-10-10 19:50:12', 'true', 2 , 3, 0);

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
        (1, 1),
        (1, 2),
        (1, 3),
        (1, 4),
        (1, 5),
        (1, 6),
        (1, 7),
        (2, 1),
        (2, 2),
        (2, 3),
        (2, 4),
        (2, 5),
        (2, 6),
        (2, 7),
        (3, 1),
        (3, 2),
        (3, 3),
        (3, 4),
        (3, 5),
        (3, 6),
        (3, 7),
        (4, 1),
        (4, 2),
        (4, 3),
        (4, 4),
        (4, 5),
        (4, 6),
        (4, 7),
        (5, 1),
        (5, 2),
        (5, 3),
        (5, 4),
        (5, 5),
        (5, 6),
        (5, 7),
        (6, 1),
        (6, 2),
        (6, 3),
        (6, 4),
        (6, 5),
        (6, 6),
        (6, 7),
        (7, 1),
        (7, 2),
        (7, 3),
        (7, 4),
        (7, 5),
        (7, 6),
        (7, 7);
INSERT INTO [Tracks_Playlists] (PlaylistId, TrackId)
VALUES 
        (1, 1),
        (1, 2),
        (1, 5),
        (1, 6),
        (2, 1),
        (2, 2),
        (2, 3),
        (3, 2),
        (3, 3),
        (4, 2),
        (4, 3),
        (4, 4),
        (4, 5),
        (4, 6),
        (5, 6),
        (6, 1),
        (7, 1),
        (7, 6); 
