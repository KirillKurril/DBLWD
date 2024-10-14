SELECT U.[username], COUNT(*) AS [orders count] 
FROM [dbo].[Order] AS O
	LEFT JOIN [dbo].[User] AS U ON O.[user_id] = U.id
GROUP BY U.[username]
ORDER BY CAST(SUBSTRING(U.[username], 5, LEN(U.[username])) AS INT);