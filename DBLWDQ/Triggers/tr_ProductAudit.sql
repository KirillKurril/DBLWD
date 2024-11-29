USE DBLWD;
GO

CREATE TRIGGER tr_ProductAudit
ON [Product]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    INSERT INTO ProductAudit (
        ProductID,
        Action,
        ChangeDate,
        UserName,
        OldPrice,
        NewPrice
    )
    SELECT 
        COALESCE(i.id, d.id),  
        CASE 
            WHEN i.id IS NOT NULL AND d.id IS NULL THEN 'INSERT'
            WHEN i.id IS NOT NULL AND d.id IS NOT NULL THEN 'UPDATE'
            WHEN i.id IS NULL AND d.id IS NOT NULL THEN 'DELETE'
        END,
        GETDATE(),
        SYSTEM_USER,
        d.price_per_unit, 
        i.price_per_unit  
    FROM inserted i
    FULL OUTER JOIN deleted d ON i.id = d.id;
END;
GO
