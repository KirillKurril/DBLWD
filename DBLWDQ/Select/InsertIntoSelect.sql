INSERT INTO [Order] (date, quantity, product_id, user_id, pickup_point_id, promocode_id)
SELECT GETDATE(), 1, p.id, 1, 1, 1
FROM 
    Product p
WHERE 
    NOT EXISTS (
        SELECT 1 
        FROM [Order] o
        WHERE o.product_id = p.id
    );
