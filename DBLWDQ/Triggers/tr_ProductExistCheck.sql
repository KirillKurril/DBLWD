USE DBLWD;
GO

CREATE TRIGGER tr_ProductExistCheck
ON Product
INSTEAD OF UPDATE, DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM deleted d
        LEFT JOIN Product p ON d.id = p.id
        WHERE p.id IS NULL
    )
    BEGIN
        THROW 50002, 'Product ID being deleted does not exist', 1;
        RETURN;
    END

    IF EXISTS (
        SELECT 1
        FROM inserted i
        LEFT JOIN Product p ON i.id = p.id
        WHERE p.id IS NULL
    )
    BEGIN
        THROW 50003, 'Product ID being updated does not exist', 1;
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM inserted) 
    BEGIN
        UPDATE p
        SET 
            [name] = i.[name],
            article_number = i.article_number,
            [description] = i.[description],
            price_per_unit = i.price_per_unit,
            [image] = i.[image],
            category_id = i.category_id,
            [count] = i.[count]
        FROM Product p
        INNER JOIN inserted i ON p.id = i.id;
    END

    ELSE 
    BEGIN
        DELETE p
        FROM Product p
        INNER JOIN deleted d ON p.id = d.id;
    END
END;
GO