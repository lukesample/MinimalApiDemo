IF NOT EXISTS (SELECT 1 FROM [dbo].[User]) --check if we can select anything from the table

BEGIN

	INSERT INTO [dbo].[User] (Firstname, Lastname)
	VALUES ('Tim', 'Corey'),
	('Sue', 'Storm'),
	('John', 'Smith'),
	('Mary', 'Jones')

END
GO
