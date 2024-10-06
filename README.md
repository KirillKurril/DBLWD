# Online-Сourses-db
# Дмитрук Богдан Ярославович гр. 253504
# Интернет-магазин по продаже автозапчастей
# 1. Определение темы проекта
Тема проекта: Система управления сайтом интернет-магазина по продаже автозапчастей. Данная система позволяет со стороны покупателя просматривать размещенную на сайте информацию, будучи зарегистрированным пользователем, оформлять заказы и оставлять комментарии. Со стороны администрации и модерации позволяет администрировать сайт. 
## 1.1 Основные цели проекта
Управление пользователями: регистрация, авторизация и распределение ролей, управление профилями.
Админитрирование товаров: создание и управление товарами и категориями товаров
Админитрирование заказов: создание и управление заказами, карзиной заказов и промокодами
Админитрирование справочной информации: создание и управление информацией о пунктах выдачи, поставщиках, производителях, партнерах, часто задаваемых вопросах, новостными статьями
# 2. Определение функциональных требований
Система ролей:
Авторизация пользователя. Пользователь может быть гостем, зарегистрированным пользователем, модератором или администратором.
### Для администраторов:
Создание и управление аккаунтами всех пользователей сервиса.
### Для модераторов и администраторов:
Реализация CRUD по отношению к моделям данных, содержащих информацию о производителях, поставщиках, продуктах, категорих продуктов, вакансиях, партнерах, часто задаваемых вопросов, новостных статей, пунктов свмовывоза, промокодов, просмотр данных о зарегистрированных пользователях, информации об оформленных ими заказах, модерирование и просмотр отзывов, оставленных пользователями.
### Для незарегистрированных пользователей:
Просмотр информации о магазине и каталога товаров.
### Для зарегистрированных пользователей:
То же, что и для незарегистрированных пользвоателей, кроме того, оформление и управление заказами, управление личным профилем, создание и редактирование отзывов о магазине.
# 3. Определение сущностей базы данных
## 3.1 Сущности
1. PickupPoint (Точка самовывоза)
id: INTEGER, NOT NULL, PK
name: VARCHAR(100), NOT NULL
address: VARCHAR(200), NOT NULL
phone_number: VARCHAR(20), NOT NULL

2. Manufacturer (Производитель)
id: INTEGER, NOT NULL, PK
name: VARCHAR(100), NOT NULL
address: VARCHAR(100), NOT NULL
phone: VARCHAR(20), NOT NULL

3. Supplier (Поставщик)
id: INTEGER, NOT NULL, PK
name: VARCHAR(100), NOT NULL
address: VARCHAR(100), NOT NULL
phone: VARCHAR(20), NOT NULL
manufacturers: MANY-TO-MANY -> Manufacturer, NOT NULL   

4. Article 
id: INTEGER, NOT NULL, PK
user_id: INTEGER, NOT NULL, FK -> User
title:  NVARCHAR(100) NOT NULL
text:  NVARCHAR(MAX) NOT NULL
photo:  NVARCHAR(MAX) NOT NULL
created_at:  DATETIME

5. FAQ
id: INTEGER, NOT NULL, PK
article_id: INTEGER, NOT NULL, FK -> Article
question:  NVARCHAR(100) NOT NULL

6. Category
id: INTEGER, NOT NULL, PK
name:  NVARCHAR(100) NOT NULL
description: NVARCHAR(200)

7. Order
id: INTEGER, NOT NULL, PK
date:  DATE NOT NULL
quantity:  INTEGER > 0
product_id:  INTEGER NOT NULL, FK -> Product
user_id:  INTEGER NOT NULL, FK -> User
pickup_point_id:  INTEGER NOT NULL, FK -> PickupPoint
promocode_id:  INTEGER NOT NULL, FK -> Promocode

