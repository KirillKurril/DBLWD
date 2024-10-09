BEGIN TRANSACTION;

BEGIN TRY

    INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('Поставщик 1', 'Адрес поставщика 1', '+375-XX-XXX-XX-X1');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('Поставщик 2', 'Адрес поставщика 2', '+375-XX-XXX-XX-X2');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('Поставщик 3', 'Адрес поставщика 3', '+375-XX-XXX-XX-X3');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('Поставщик 4', 'Адрес поставщика 4', '+375-XX-XXX-XX-X4');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('Поставщик 5', 'Адрес поставщика 5', '+375-XX-XXX-XX-X5');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('Поставщик 6', 'Адрес поставщика 6', '+375-XX-XXX-XX-X6');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('Поставщик 7', 'Адрес поставщика 7', '+375-XX-XXX-XX-X7');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('Поставщик 8', 'Адрес поставщика 8', '+375-XX-XXX-XX-X8');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('Поставщик 9', 'Адрес поставщика 9', '+375-XX-XXX-XX-X9');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('Поставщик 10', 'Адрес поставщика 10', '+375-XX-XXX-XX-10');

    COMMIT;
    PRINT 'Транзакция выполнена успешно';
END TRY
BEGIN CATCH
    ROLLBACK;
    PRINT 'Ошибка: ' + ERROR_MESSAGE();
END CATCH;
