CREATE TABLE [dbo].[Client]
(
    ClientId INT NOT NULL,
    GroupId INT NULL,
    BirthDate DATE NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY NONCLUSTERED (ClientId),
    CONSTRAINT [FK_Client_User] FOREIGN KEY (ClientId)
	   REFERENCES [dbo].[User] (UserId),
    CONSTRAINT [FK_Client_SportGroup] FOREIGN KEY (GroupId)
	   REFERENCES [dbo].[SportGroup] (GroupId)
)
GO


ALTER TABLE [dbo].[Visiting]
    DROP CONSTRAINT [FK_Visiting_User] ;
GO

ALTER TABLE [dbo].[Visiting]
    DROP COLUMN ClientId ;
GO

ALTER TABLE [dbo].[Visiting]
    ADD [ClientId] INT NOT NULL,
    CONSTRAINT [FK_Visiting_Client] FOREIGN KEY (ClientId)
	   REFERENCES [dbo].[Client] (ClientId) ;
GO

DROP TABLE [dbo].[UserAbonement];
GO

CREATE TABLE [dbo].[ClientAbonement] (
    [ClientId]      INT NOT NULL,
    [AbonementId] INT NOT NULL,
    CONSTRAINT [PK_ClientAbonement] PRIMARY KEY CLUSTERED ([ClientId] ASC, [AbonementId] ASC),
    CONSTRAINT [FK_ClientAbonement_Client] FOREIGN KEY ([ClientId])	  
	   REFERENCES [dbo].[Client] ([ClientId]),
    CONSTRAINT [FK_ClientAbonement_Abonement] FOREIGN KEY ([AbonementId]) 
	   REFERENCES [dbo].[Abonement] ([AbonementId])
);
GO

ALTER TABLE [dbo].[AbonementOrder]
    DROP CONSTRAINT [FK_AbonementOrder_User] ;
GO

ALTER TABLE [dbo].[AbonementOrder]
    DROP COLUMN ClientId ;
GO

ALTER TABLE [dbo].[AbonementOrder]
    ADD [ClientId] INT NOT NULL,
    CONSTRAINT [FK_AbonementOrder_Client] FOREIGN KEY (ClientId)
	   REFERENCES [dbo].[Client] (ClientId) ;
GO