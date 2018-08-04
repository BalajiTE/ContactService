USE [EvolentContacts]
GO

/* DELETE existing data from [dbo].[EvolentContact] table */
DELETE FROM [dbo].[EvolentContact]
DBCC CHECKIDENT (EvolentContact, RESEED, 0)

/* START OF: INSERT DATA INTO BWIInstanceDefinition TABLE */
SET IDENTITY_INSERT [dbo].[EvolentContact] ON

INSERT INTO [dbo].[EvolentContact] ([ID],[FirstName],[LastName],[Email],[Phone],[CreatedDate],[ModifiedDate],[Status])
     VALUES (1, 'Contact One', 'Last One', 'one.last@gmail.com', '1112221111', GETDATE(), NULL, 1)

INSERT INTO [dbo].[EvolentContact] ([ID],[FirstName],[LastName],[Email],[Phone],[CreatedDate],[ModifiedDate],[Status])
     VALUES (2, 'Contact Two', 'Last Two', 'two.last@gmail.com', '2223332222', GETDATE(), NULL, 1)

INSERT INTO [dbo].[EvolentContact] ([ID],[FirstName],[LastName],[Email],[Phone],[CreatedDate],[ModifiedDate],[Status])
     VALUES (3, 'Contact Three', 'Last Three', 'three.last@gmail.com', '3334443333', GETDATE(), NULL, 1)

INSERT INTO [dbo].[EvolentContact] ([ID],[FirstName],[LastName],[Email],[Phone],[CreatedDate],[ModifiedDate],[Status])
     VALUES (4, 'Contact Four', 'Last Four', 'four.last@gmail.com', '4445554444', GETDATE(), NULL, 1)

INSERT INTO [dbo].[EvolentContact] ([ID],[FirstName],[LastName],[Email],[Phone],[CreatedDate],[ModifiedDate],[Status])
     VALUES (5, 'Contact Five', 'Last Five', 'five.last@gmail.com', '5556665555', GETDATE(), NULL, 1)

SET IDENTITY_INSERT [dbo].[EvolentContact] OFF
/* END OF: INSERT DATA INTO BWIInstanceDefinition TABLE */
GO


