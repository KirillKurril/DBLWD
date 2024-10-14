SELECT * FROM [dbo].[Order]
UNION --ALL if it required to save duplicates
SELECT * FROM [dbo].[ArchivedOrder]