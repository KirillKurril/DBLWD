CREATE TABLE [PickupPoint] (
    [id] INT PRIMARY KEY IDENTITY(1,1),
    [name] NVARCHAR(100) NOT NULL,
    [address] NVARCHAR(200) NOT NULL,
    [phone_number] NVARCHAR(20) NOT NULL
);
