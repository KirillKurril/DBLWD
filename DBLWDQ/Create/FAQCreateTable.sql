CREATE TABLE [FAQ](
[id] INT IDENTITY(1,1) PRIMARY KEY,
[article_id] INT NOT NULL,
[question] NVARCHAR(100) NOT NULL
FOREIGN KEY ([article_id]) REFERENCES [Article]([id])
)