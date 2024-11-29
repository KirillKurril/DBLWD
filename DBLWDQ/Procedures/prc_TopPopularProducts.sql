USE DBLWD;
GO

CREATE PROCEDURE prc_TopPopularProducts
    @TopN INT = 10,
    @CategoryID INT = NULL
AS
BEGIN
    SELECT 
        p.[name],
        p.article_number,
        COUNT(o.id) as order_count,
        SUM(o.quantity) as total_quantity
    FROM [Product] p
    LEFT JOIN [Order] o ON p.id = o.product_id
    WHERE (@CategoryID IS NULL OR p.category_id = @CategoryID)
    GROUP BY p.[name], p.article_number
    ORDER BY order_count DESC
    OFFSET 0 ROWS
    FETCH NEXT @TopN ROWS ONLY
END;
GO