8. Vacancy
id: INTEGER, NOT NULL, PK
title:  NVARCHAR(100)
description:  NVARCHAR(MAX)
requirements:  NVARCHAR(MAX)
responsibilities:  NVARCHAR(MAX)
location:  NVARCHAR(254)
salary:  DECIMAL(8,2)
image:  NVARCHAR(MAX)
created_at:  DATETIME NOT NULL

9. Review
id:  INTEGER IDENTITY(1,1) PK
user_id: INTEGER, NOT NULL, FK -> User
title:  NVARCHAR(100)
text:  NVARCHAR(MAX) NOT NULL
rating:  INTEGER DEFAULT 5 NOT NULL
created_at: DATETIME DEFAULT NOT NULL

10. User
id:  INTEGER IDENTITY(1,1) PK
password:  NVARCHAR(128) NOT NULL
last_login:  DATETIME NULL
is_superuser:  BIT NOT NULL
username:  NVARCHAR(150) NOT NULL UNIQUE
first_name:  NVARCHAR(128) NULL
last_name:  NVARCHAR(128) NULL
email:  NVARCHAR(254) NULL UNIQUE
is_staff:  BIT NOT NULL
is_active:  BIT NOT NULL
date_joined:  DATETIME NOT NULL

11. Profile
id:  INTEGER IDENTITY(1,1) PK
user_id: INTEGER NOT NULL UNIQUE FK -> User
birth_date: DATE NULL
phone_number: VARCHAR(20) NULL
photo: NVARCHAR(MAX)
job_description: NVARCHAR(MAX) NULL
non_secretive: BIT DEFAULT 0

12. Product
id:  INTEGER IDENTITY(1,1) PK
name:  NVARCHAR(100) NOT NULL
article_number  NVARCHAR(20) NOT NULL
description  NVARCHAR(100) NOT NULL
price_per_unit  DECIMAL(10, 2) DEFAULT 9.99
image  NVARCHAR(255)
count  INTEGER DEFAULT 5
category_id  INT NOT NULL, FK -> Category

13. Partner
id:  INTEGER IDENTITY(1,1) PK
name:  NVARCHAR(100) NOT NULL
photo: NVARCHAR(MAX)
website: NVARCHAR(255) NOT NULL

14 Supply
user_id: INTEGER, NOT NULL, FK -> Order
supplier_id: INTEGER, NOT NULL, FK -> Supplier
manufacturer_id: INTEGER, NOT NULL, FK -> Manufacturer
# 3.2 Описание сущностей
1. PickupPoint (Точка самовывоза):
Содержит информацию о точках самовывоза, включая название, адрес и номер телефона для связи.

2. Manufacturer (Производитель):
Описывает производителей товаров, содержащие информацию о названии, адресе и контактных данных.

3. Supplier (Поставщик):
Представляет поставщиков, связанных с несколькими производителями. Содержит контактную информацию  о поставщиках и связь с производителями.

4. Article (Статья):
Статья, опубликованная модератором или администратором, с указанием названия, текста, фото и даты создания.

5. FAQ (Часто задаваемые вопросы):
Специальное расширение статьи, позволяющее описывать часто задаваемые вопросы. Хранит вопрос и связь со статьей.

6. Category (Категория):
Представляет категории товаров и описывает эти категории.

7. Order (Заказ):
Содержит информацию о заказах, включая количество товаров, связанные промокоды, пользователей и точку самовывоза.

8. Vacancy (Вакансия):
Хранит информацию о вакансиях, включая требования, обязанности и уровень заработной платы.

9. Review (Отзыв):
Представляет отзывы пользователей о товарах с оценкой, названием и текстом отзыва.

10. User (Пользователь):
Содержит основную информацию о пользователях системы, включая логин, пароль, электронную почту и статус активности.

11. Profile (Профиль):
Хранит дополнительную информацию о пользователе, такую как дата рождения, номер телефона и фото.

12. Product (Товар):
Содержит информацию о товарах, включая название, описание, артикул, цену и категорию.

13. Partner (Партнер):
Представляет партнеров компании, включая название, фото и ссылку на сайт.
