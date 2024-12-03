-- Очистка таблицы (если нужно)
DELETE FROM [User];
DBCC CHECKIDENT ('[User]', RESEED, 0);

-- Вставка тестовых пользователей
INSERT INTO [User] (
    [Email],
    [Username],
    [Password], -- В реальном приложении пароли должны быть захешированы
    [FirstName],
    [LastName],
    [IsStaff],
    [IsSuperuser],
    [IsActive],
    [DateJoined]
)
VALUES 
    (
        'admin@example.com',
        'admin',
        'admin123', -- В реальном приложении должен быть хеш
        'Admin',
        'User',
        1, -- IsStaff
        1, -- IsSuperuser
        1, -- IsActive
        GETDATE()
    ),
    (
        'john.doe@example.com',
        'johndoe',
        'user123',
        'John',
        'Doe',
        0, -- IsStaff
        0, -- IsSuperuser
        1, -- IsActive
        GETDATE()
    ),
    (
        'jane.smith@example.com',
        'janesmith',
        'user456',
        'Jane',
        'Smith',
        0, -- IsStaff
        0, -- IsSuperuser
        1, -- IsActive
        GETDATE()
    ),
    (
        'staff.user@example.com',
        'staffuser',
        'staff123',
        'Staff',
        'User',
        1, -- IsStaff
        0, -- IsSuperuser
        1, -- IsActive
        GETDATE()
    ),
    (
        'inactive@example.com',
        'inactive',
        'inactive123',
        'Inactive',
        'User',
        0, -- IsStaff
        0, -- IsSuperuser
        0, -- IsActive
        GETDATE()
    );

-- Проверка вставленных данных
SELECT * FROM [User];
