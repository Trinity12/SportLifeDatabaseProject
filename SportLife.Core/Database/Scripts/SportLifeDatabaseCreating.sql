CREATE TABLE [dbo].[Coach] (
    [CoachId] INT IDENTITY(1,1) NOT NULL,
    [UserId] INT NOT NULL,
    CONSTRAINT [PK_Coaches] PRIMARY KEY CLUSTERED ([CoachId] ASC),
    CONSTRAINT [FK_Coach_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

CREATE TABLE [dbo].[SportCategory] (
    [SportCategoryId]   INT           IDENTITY (1, 1) NOT NULL,
    [SportCategoryName] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_SportCategory] PRIMARY KEY CLUSTERED ([SportCategoryId] ASC)
)

CREATE TABLE [dbo].[SportKind] (
    [SportId]         INT           IDENTITY (1, 1) NOT NULL,
    [SportName]       VARCHAR (MAX) NOT NULL,
    [SportCategoryId] INT           NULL,
    CONSTRAINT [PK_SportKind] PRIMARY KEY CLUSTERED ([SportId] ASC),
    CONSTRAINT [FK_SportKind_SportCategory] FOREIGN KEY ([SportCategoryId]) REFERENCES [dbo].[SportCategory] ([SportCategoryId])
)

CREATE TABLE [dbo].[SportGroup] (
    [GroupId]         INT IDENTITY (1, 1) NOT NULL,
    [GroupMaxMembers] INT DEFAULT 10,
    [CoachId]         INT NOT NULL,
    [SportId]         INT NOT NULL,
    CONSTRAINT [PK_SportGroup] PRIMARY KEY CLUSTERED ([GroupId] ASC),
    CONSTRAINT [FK_SportGroup_Coach] FOREIGN KEY ([CoachId]) REFERENCES [dbo].[Coach] ([CoachId]),
    CONSTRAINT [FK_SportGroup_SportId] FOREIGN KEY ([SportId]) REFERENCES [dbo].[SportKind] ([SportId])
)

CREATE TABLE [dbo].[Abonement] (
    [AbonementId]        INT           IDENTITY (1, 1) NOT NULL,
    [AbonementName]      VARCHAR (MAX) NOT NULL,
    [AbonementDuring]    INT           DEFAULT (30) NOT NULL,
    [AbonementTrainings] INT           DEFAULT (8) NOT NULL,
    CONSTRAINT [PK_Abonement] PRIMARY KEY CLUSTERED ([AbonementId] ASC)
)

CREATE TABLE [dbo].[SportAbonement] (
    [AbonementId]      INT NOT NULL,
    [SportId]          INT NOT NULL,
    CONSTRAINT [PK_SportAbonement] PRIMARY KEY CLUSTERED ([AbonementId] ASC, [SportId] ASC),
    CONSTRAINT [FK_SportAbonement_Abonement] FOREIGN KEY ([AbonementId]) REFERENCES [dbo].[Abonement] ([AbonementId]),
    CONSTRAINT [FK_SportAbonement_Sport] FOREIGN KEY ([SportId]) REFERENCES [dbo].[SportKind] ([SportId])
)

CREATE TABLE [dbo].[Price] (
    [PriceId]     INT   IDENTITY (1, 1) NOT NULL,
    [Price]       MONEY DEFAULT (100) NOT NULL,
    [AbonementId] INT   NOT NULL,
    CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED ([PriceId] ASC),
    CONSTRAINT [FK_Price_Abonement] FOREIGN KEY ([AbonementId]) REFERENCES [dbo].[Abonement] ([AbonementId])
)

CREATE TABLE [dbo].[AbonementOrder] (
    [OrderId]  INT IDENTITY (1, 1) NOT NULL,
    [PriceId]  INT NOT NULL,
    [ClientId] INT NOT NULL,
    CONSTRAINT [PK_AbonementOrder] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_AbonementOrder_Price] FOREIGN KEY ([PriceId]) REFERENCES [dbo].[Price] ([PriceId]),
    CONSTRAINT [FK_AbonementOrder_User] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[User] ([UserId])
)

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

CREATE TABLE [dbo].[Hall] (
    [HallId]   INT IDENTITY (1, 1) NOT NULL,
    [AdressId] INT NULL,
    CONSTRAINT [PK_Hall] PRIMARY KEY CLUSTERED ([HallId] ASC),
    CONSTRAINT [FK_Hall_Adress] FOREIGN KEY ([AdressId]) REFERENCES [dbo].[Adress] ([AdressId])
)

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

CREATE TABLE [dbo].[UserAbonement] (
    [UserId]	 INT	NOT NULL,
    [AbonementId] INT NOT NULL,
    CONSTRAINT [PK_UserAbonement] PRIMARY KEY CLUSTERED ([UserId] ASC, [AbonementId] ASC),
    CONSTRAINT [FK_UserAbonement_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [FK_UserAbonement_Abonement] FOREIGN KEY ([AbonementId]) REFERENCES [dbo].[Abonement] ([AbonementId])
)

CREATE TABLE [dbo].[Visiting] (
    [VisitingId]       INT      IDENTITY (1, 1) NOT NULL,
    [VisitingDateTime] DATETIME DEFAULT (getdate()) NOT NULL,
    [SheduleId]        INT      NOT NULL,
    [ClientId]         INT      NOT NULL,
    CONSTRAINT [PK_Visiting] PRIMARY KEY CLUSTERED ([VisitingId] ASC),
    CONSTRAINT [FK_Visiting_Shedule] FOREIGN KEY ([SheduleId]) REFERENCES [dbo].[Shedule] ([ShedulId]),
    CONSTRAINT [FK_Visiting_User] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[User] ([UserId])
)

CREATE TABLE [dbo].[UserSportGroup] (
    [UserId]       INT NOT NULL,
    [GroupId]	    INT NOT NULL,
    CONSTRAINT [PK_UserClientGroup] PRIMARY KEY CLUSTERED ([UserId] ASC, [GroupId] ASC),
    CONSTRAINT [FK_UserClientGroup_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [FK_UserClientGroup_SportGroup] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SportGroup] ([GroupId])
);


