
-- IDENTITY TABLES

CREATE TYPE [dbo].[Flag]
    FROM BIT NOT NULL;
GO

CREATE TYPE [dbo].[Name]
    FROM NVARCHAR (50) NOT NULL;
GO

CREATE TYPE [dbo].[Email]
    FROM NVARCHAR (100) NULL;
GO

CREATE TYPE [dbo].[Phone]
    FROM NVARCHAR (25) NULL;
GO

CREATE TABLE [dbo].[User]
(
    [UserId]               INT IDENTITY (1000, 1) NOT NULL,
    [UserName]             [dbo].[Name]           NOT NULL,
    [UserFirstName]		   [dbo].[Name]			NULL,
    [UserSurname]		   [dbo].[Name]			NULL,
    [Email]                [dbo].[Email]          NULL,
    [EmailConfirmed]       [dbo].[Flag]           NOT NULL,
    [PasswordHash]         NVARCHAR (100)         NULL,
    [SecurityStamp]        NVARCHAR (100)         NULL,
    [PhoneNumber]          [dbo].[Phone]          NULL,
    [PhoneNumberConfirmed] [dbo].[Flag]           NOT NULL,
    [TwoFactorEnabled]     [dbo].[Flag]           NOT NULL,
    [LockoutEndDateUtc]    DATETIME               NULL,
    [LockoutEnabled]       [dbo].[Flag]           NOT NULL,
    [AccessFailedCount]    INT                    NOT NULL,

    CONSTRAINT [PK_User_UserID] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [UK_User_UserName] UNIQUE NONCLUSTERED ([UserName] ASC)
)

