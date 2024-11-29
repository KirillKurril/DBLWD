USE DBLWD;
GO

CREATE TRIGGER TR_OrderInsertValidation
ON [dbo].[Order]
INSTEAD OF INSERT
AS
BEGIN
   IF EXISTS(
      SELECT 1
      FROM inserted AS I
        JOIN [Product] AS P ON I.product_id = P.Id
        WHERE I.Quantity > P.[count]
   )
   BEGIN
      THROW 50001, 'Not enough products in stock', 16;
      RETURN;
   END

   INSERT INTO [dbo].[Order] (
        [date],
        [quantity],
        [product_id],
        [user_id],
        [pickup_point_id], 
        [promocode_id]
    )
    SELECT 
        [date], 
        [quantity], 
        [product_id], 
        [user_id], 
        [pickup_point_id], 
        [promocode_id]
    FROM inserted;
END;
GO
