-- Очистка таблицы
DELETE FROM [Product];
DBCC CHECKIDENT ('[Product]', RESEED, 0);

-- Вставка автозапчастей
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
        'Масляный фильтр MANN W 914/2',
        'W914-2',
        'Масляный фильтр MANN для Lada Vesta, XRAY. Высокая степень фильтрации.',
        15.99,
        'images/products/oil-filter-w914.png',
        50,
        1  -- Фильтры
    ),
    (
        'Воздушный фильтр BIG GB-95',
        'GB-95',
        'Воздушный фильтр для Lada Granta, Kalina. Улучшенная система фильтрации.',
        12.99,
        'images/products/air-filter-gb95.png',
        40,
        1  -- Фильтры
    ),
    (
        'Тормозные колодки TRW GDB1583',
        'GDB1583',
        'Передние тормозные колодки для Lada Vesta. Высокая износостойкость.',
        45.99,
        'images/products/brake-pads-gdb1583.png',
        30,
        2  -- Тормозная система
    ),
    (
        'Тормозной диск BREMBO 08.5085.10',
        '08508510',
        'Передний тормозной диск для Lada Vesta. Вентилируемый.',
        89.99,
        'images/products/brake-disc-085085.png',
        20,
        2  -- Тормозная система
    ),
    (
        'Амортизатор KAYABA 365502',
        '365502',
        'Передний амортизатор для Lada Granta. Газомасляный.',
        79.99,
        'images/products/shock-365502.png',
        15,
        3  -- Подвеска
    ),
    (
        'Пружина подвески KYB RH2466',
        'RH2466',
        'Передняя пружина для Lada Vesta. Усиленная.',
        49.99,
        'images/products/spring-rh2466.png',
        25,
        3  -- Подвеска
    ),
    (
        'Ремень ГРМ GATES 5471XS',
        '5471XS',
        'Ремень ГРМ для Lada Largus 16V. Усиленный.',
        29.99,
        'images/products/belt-5471xs.png',
        35,
        4  -- Двигатель
    ),
    (
        'Свечи зажигания NGK BKR6E',
        'BKR6E',
        'Свечи зажигания для Lada Vesta, XRAY. Комплект 4 шт.',
        24.99,
        'images/products/spark-bkr6e.png',
        60,
        4  -- Двигатель
    ),
    (
        'Радиатор охлаждения LUZAR LRc 0127',
        'LRc0127',
        'Радиатор для Lada Granta. Алюминиевый.',
        119.99,
        'images/products/radiator-lrc0127.png',
        10,
        5  -- Система охлаждения
    ),
    (
        'Помпа водяная KRAFT KT 101501',
        'KT101501',
        'Водяной насос для Lada Vesta. В комплекте прокладка.',
        39.99,
        'images/products/pump-kt101501.png',
        20,
        5  -- Система охлаждения
    ),
    (
        'Фара передняя правая DEPO 551-1174R',
        '5511174R',
        'Передняя фара для Lada Granta. Галогеновая.',
        149.99,
        'images/products/light-5511174r.png',
        8,
        6  -- Освещение
    ),
    (
        'Лампа H7 OSRAM 64210',
        '64210',
        'Галогеновая лампа H7 для фар. 55W.',
        9.99,
        'images/products/bulb-64210.png',
        100,
        6  -- Освещение
    ),
    (
        'Стартер BOSCH 0 986 022 640',
        '0986022640',
        'Стартер для Lada Vesta. Мощность 1.4 кВт.',
        199.99,
        'images/products/starter-0986022640.png',
        5,
        7  -- Электрика
    ),
    (
        'Аккумулятор VARTA Blue Dynamic 60Ah',
        'E23-60',
        'Аккумулятор 60 Ач, 540 А. Обратная полярность.',
        129.99,
        'images/products/battery-e23.png',
        15,
        7  -- Электрика
    ),
    (
        'Комплект сцепления LUK 620 3160 09',
        '62031609',
        'Комплект сцепления для Lada Vesta. Корзина, диск, подшипник.',
        259.99,
        'images/products/clutch-62031609.png',
        7,
        8  -- Трансмиссия
    );

-- Проверка вставленных данных
SELECT * FROM [Product];
