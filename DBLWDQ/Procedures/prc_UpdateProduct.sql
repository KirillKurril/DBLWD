USE DBLWD;
GO

CREATE PROCEDURE prc_UpdateProduct
    @ProductID INT,
    @Name NVARCHAR(50) = NULL,
    @ArticleNumber NVARCHAR(20) = NULL,
    @Description NVARCHAR(100) = NULL,
    @PricePerUnit DECIMAL(10,2) = NULL,
    @Image NVARCHAR(255) = NULL,
    @CategoryID INT = NULL,
    @SupplyID INT = NULL,
    @Count INT = NULL
AS
BEGIN 
    UPDATE Product
    SET 
        [name] = CASE WHEN @Name IS NOT NULL THEN @Name ELSE [name] END,
        article_number = CASE WHEN @ArticleNumber IS NOT NULL THEN @ArticleNumber ELSE article_number END,
        [description] = CASE WHEN @Description IS NOT NULL THEN @Description ELSE [description] END,
        price_per_unit = CASE WHEN @PricePerUnit IS NOT NULL THEN @PricePerUnit ELSE price_per_unit END,
        [image] = CASE WHEN @Image IS NOT NULL THEN @Image ELSE [image] END,
        category_id = CASE WHEN @CategoryID IS NOT NULL THEN @CategoryID ELSE category_id END,
        supply_id = CASE WHEN @SupplyID IS NOT NULL THEN @SupplyID ELSE supply_id END,
        [count] = CASE WHEN @Count IS NOT NULL THEN @Count ELSE [count] END
    WHERE id = @ProductID;
END

