SELECT U.[username],
       (SELECT COUNT(*)
        FROM [dbo].[Order] AS O 
        WHERE O.[user_id] = U.[id]) AS [order_count]
FROM [dbo].[User] AS U;
