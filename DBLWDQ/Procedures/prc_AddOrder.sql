USE DBLWD;
GO

CREATE PROCEDURE prc_AddOrder
    @OrderDate DATETIME,
    @ProductId INT,
    @OrderQuantity INT,
    @UserId INT,
    @PickupPointId INT,
    @PromoCodeId INT = NULL
AS
BEGIN
	BEGIN TRY
		INSERT INTO [Order] (
			[date], 
			[quantity], 
			[product_id], 
			[user_id], 
			[pickup_point_id], 
			[promocode_id]
		)
		VALUES (
			@OrderDate, 
			@OrderQuantity, 
			@ProductId, 
			@UserId, 
			@PickupPointId, 
			@PromoCodeId
		);
	END TRY
	BEGIN CATCH
		PRINT 'Error number ' + CAST(ERROR_NUMBER() AS VARCHAR(10));
		PRINT 'Error message ' + ERROR_MESSAGE();
	END CATCH
END;
GO
