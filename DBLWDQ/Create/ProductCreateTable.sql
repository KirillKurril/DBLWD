CREATE TABLE  [Product]  (
     [id] INT PRIMARY KEY IDENTITY(1,1),
     [name] NVARCHAR(100) NOT NULL,
     [article_number] NVARCHAR(20) NOT NULL,
     [description] NVARCHAR(100) NOT NULL,
     [price_per_unit] DECIMAL(10, 2) DEFAULT 9.99,
     [image] NVARCHAR(255),
     [count] INT DEFAULT 5,
     [category_id] INT NOT NULL,
	 FOREIGN KEY ([category_id]) REFERENCES [Category]([id]) ON DELETE NO ACTION,
);
