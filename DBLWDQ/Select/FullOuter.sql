SELECT O.id AS OrderID, P.name AS ProductName, C.name AS CategoryName
FROM [DBLWD].[dbo].[Order] AS O
FULL JOIN [DBLWD].[dbo].[Product] AS P ON O.product_id = P.id
FULL JOIN [DBLWD].[dbo].[Category] AS C ON P.category_id = C.id;
