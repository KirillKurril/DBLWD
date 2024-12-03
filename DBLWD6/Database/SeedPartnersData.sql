-- Очистка таблиц
DELETE FROM [Manufacturer];
DBCC CHECKIDENT ('[Manufacturer]', RESEED, 0);

DELETE FROM [Supplier];
DBCC CHECKIDENT ('[Supplier]', RESEED, 0);

-- Вставка производителей
INSERT INTO [Manufacturer] (
    [Name],
    [Description],
    [Country],
    [Address],
    [Phone],
    [Email],
    [Website]
)
VALUES 
    (
        'TechCorp Industries',
        'Ведущий производитель электроники и компьютерных комплектующих',
        'США',
        '123 Innovation Drive, Silicon Valley, CA 94025',
        '+1-555-0123',
        'contact@techcorp.com',
        'www.techcorp.com'
    ),
    (
        'EuroTech GmbH',
        'Европейский производитель высокоточного оборудования',
        'Германия',
        'Industriestraße 45, 80339 München',
        '+49-89-12345',
        'info@eurotech.de',
        'www.eurotech.de'
    ),
    (
        'AsianElectronics Co.',
        'Крупный производитель потребительской электроники',
        'Южная Корея',
        '789 Digital-ro, Gangnam-gu, Seoul',
        '+82-2-9876-5432',
        'contact@asianelec.kr',
        'www.asianelec.kr'
    ),
    (
        'RusTech Systems',
        'Российский производитель компьютерного оборудования',
        'Россия',
        'ул. Технологическая 78, Москва, 123456',
        '+7-495-123-45-67',
        'info@rustech.ru',
        'www.rustech.ru'
    ),
    (
        'SmartDevices Ltd',
        'Производитель умных устройств и гаджетов',
        'Великобритания',
        '45 Tech Park Road, London, SW1A 1AA',
        '+44-20-7123-4567',
        'contact@smartdevices.uk',
        'www.smartdevices.uk'
    );

-- Вставка поставщиков
INSERT INTO [Supplier] (
    [Name],
    [Description],
    [Country],
    [Address],
    [Phone],
    [Email],
    [Website],
    [DeliveryTime]
)
VALUES 
    (
        'Global Distribution Co.',
        'Международный поставщик электроники с быстрой доставкой',
        'США',
        '456 Logistics Ave, New York, NY 10001',
        '+1-555-9876',
        'sales@globaldist.com',
        'www.globaldist.com',
        3 -- дни доставки
    ),
    (
        'FastTech Logistics',
        'Специализированный поставщик компьютерных комплектующих',
        'Нидерланды',
        'Distribution Street 67, 1012 AB Amsterdam',
        '+31-20-555-1234',
        'orders@fasttech.nl',
        'www.fasttech.nl',
        2
    ),
    (
        'AsiaSupply Network',
        'Азиатская сеть поставок электронных компонентов',
        'Сингапур',
        '123 Supply Chain Road, Singapore 123456',
        '+65-6789-0123',
        'info@asiasupply.sg',
        'www.asiasupply.sg',
        5
    ),
    (
        'EuroLogistics',
        'Европейский поставщик с широкой сетью складов',
        'Франция',
        '789 Rue de la Logistique, 75001 Paris',
        '+33-1-2345-6789',
        'contact@eurologistics.fr',
        'www.eurologistics.fr',
        4
    ),
    (
        'RusSupply',
        'Российский поставщик компьютерной техники',
        'Россия',
        'ул. Складская 45, Санкт-Петербург, 198099',
        '+7-812-123-45-67',
        'sales@russupply.ru',
        'www.russupply.ru',
        1
    );

-- Проверка вставленных данных
SELECT * FROM [Manufacturer];
SELECT * FROM [Supplier];
