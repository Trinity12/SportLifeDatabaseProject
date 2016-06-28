USE [SportLifeDatabase]
GO

CREATE TABLE [dbo].[FileType]
(
    FileTypeId INT,
    TypeName NVARCHAR(MAX),
    CONSTRAINT [PK_FileType] PRIMARY KEY CLUSTERED ([FileTypeId])
)
GO

CREATE TABLE [dbo].[Image]
(
    FileId INT NOT NULL,
    FileName NVARCHAR(MAX) NOT NULL,
    ContentType NVARCHAR(MAX) NOT NULL,
    Content VARBINARY(MAX) NOT NULL,
    FileType INT NOT NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED ([FileId]),
    CONSTRAINT [FK_Image_FileType] FOREIGN KEY ([FileType]) REFERENCES [dbo].[FileType] ([FileTypeId]) ON DELETE CASCADE
)
GO

CREATE TABLE [dbo].[UserImage]
(
    [UserId] INT NOT NULL,
    [FileId] INT NOT NULL,
    CONSTRAINT [PK_UserImage] PRIMARY KEY CLUSTERED ([UserId] ASC, [FileId] ASC),
    CONSTRAINT [FK_UserImage_Image] FOREIGN KEY ([FileId]) REFERENCES [dbo].[Image] ([FileId]),
    CONSTRAINT [FK_UserImage_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
)
GO

ALTER TABLE [dbo].[SportCategory]
ADD [Image] INT NULL
GO

ALTER TABLE [dbo].[SportCategory]
ADD CONSTRAINT [FK_SportCategory_Image] FOREIGN KEY ([Image]) REFERENCES [dbo].[Image] ([FileId])
GO

ALTER TABLE [dbo].[SportKind]
ADD [Image] INT NULL
GO

ALTER TABLE [dbo].[SportKind]
ADD CONSTRAINT [FK_SportKind_Image] FOREIGN KEY ([Image]) REFERENCES [dbo].[Image] ([FileId])
GO

ALTER TABLE [dbo].[Hall]
ADD [Image] INT NULL
GO

ALTER TABLE [dbo].[Hall]
ADD CONSTRAINT [FK_Hall_Image] FOREIGN KEY ([Image]) REFERENCES [dbo].[Image] ([FileId])
GO