USE DBLWD;
GO

CREATE PROCEDURE prc_AddProduct
    @Name NVARCHAR(50),
    @ArticleNumber NVARCHAR(20),
    @Description NVARCHAR(100),
    @PricePerUnit DECIMAL(10,2) = NULL,
    @Image NVARCHAR(255) = NULL,
    @CategoryID INT,
    @SupplyID INT = NULL,
    @Count INT = NULL
AS
BEGIN 
   IF @Name IS NULL OR @ArticleNumber IS NULL OR @Description IS NULL OR @PricePerUnit IS NULL OR @CategoryID IS NULL
    BEGIN
        THROW 50010, 'Required parameters cannot be NULL', 1;
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Category WHERE id = @CategoryID)
    BEGIN
        THROW 50011, 'Category does not exist', 1;
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Product WHERE article_number = @ArticleNumber)
    BEGIN
        THROW 50013, 'Product with this article number already exists', 1;
        RETURN;
    END

    INSERT INTO Product (
        [name], 
        article_number, 
        [description], 
        price_per_unit, 
        [image], 
        category_id, 
        supply_id, 
        [count]
    )

    VALUES (
        @Name, 
        @ArticleNumber, 
        @Description, 
        @PricePerUnit, 
        @Image, 
        @CategoryID, 
        @SupplyID, 
        @Count
    );

    SELECT SCOPE_IDENTITY() AS NewProductID; --последнее значение IDENTITY()
END;
GO

