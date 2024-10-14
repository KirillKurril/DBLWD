CREATE TABLE [Category] (
    [id] INT PRIMARY KEY IDENTITY(1,1),
    [name] NVARCHAR(100) NOT NULL,
    [description] NVARCHAR(200) DEFAULT 'category description'
)
