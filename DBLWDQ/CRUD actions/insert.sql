BEGIN TRANSACTION;

BEGIN TRY

    INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('��������� 1', '����� ���������� 1', '+375-XX-XXX-XX-X1');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('��������� 2', '����� ���������� 2', '+375-XX-XXX-XX-X2');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('��������� 3', '����� ���������� 3', '+375-XX-XXX-XX-X3');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('��������� 4', '����� ���������� 4', '+375-XX-XXX-XX-X4');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('��������� 5', '����� ���������� 5', '+375-XX-XXX-XX-X5');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('��������� 6', '����� ���������� 6', '+375-XX-XXX-XX-X6');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('��������� 7', '����� ���������� 7', '+375-XX-XXX-XX-X7');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('��������� 8', '����� ���������� 8', '+375-XX-XXX-XX-X8');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('��������� 9', '����� ���������� 9', '+375-XX-XXX-XX-X9');
	INSERT INTO [Supplier] ([name], [address], [phone]) VALUES ('��������� 10', '����� ���������� 10', '+375-XX-XXX-XX-10');

    COMMIT;
    PRINT '���������� ��������� �������';
END TRY
BEGIN CATCH
    ROLLBACK;
    PRINT '������: ' + ERROR_MESSAGE();
END CATCH;
