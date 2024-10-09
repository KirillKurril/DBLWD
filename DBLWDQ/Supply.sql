CREATE TABLE [Supply] (
    [product_id] INT,
    [supplier_id] INT,
    [manufacturer_id] INT,
    PRIMARY KEY ([product_id], [supplier_id], [manufacturer_id]),
    FOREIGN KEY ([product_id]) REFERENCES [Product]([id]) ON DELETE CASCADE,
    FOREIGN KEY ([supplier_id]) REFERENCES [Supplier]([id]) ON DELETE CASCADE,
    FOREIGN KEY ([manufacturer_id]) REFERENCES [Manufacturer]([id]) ON DELETE CASCADE
);