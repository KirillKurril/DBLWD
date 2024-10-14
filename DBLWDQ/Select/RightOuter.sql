SELECT O.id AS OrderID, P.name AS ProductName, C.name AS CategoryName
FROM [Order] AS O
RIGHT JOIN [Product] AS P ON O.product_id = P.id
RIGHT JOIN [Category] AS C ON P.category_id = C.id;
