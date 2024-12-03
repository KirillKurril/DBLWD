-- Очистка таблицы
DELETE FROM [Product];
DBCC CHECKIDENT ('[Product]', RESEED, 0);

-- Вставка продуктов
INSERT INTO [Product] (
    [Name],
    [ArticleNumber],
    [Description],
    [PricePerUnit],
    [Image],
    [Count],
    [CategoryId]
)
VALUES 
    (
        'Процессор Intel Core i5-12400F',
        'CPU-12400F',
        'Процессор Intel Core i5-12400F, 6 ядер, 12 потоков, 2.5-4.4 ГГц',
        299.99,
        'images/products/cpu-12400f.png',
        10,
        1  -- Предполагаем, что категория "Процессоры" имеет ID = 1
    ),
    (
        'Видеокарта NVIDIA RTX 3060',
        'GPU-3060',
        'Видеокарта NVIDIA GeForce RTX 3060, 12GB GDDR6',
        499.99,
        'images/products/gpu-3060.png',
        5,
        2  -- Видеокарты
    ),
    (
        'Материнская плата MSI B550',
        'MB-B550',
        'Материнская плата MSI B550 GAMING PLUS, AM4, ATX',
        159.99,
        'images/products/mb-b550.png',
        8,
        3  -- Материнские платы
    ),
    (
        'Оперативная память Kingston 16GB',
        'RAM-K16',
        'Оперативная память Kingston FURY Beast 16GB DDR4-3200',
        89.99,
        'images/products/ram-k16.png',
        15,
        4  -- Оперативная память
    ),
    (
        'SSD Samsung 970 EVO Plus 1TB',
        'SSD-970-1TB',
        'NVMe SSD Samsung 970 EVO Plus 1TB, M.2',
        129.99,
        'images/products/ssd-970.png',
        12,
        5  -- SSD накопители
    ),
    (
        'Блок питания Corsair RM750x',
        'PSU-RM750X',
        'Блок питания Corsair RM750x, 750W, 80+ Gold',
        149.99,
        'images/products/psu-rm750x.png',
        7,
        6  -- Блоки питания
    ),
    (
        'Корпус NZXT H510',
        'CASE-H510',
        'Корпус NZXT H510, ATX, черный',
        79.99,
        'images/products/case-h510.png',
        6,
        7  -- Корпуса
    ),
    (
        'Кулер CPU Noctua NH-D15',
        'COOL-NHD15',
        'Кулер для процессора Noctua NH-D15, 140mm',
        99.99,
        'images/products/cool-nhd15.png',
        9,
        8  -- Охлаждение
    ),
    (
        'Монитор LG 27GL850-B',
        'MON-27GL850',
        'Монитор LG 27GL850-B, 27", 1440p, 144Hz, IPS',
        449.99,
        'images/products/mon-27gl850.png',
        4,
        9  -- Мониторы
    ),
    (
        'Клавиатура Ducky One 2',
        'KB-DUCKY2',
        'Механическая клавиатура Ducky One 2, Cherry MX Brown',
        119.99,
        'images/products/kb-ducky2.png',
        11,
        10  -- Периферия
    ),
    (
        'Мышь Logitech G Pro X Superlight',
        'MOUSE-GPXS',
        'Игровая мышь Logitech G Pro X Superlight, 25600 DPI',
        149.99,
        'images/products/mouse-gpxs.png',
        8,
        10  -- Периферия
    ),
    (
        'Наушники HyperX Cloud II',
        'HEAD-CLOUD2',
        'Игровые наушники HyperX Cloud II, 7.1',
        99.99,
        'images/products/head-cloud2.png',
        13,
        10  -- Периферия
    ),
    (
        'Процессор AMD Ryzen 7 5800X',
        'CPU-5800X',
        'Процессор AMD Ryzen 7 5800X, 8 ядер, 16 потоков, 3.8-4.7 ГГц',
        349.99,
        'images/products/cpu-5800x.png',
        6,
        1  -- Процессоры
    ),
    (
        'Видеокарта AMD RX 6700 XT',
        'GPU-6700XT',
        'Видеокарта AMD Radeon RX 6700 XT, 12GB GDDR6',
        479.99,
        'images/products/gpu-6700xt.png',
        4,
        2  -- Видеокарты
    ),
    (
        'SSD Crucial P5 2TB',
        'SSD-P5-2TB',
        'NVMe SSD Crucial P5 2TB, M.2',
        199.99,
        'images/products/ssd-p5.png',
        7,
        5  -- SSD накопители
    );

-- Проверка вставленных данных
SELECT * FROM [Product];
