CREATE TABLE Vacancy(
[id] INT IDENTITY(1,1) PRIMARY KEY,
[title] NVARCHAR(100) DEFAULT 'Not definded',
[description] NVARCHAR(MAX) DEFAULT 'Not definded',
[requirements] NVARCHAR(MAX) DEFAULT 'Not definded',
[responsibilities] NVARCHAR(MAX) DEFAULT 'Not definded',
[location] NVARCHAR(254) DEFAULT 'Not definded',
[salary] DECIMAL(8,2) DEFAULT 500,
[image] NVARCHAR(MAX),
[created_at] DATETIME DEFAULT GETDATE() NOT NULL,
)