CREATE TABLE Article (
    [id] INT PRIMARY KEY IDENTITY(1,1),
    [title] NVARCHAR(100) NOT NULL DEFAULT 'not defined',
    [text] NVARCHAR(MAX) NOT NULL DEFAULT 'not defined',
	[photo] NVARCHAR(MAX) NOT NULL DEFAULT 'default_image.png',
    [created_at] DATETIME DEFAULT GETDATE()
);