CREATE TABLE [dbo].[Role] (
    [Id]   INT IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[UserRole] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
)
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[UserRole]([RoleId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[UserRole]([UserId] ASC);
GO

CREATE TABLE [dbo].[UserLogin] (
    [UserId]        INT NOT NULL,
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_UserLogin_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
)
GO

CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[UserLogin]([UserId] ASC);
GO

CREATE TABLE [dbo].[UserClaim] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [UserId]     INT NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClaim_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
)
GO

CREATE NONCLUSTERED INDEX [IX_User_Id]
    ON [dbo].[UserClaim]([UserId] ASC);

GO

-- BUSSINESS LOGIC TABLES

CREATE TABLE [dbo].[Coach] (
    [CoachId] INT NOT NULL,
    CONSTRAINT [PK_Coach] PRIMARY KEY CLUSTERED ([CoachId] ASC),
    CONSTRAINT [FK_Coach_User] FOREIGN KEY ([CoachId]) REFERENCES [dbo].[User] ([UserId])
)
GO

CREATE TABLE [dbo].[Client] (
    [ClientId]  INT  NOT NULL,
    [GroupId]   INT  NULL,
    [BirthDate] DATE NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY NONCLUSTERED ([ClientId] ASC),
    CONSTRAINT [FK_Client_User] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [FK_Client_SportGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[SportGroup] ([GroupId])
)
GO

CREATE TABLE [dbo].[SportCategory] (
    [SportCategoryId]   INT           IDENTITY (1, 1) NOT NULL,
    [SportCategoryName] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_SportCategory] PRIMARY KEY CLUSTERED ([SportCategoryId] ASC)
)
GO

CREATE TABLE [dbo].[SportKind] (
    [SportId]         INT           IDENTITY (1, 1) NOT NULL,
    [SportName]       VARCHAR (MAX) NOT NULL,
    [SportCategoryId] INT           NULL,
    CONSTRAINT [PK_SportKind] PRIMARY KEY CLUSTERED ([SportId] ASC),
    CONSTRAINT [FK_SportKind_SportCategory] FOREIGN KEY ([SportCategoryId]) REFERENCES [dbo].[SportCategory] ([SportCategoryId])
)
GO

CREATE TABLE [dbo].[SportGroup] (
    [GroupId]         INT IDENTITY (1, 1) NOT NULL,
    [GroupMaxMembers] INT DEFAULT 10,
    [CoachId]         INT NOT NULL,
    [SportId]         INT NOT NULL,
    CONSTRAINT [PK_SportGroup] PRIMARY KEY CLUSTERED ([GroupId] ASC),
    CONSTRAINT [FK_SportGroup_Coach] FOREIGN KEY ([CoachId]) REFERENCES [dbo].[Coach] ([CoachId]),
    CONSTRAINT [FK_SportGroup_SportId] FOREIGN KEY ([SportId]) REFERENCES [dbo].[SportKind] ([SportId])
)
GO

CREATE TABLE [dbo].[Abonement] (
    [AbonementId]        INT           IDENTITY (1, 1) NOT NULL,
    [AbonementName]      VARCHAR (MAX) NOT NULL,
    [AbonementDuring]    INT           DEFAULT (30) NOT NULL,
    [AbonementTrainings] INT           DEFAULT (8) NOT NULL,
    CONSTRAINT [PK_Abonement] PRIMARY KEY CLUSTERED ([AbonementId] ASC)
)
GO

CREATE TABLE [dbo].[SportAbonement] (
    [AbonementId]      INT NOT NULL,
    [SportId]          INT NOT NULL,
    CONSTRAINT [PK_SportAbonement] PRIMARY KEY CLUSTERED ([AbonementId] ASC, [SportId] ASC),
    CONSTRAINT [FK_SportAbonement_Abonement] FOREIGN KEY ([AbonementId]) REFERENCES [dbo].[Abonement] ([AbonementId]),
    CONSTRAINT [FK_SportAbonement_Sport] FOREIGN KEY ([SportId]) REFERENCES [dbo].[SportKind] ([SportId])
)
GO

CREATE TABLE [dbo].[Price] (
    [PriceId]     INT   IDENTITY (1, 1) NOT NULL,
    [Price]       MONEY DEFAULT (100) NOT NULL,
    [AbonementId] INT   NOT NULL,
    CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED ([PriceId] ASC),
    CONSTRAINT [FK_Price_Abonement] FOREIGN KEY ([AbonementId]) REFERENCES [dbo].[Abonement] ([AbonementId])
)
GO

CREATE TABLE [dbo].[AbonementOrder] (
    [OrderId]  INT IDENTITY (1, 1) NOT NULL,
    [PriceId]  INT NOT NULL,
    [ClientId] INT NOT NULL,
    CONSTRAINT [PK_AbonementOrder] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_AbonementOrder_Price] FOREIGN KEY ([PriceId]) REFERENCES [dbo].[Price] ([PriceId]),
    CONSTRAINT [FK_AbonementOrder_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([ClientId])
)
GO

CREATE TABLE [dbo].[Adress] (
    [AdressId]         INT           IDENTITY (1, 1) NOT NULL,
    [AdressStreet]     VARCHAR (MAX) NULL,
    [AdressCity]       VARCHAR (MAX) NULL,
    [AdressState]      VARCHAR (MAX) NULL,
    [AdressBuilding]   VARCHAR (10)  NULL,
    [AdressApartament] VARCHAR (10)  NULL,
    [AdressLatitude]   VARCHAR (15)  NULL,
    [AdressLongtitude] VARCHAR (15)  NULL,
    CONSTRAINT [PK_Adress] PRIMARY KEY CLUSTERED ([AdressId] ASC)
)
GO

CREATE TABLE [dbo].[Hall] (
    [HallId]   INT IDENTITY (1, 1) NOT NULL,
    [AdressId] INT NULL,
    CONSTRAINT [PK_Hall] PRIMARY KEY CLUSTERED ([HallId] ASC),
    CONSTRAINT [FK_Hall_Adress] FOREIGN KEY ([AdressId]) REFERENCES [dbo].[Adress] ([AdressId])
)
GO

CREATE TABLE [dbo].[Shedule] (
    [ShedulId]    INT          IDENTITY (1, 1) NOT NULL,
    [SheduleDay]  VARCHAR (15) DEFAULT ('Monday') NOT NULL,
    [SheduleTime] TIME (7)     DEFAULT ('12:00:00') NOT NULL,
    [GroupId]     INT          NULL,
    [HallId]      INT          NULL,
    CONSTRAINT [PK_Shedule] PRIMARY KEY CLUSTERED ([ShedulId] ASC),
    CONSTRAINT [FK_Shedule_SportGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[SportGroup] ([GroupId]),
    CONSTRAINT [FK_Shedule_Hall] FOREIGN KEY ([HallId]) REFERENCES [dbo].[Hall] ([HallId])
)
GO

CREATE TABLE [dbo].[ClientAbonement] (
    [ClientId]    INT NOT NULL,
    [AbonementId] INT NOT NULL,
    CONSTRAINT [PK_ClientAbonement] PRIMARY KEY CLUSTERED ([ClientId] ASC, [AbonementId] ASC),
    CONSTRAINT [FK_ClientAbonement_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([ClientId]),
    CONSTRAINT [FK_ClientAbonement_Abonement] FOREIGN KEY ([AbonementId]) REFERENCES [dbo].[Abonement] ([AbonementId])
)
GO

CREATE TABLE [dbo].[Visiting] (
    [VisitingId]       INT      IDENTITY (1, 1) NOT NULL,
    [VisitingDateTime] DATETIME DEFAULT (getdate()) NOT NULL,
    [SheduleId]        INT      NOT NULL,
    [ClientId]         INT      NOT NULL,
    CONSTRAINT [PK_Visiting] PRIMARY KEY CLUSTERED ([VisitingId] ASC),
    CONSTRAINT [FK_Visiting_Shedule] FOREIGN KEY ([SheduleId]) REFERENCES [dbo].[Shedule] ([ShedulId]),
    CONSTRAINT [FK_Visiting_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([ClientId])
)
GO



