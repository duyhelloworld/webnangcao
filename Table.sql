IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Roles] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(256) NOT NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Tracks] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL DEFAULT AudioFile,
    [Description] nvarchar(max) NULL,
    [AudioFile] nvarchar(max) NOT NULL,
    [ArtWork] nvarchar(max) NULL,
    [ListenCount] int NOT NULL,
    [LikeCount] int NOT NULL,
    [CommentCount] int NOT NULL,
    [ArtworkFile] nvarchar(max) NOT NULL,
    [IsPrivate] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Tracks] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] bigint NOT NULL IDENTITY,
    [UserName] nvarchar(256) NOT NULL,
    [Email] nvarchar(256) NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Avatar] nvarchar(max) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] bigint NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Tracks_Categories] (
    [TrackId] int NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Tracks_Categories] PRIMARY KEY ([CategoryId], [TrackId]),
    CONSTRAINT [FK_Tracks_Categories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Tracks_Categories_Tracks_TrackId] FOREIGN KEY ([TrackId]) REFERENCES [Tracks] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [Content] nvarchar(max) NOT NULL,
    [CommentAt] datetime2 NOT NULL,
    [IsEdited] datetime2 NOT NULL DEFAULT 0,
    [Status] BIT NOT NULL DEFAULT 1,
    [TrackId] int NOT NULL,
    [UserId] bigint NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Tracks_TrackId] FOREIGN KEY ([TrackId]) REFERENCES [Tracks] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Follows] (
    [FollowingUserId] bigint NOT NULL,
    [FollowedUserId] bigint NOT NULL,
    [FollowedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Follows] PRIMARY KEY ([FollowedUserId], [FollowingUserId]),
    CONSTRAINT [FK_Follows_Users_FollowedUserId] FOREIGN KEY ([FollowedUserId]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_Follows_Users_FollowingUserId] FOREIGN KEY ([FollowingUserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [Playlists] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [LastUpdatedAt] datetime2 NOT NULL,
    [IsPrivate] bit NOT NULL,
    [AuthorId] bigint NOT NULL,
    [Description] nvarchar(max) NULL,
    [ArtWork] nvarchar(max) NULL,
    [Tags] nvarchar(max) NULL, -- #Pop, #Rap, #RnB
    CONSTRAINT [PK_Playlists] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Playlists_Users_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] bigint NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] bigint NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserRoles] (
    [UserId] bigint NOT NULL,
    [RoleId] bigint NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserTokens] (
    [UserId] bigint NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserTrackActions] (
    [Id] nvarchar(450) NOT NULL,
    [ActionType] varchar(10) NOT NULL,
    [ActionAt] datetime2 NOT NULL,
    [UserId] bigint NOT NULL,
    [TrackId] int NOT NULL,
    CONSTRAINT [PK_UserTrackActions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserTrackActions_Tracks_TrackId] FOREIGN KEY ([TrackId]) REFERENCES [Tracks] ([Id]),
    CONSTRAINT [FK_UserTrackActions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [Tracks_Playlists] (
    [TrackId] int NOT NULL,
    [PlaylistId] int NOT NULL,
    CONSTRAINT [PK_Tracks_Playlists] PRIMARY KEY ([PlaylistId], [TrackId]),
    CONSTRAINT [FK_Tracks_Playlists_Playlists_PlaylistId] FOREIGN KEY ([PlaylistId]) REFERENCES [Playlists] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Tracks_Playlists_Tracks_TrackId] FOREIGN KEY ([TrackId]) REFERENCES [Tracks] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserPlaylistActions] (
    [Id] nvarchar(450) NOT NULL,
    [ActionType] varchar(10) NOT NULL,
    [ActionAt] datetime2 NOT NULL,
    [UserId] bigint NOT NULL,
    [PlaylistId] int NOT NULL,
    CONSTRAINT [PK_UserPlaylistActions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserPlaylistActions_Playlists_PlaylistId] FOREIGN KEY ([PlaylistId]) REFERENCES [Playlists] ([Id]),
    CONSTRAINT [FK_UserPlaylistActions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE INDEX [IX_Comments_TrackId] ON [Comments] ([TrackId]);
GO

CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
GO

CREATE INDEX [IX_Follows_FollowingUserId] ON [Follows] ([FollowingUserId]);
GO

CREATE INDEX [IX_Playlists_AuthorId] ON [Playlists] ([AuthorId]);
GO

CREATE INDEX [IX_RoleClaims_RoleId] ON [RoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_Tracks_Categories_TrackId] ON [Tracks_Categories] ([TrackId]);
GO

CREATE INDEX [IX_Tracks_Playlists_TrackId] ON [Tracks_Playlists] ([TrackId]);
GO

CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);
GO

CREATE INDEX [IX_UserLogins_UserId] ON [UserLogins] ([UserId]);
GO

CREATE INDEX [IX_UserPlaylistActions_PlaylistId] ON [UserPlaylistActions] ([PlaylistId]);
GO

CREATE INDEX [IX_UserPlaylistActions_UserId] ON [UserPlaylistActions] ([UserId]);
GO

CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_UserTrackActions_TrackId] ON [UserTrackActions] ([TrackId]);
GO

CREATE INDEX [IX_UserTrackActions_UserId] ON [UserTrackActions] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231113091940_InitialCreate', N'7.0.12');
GO

COMMIT;
GO

