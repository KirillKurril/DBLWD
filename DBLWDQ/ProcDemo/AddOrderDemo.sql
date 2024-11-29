DECLARE @CurrentDate DATETIME = GETDATE()

EXECUTE prc_AddOrder
    @OrderDate = @CurrentDate,
    @ProductId = 25,
    @OrderQuantity = 5,
    @UserId = 2,
    @PickupPointId = 1

SELECT * FROM [Order]
ORDER BY [date] DESC;