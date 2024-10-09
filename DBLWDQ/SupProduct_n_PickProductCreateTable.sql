CREATE TABLE [PickupPoint_Product] (
    [pickup_point_id] INT,
    [product_id] INT,
    PRIMARY KEY ([pickup_point_id], [product_id]),
    FOREIGN KEY ([pickup_point_id]) REFERENCES [PickupPoint]([id]) ON DELETE CASCADE,
    FOREIGN KEY ([product_id]) REFERENCES [Product]([id]) ON DELETE CASCADE
);
