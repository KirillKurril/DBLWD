BEGIN TRY
	BEGIN TRANSACTION
	INSERT INTO [DBLWD].[dbo].[ArchivedOrder] ([date], [quantity], [product_id], [pickup_point_id], [promocode_id])
	SELECT [date], [quantity], [product_id], [pickup_point_id], [promocode_id]
	FROM [dbo].[Order] AS O
		JOIN [dbo].[Promocode] AS P ON O.[promocode_id] = P.[id]
	WHERE	O.[date] < DATEADD(MONTH, -1, GETDATE())
			OR P.[expiration] < GETDATE()
	COMMIT;
END TRY
BEGIN CATCH
	ROLLBACK;
END CATCH

