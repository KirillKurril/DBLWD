USE DBLWD;
GO

CREATE PROCEDURE prc_GetProductsList
    @CategoryID INT = NULL,   
    @SearchTerm NVARCHAR(50) = NULL,
    @MinPrice DECIMAL(10,2) = NULL,
    @MaxPrice DECIMAL(10,2) = NULL,   
    @InStock BIT = NULL               
AS
BEGIN
    SELECT 
        p.id,
        p.name,
        p.article_number,
        p.[description],
        p.price_per_unit,
        p.[image],
        p.category_id,
        c.name AS category_name,
        p.supply_id,
        s.name AS supplier_name,
        m.name AS manufacturer_name,
        p.[count]
    FROM Product p
    LEFT JOIN Category c ON p.category_id = c.id
    LEFT JOIN Supply sup ON sup.id = p.supply_id
    LEFT JOIN Manufacturer m ON sup.manufacturer_id = m.id
    LEFT JOIN Supplier s ON sup.supplier_id = s.id
    WHERE 1=1
        AND (@CategoryID IS NULL OR p.category_id = @CategoryID)
        AND (@SearchTerm IS NULL 
            OR p.name LIKE '%' + @SearchTerm + '%' 
            OR p.article_number LIKE '%' + @SearchTerm + '%'
            OR p.[description] LIKE '%' + @SearchTerm + '%')
        AND (@MinPrice IS NULL OR p.price_per_unit >= @MinPrice)
        AND (@MaxPrice IS NULL OR p.price_per_unit <= @MaxPrice)
        AND (@InStock IS NULL OR (@InStock = 1 AND p.[count] > 0) OR (@InStock = 0 AND p.[count] <= 0))
    ORDER BY p.name;
END;
GO