SELECT O.id AS OrderID, P.name AS ProductName, C.name AS CategoryName
FROM [Order] AS O
INNER JOIN [Product] AS P ON O.product_id = P.id
INNER JOIN [Category] AS C ON P.category_id = C.id;
