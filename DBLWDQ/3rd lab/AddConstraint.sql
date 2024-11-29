WITH ManufacturerStats AS (
    SELECT 
        username,
        manufacturer,
        COUNT(*) AS OrderCount
    FROM UserManufacturerTable
    GROUP BY username, manufacturer
),
MaxManufacturer AS (
    SELECT 
        username,
        MAX(OrderCount) AS MaxOrders
    FROM ManufacturerStats
    GROUP BY username
)
SELECT 
    ms.username,
    ms.manufacturer,
    ms.OrderCount AS TotalOrders
FROM ManufacturerStats ms
JOIN MaxManufacturer mm 
    ON ms.username = mm.username AND ms.OrderCount = mm.MaxOrders
ORDER BY ms.username;
