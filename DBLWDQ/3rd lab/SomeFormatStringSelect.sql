SELECT CONCAT([first_name], N' ', [last_name]) AS [full_name], [email]
FROM dbo.[User]
WHERE LOWER(LEFT([first_name], 1)) = 'j' 
AND [first_name] LIKE N'[Jj]%' -- рн фе яюлне мн ашярпее онрнлс врн опедхйюр