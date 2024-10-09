SELECT [username], [email],
	CASE [is_staff]
		WHEN 0 THEN 'customer'
		WHEN 1 THEN 'administrator'
		ELSE 'unknown'
	END AS [user status]
FROM dbo.[User]