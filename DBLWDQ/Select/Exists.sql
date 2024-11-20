SELECT P.id, P.name 
FROM Product P
WHERE EXISTS (
    SELECT 1 
    FROM [Order] O
    WHERE O.product_id = P.id
    );
