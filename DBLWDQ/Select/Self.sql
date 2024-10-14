SELECT O1.id AS OrderID, O1.[date] AS OrderDate, O2.[quantity] AS [joined order quantity]
FROM [Order] AS O1
INNER JOIN [Order] AS O2 ON O1.[user_id] = O2.[user_id]
ORDER BY [joined order quantity]