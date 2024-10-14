SELECT U.[username], COUNT(*) AS order_count
FROM [dbo].[Order] AS O
	JOIN [dbo].[User] AS U ON O.[user_id] = U.[id]
GROUP BY U.[username]
HAVING COUNT(*) > 2;


