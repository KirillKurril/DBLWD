BEGIN TRANSACTION;

BEGIN TRY
    -- ��� 1: ��������� 10 ������� � ������� Article
    DECLARE @article_id1 INT, @article_id2 INT, @article_id3 INT, @article_id4 INT, @article_id5 INT;
    DECLARE @article_id6 INT, @article_id7 INT, @article_id8 INT, @article_id9 INT, @article_id10 INT;

    -- ������� ������ � ��������� �� ID
    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('�������� FAQ  1', '����� ������ 1', '+375-XX-XXX-XX-X1');
    SET @article_id1 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('�������� FAQ  2', '����� ������ 2', '+375-XX-XXX-XX-X2');
    SET @article_id2 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('�������� FAQ  3', '����� ������ 3', '+375-XX-XXX-XX-X3');
    SET @article_id3 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('�������� FAQ  4', '����� ������ 4', '+375-XX-XXX-XX-X4');
    SET @article_id4 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('�������� FAQ  5', '����� ������ 5', '+375-XX-XXX-XX-X5');
    SET @article_id5 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('�������� FAQ  6', '����� ������ 6', '+375-XX-XXX-XX-X6');
    SET @article_id6 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('�������� FAQ  7', '����� ������ 7', '+375-XX-XXX-XX-X7');
    SET @article_id7 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('�������� FAQ  8', '����� ������ 8', '+375-XX-XXX-XX-X8');
    SET @article_id8 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('�������� FAQ  9', '����� ������ 9', '+375-XX-XXX-XX-X9');
    SET @article_id9 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('�������� FAQ  10', '����� ������ 10', '+375-XX-XXX-XX-X10');
    SET @article_id10 = SCOPE_IDENTITY();

    -- ��� 2: ��������� 10 ������� � ������� FAQ, ��������� ID ������
    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id1, '������ 1 �� ������ 1');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id2, '������ 2 �� ������ 2');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id3, '������ 3 �� ������ 3');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id4, '������ 4 �� ������ 4');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id5, '������ 5 �� ������ 5');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id6, '������ 6 �� ������ 6');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id7, '������ 7 �� ������ 7');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id8, '������ 8 �� ������ 8');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id9, '������ 9 �� ������ 9');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id10, '������ 10 �� ������ 10');

    -- ��������� ����������
    COMMIT;
    PRINT '���������� ��������� �������';

END TRY
BEGIN CATCH
    -- � ������ ������ ���������� ����������
    ROLLBACK;
    PRINT '������: ' + ERROR_MESSAGE();
END CATCH;
