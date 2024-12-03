-- Очистка таблицы
DELETE FROM [Supply];
DBCC CHECKIDENT ('[Supply]', RESEED, 0);

-- Предполагаем, что у нас есть продукты с ID от 1 до 5
-- И используем созданные ранее Supplier и Manufacturer с ID от 1 до 5

-- Вставка поставок
INSERT INTO [Supply] (
    [ProductId],
    [SupplierId],
    [ManufacturerId]
)
VALUES 
    (
        1, -- ProductId
        1, -- ТехноСклад
        1  -- TechCorp
    ),
    (
        2, -- ProductId
        2, -- СмартЛогистик
        2  -- ElectroSystems
    ),
    (
        3, -- ProductId
        3, -- ДиджиталПоставка
        3  -- SmartTech
    ),
    (
        4, -- ProductId
        4, -- ТехИмпорт
        4  -- DigiPro
    ),
    (
        5, -- ProductId
        5, -- ЛогистикПро
        5  -- InnovateTech
    ),
    (
        1, -- ProductId (тот же продукт, но другой поставщик и производитель)
        2, -- СмартЛогистик
        3  -- SmartTech
    ),
    (
        2, -- ProductId
        3, -- ДиджиталПоставка
        4  -- DigiPro
    ),
    (
        3, -- ProductId
        4, -- ТехИмпорт
        5  -- InnovateTech
    ),
    (
        4, -- ProductId
        5, -- ЛогистикПро
        1  -- TechCorp
    ),
    (
        5, -- ProductId
        1, -- ТехноСклад
        2  -- ElectroSystems
    );

-- Проверка вставленных данных
SELECT 
    s.Id as SupplyId,
    p.Name as ProductName,
    sup.Name as SupplierName,
    m.Name as ManufacturerName
FROM [Supply] s
JOIN Product p ON s.ProductId = p.Id
JOIN Supplier sup ON s.SupplierId = sup.Id
JOIN Manufacturer m ON s.ManufacturerId = m.Id;
