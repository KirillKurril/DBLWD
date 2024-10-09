CREATE TABLE [Order] (
    [id] INT IDENTITY(1,1) PRIMARY KEY,
    [date] DATE NOT NULL,
    [quantity] INT CHECK ([quantity] > 0),
    [product_id] INT NOT NULL,
    [user_id] INT NOT NULL,
    [pickup_point_id] INT NOT NULL,
    [promocode_id] INT NOT NULL,
	FOREIGN KEY ([user_id]) REFERENCES [User]([id]) ON DELETE NO ACTION,
	FOREIGN KEY ([product_id]) REFERENCES [Product]([id]) ON DELETE NO ACTION,
    FOREIGN KEY ([pickup_point_id]) REFERENCES [PickupPoint]([id]) ON DELETE NO ACTION,
    FOREIGN KEY ([promocode_id]) REFERENCES [PromoCode]([id]) ON DELETE NO ACTION
);
