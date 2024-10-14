CREATE TABLE Partner (
    id INT IDENTITY(1,1) PRIMARY KEY, 
    name NVARCHAR(100),
    image NVARCHAR(255) NOT NULL DEFAULT 'images/partners/default.png',
    website NVARCHAR(255) NOT NULL DEFAULT 'https://minsk-lada.by'    
);
