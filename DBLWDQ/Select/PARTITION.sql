SELECT 
		U.[username],
		O.[quantity], 
		P.[name],
		P.[price_per_unit],
		SUM( P.[price_per_unit] * O.[quantity]) OVER (PARTITION BY U.[username] ORDER BY U.[username] DESC) AS [cart cost]
		--write another atribute containing SUM( P.[price_per_unit] * O.[quantity]) to every tuple
		--all the tuples combines to groups by PARTITION BY U.[username]
		--inside groups all the tuples sorted by ORDER BY U.[username] DESC
FROM [dbo].[Order] AS O
	JOIN [dbo].[User] AS U ON O.[user_id] = U.[id]
	JOIN [dbo].[Product] AS P ON O.[product_id] = P.[id]


