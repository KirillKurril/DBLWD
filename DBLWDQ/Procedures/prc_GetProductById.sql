USE DBLWD;
GO

CREATE PROCEDURE prc_GetProductById
    @ProductID INT
AS
BEGIN
    IF @ProductID IS NULL
    BEGIN
        THROW 50020, 'ProductID cannot be NULL', 1;
        RETURN;
    END

    SELECT 
        p.id,
        p.name,
        p.article_number,
        p.[description],
        p.price_per_unit,
        p.[image],
        p.category_id,
        c.name AS category_name,
        p.[count]
    FROM Product p
    LEFT JOIN Category c ON p.category_id = c.id
    WHERE p.id = @ProductID;
END;
GO