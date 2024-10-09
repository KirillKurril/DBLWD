CREATE TABLE Profile (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL UNIQUE,
    birth_date DATE NULL,
    phone_number VARCHAR(20) NULL,
    photo NVARCHAR(MAX) DEFAULT 'images/employees/default_employee.png',
    job_description NVARCHAR(MAX) NULL,
    non_secretive BIT DEFAULT 0,
    FOREIGN KEY (user_id) REFERENCES [User](id) ON DELETE CASCADE
);
