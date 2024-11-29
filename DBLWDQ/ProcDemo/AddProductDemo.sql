USE DBLWD;
GO

EXECUTE prc_AddProduct
    @Name = 'Тормозной диск',
    @ArticleNumber = 'BR-001',
    @Description = 'Передний тормозной диск',
    @PricePerUnit = 2500.00,
    @CategoryID = 1;

EXECUTE prc_AddProduct
    @Name = 'Масляный фильтр',
    @ArticleNumber = 'OF-002',
    @Description = 'Масляный фильтр двигателя',
    @PricePerUnit = 350.00,
    @Image = 'oil_filter.jpg',
    @CategoryID = 2,
    @SupplyID = 1,
    @Count = 50;

EXECUTE prc_AddProduct
    @Name = 'Тест',
    @ArticleNumber = 'BR-001',
    @Description = 'Тест',
    @PricePerUnit = 100.00,
    @CategoryID = 1;

--SELECT * FROM Product ORDER BY [id] DESC