USE DBLWD;
GO

EXECUTE prc_UpdateProduct
    @ProductID = 999,
    @Name = 'Test Product',
    @ArticleNumber = 'TEST001',
    @Description = 'Test Description',
    @PricePerUnit = 99.99,
    @Count = 10,
    @CategoryID = 1;

EXECUTE prc_UpdateProduct
    @ProductID = 10,
    @Name = 'Updated via Procedure',
    @PricePerUnit = 149.99;

SELECT * FROM Product WHERE id = 10;

EXECUTE prc_UpdateProduct
    @ProductID = 1,
    @Name = 'Fully Updated Product',
    @ArticleNumber = 'UPD001',
    @Description = 'Updated Description',
    @PricePerUnit = 199.99,
    @Image = 'new_image.jpg',
    @CategoryID = 2,
    @Count = 50;

SELECT * FROM Product WHERE id = 10;
