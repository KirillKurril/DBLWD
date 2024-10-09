BEGIN TRANSACTION;

BEGIN TRY
    -- Шаг 1: Вставляем 10 записей в таблицу Article
    DECLARE @article_id1 INT, @article_id2 INT, @article_id3 INT, @article_id4 INT, @article_id5 INT;
    DECLARE @article_id6 INT, @article_id7 INT, @article_id8 INT, @article_id9 INT, @article_id10 INT;

    -- Вставка статей и получение их ID
    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('Заглавие FAQ  1', 'ответ статьи 1', '+375-XX-XXX-XX-X1');
    SET @article_id1 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('Заглавие FAQ  2', 'ответ статьи 2', '+375-XX-XXX-XX-X2');
    SET @article_id2 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('Заглавие FAQ  3', 'ответ статьи 3', '+375-XX-XXX-XX-X3');
    SET @article_id3 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('Заглавие FAQ  4', 'ответ статьи 4', '+375-XX-XXX-XX-X4');
    SET @article_id4 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('Заглавие FAQ  5', 'ответ статьи 5', '+375-XX-XXX-XX-X5');
    SET @article_id5 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('Заглавие FAQ  6', 'ответ статьи 6', '+375-XX-XXX-XX-X6');
    SET @article_id6 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('Заглавие FAQ  7', 'ответ статьи 7', '+375-XX-XXX-XX-X7');
    SET @article_id7 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('Заглавие FAQ  8', 'ответ статьи 8', '+375-XX-XXX-XX-X8');
    SET @article_id8 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('Заглавие FAQ  9', 'ответ статьи 9', '+375-XX-XXX-XX-X9');
    SET @article_id9 = SCOPE_IDENTITY();

    INSERT INTO [Article] ([title], [text], [photo]) 
    VALUES ('Заглавие FAQ  10', 'ответ статьи 10', '+375-XX-XXX-XX-X10');
    SET @article_id10 = SCOPE_IDENTITY();

    -- Шаг 2: Вставляем 10 записей в таблицу FAQ, используя ID статей
    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id1, 'Вопрос 1 по статье 1');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id2, 'Вопрос 2 по статье 2');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id3, 'Вопрос 3 по статье 3');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id4, 'Вопрос 4 по статье 4');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id5, 'Вопрос 5 по статье 5');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id6, 'Вопрос 6 по статье 6');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id7, 'Вопрос 7 по статье 7');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id8, 'Вопрос 8 по статье 8');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id9, 'Вопрос 9 по статье 9');

    INSERT INTO [FAQ] ([article_id], [question]) 
    VALUES (@article_id10, 'Вопрос 10 по статье 10');

    -- Фиксируем транзакцию
    COMMIT;
    PRINT 'Транзакция выполнена успешно';

END TRY
BEGIN CATCH
    -- В случае ошибки откатываем транзакцию
    ROLLBACK;
    PRINT 'Ошибка: ' + ERROR_MESSAGE();
END CATCH;
