-- Очистка таблиц
DELETE FROM [Partner];
DBCC CHECKIDENT ('[Partner]', RESEED, 0);

DELETE FROM [Manufacturer];
DBCC CHECKIDENT ('[Manufacturer]', RESEED, 0);

DELETE FROM [Supplier];
DBCC CHECKIDENT ('[Supplier]', RESEED, 0);

-- Вставка партнеров
INSERT INTO [Partner] (
    [Name],
    [Image],
    [Website]
)
VALUES 
    (
        'TechPartner',
        'images/partners/techpartner.png',
        'https://techpartner.com'
    ),
    (
        'SmartSolutions',
        'images/partners/smartsolutions.png',
        'https://smartsolutions.com'
    ),
    (
        'DigiCorp',
        'images/partners/digicorp.png',
        'https://digicorp.com'
    ),
    (
        'InnoTech',
        'images/partners/innotech.png',
        'https://innotech.com'
    ),
    (
        'FutureNet',
        'images/partners/futurenet.png',
        'https://futurenet.com'
    );

-- Вставка производителей
INSERT INTO [Manufacturer] (
    [Name],
    [Address],
    [Phone]
)
VALUES 
    (
        'TechCorp',
        'ул. Производственная 1, Минск',
        '+375291234567'
    ),
    (
        'ElectroSystems',
        'ул. Заводская 15, Гомель',
        '+375332345678'
    ),
    (
        'SmartTech',
        'пр. Независимости 45, Минск',
        '+375443456789'
    ),
    (
        'DigiPro',
        'ул. Промышленная 78, Брест',
        '+375254567890'
    ),
    (
        'InnovateTech',
        'ул. Фабричная 23, Гродно',
        '+375295678901'
    );

-- Вставка поставщиков
INSERT INTO [Supplier] (
    [Name],
    [Address],
    [Phone]
)
VALUES 
    (
        'ТехноСклад',
        'ул. Логистическая 10, Минск',
        '+375291111111'
    ),
    (
        'СмартЛогистик',
        'ул. Складская 22, Гомель',
        '+375332222222'
    ),
    (
        'ДиджиталПоставка',
        'пр. Дзержинского 91, Минск',
        '+375443333333'
    ),
    (
        'ТехИмпорт',
        'ул. Транспортная 45, Брест',
        '+375254444444'
    ),
    (
        'ЛогистикПро',
        'ул. Доставки 67, Гродно',
        '+375295555555'
    );

-- Проверка вставленных данных
SELECT * FROM [Partner];
SELECT * FROM [Manufacturer];
SELECT * FROM [Supplier];
