SELECT O.id AS OrderID, P.name AS ProductName, C.name AS CategoryName
FROM [Order] AS O
LEFT JOIN [Product] AS P ON O.product_id = P.id
LEFT JOIN [Category] AS C ON P.category_id = C.id;
