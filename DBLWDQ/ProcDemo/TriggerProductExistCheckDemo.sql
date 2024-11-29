USE DBLWD;
GO

UPDATE Product
SET name = 'Test Product'
WHERE id = 999;

DELETE FROM Product
WHERE id = 999;

UPDATE Product
SET name = 'Updated Product Name'
WHERE id = 10;

SELECT * FROM Product WHERE id = 10;

DELETE FROM Product
WHERE id = 2;

SELECT * FROM Product WHERE id = 2;
