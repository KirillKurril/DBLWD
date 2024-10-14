SELECT O.id AS OrderID, P.name AS ProductName
FROM [Order] AS O
CROSS JOIN [Product] AS P;
