CREATE TABLE [Supplier_Manufacturer] (
    [supplier_id] INT,
    [manufacturer_id] INT,
    PRIMARY KEY ([supplier_id], [manufacturer_id]),
    FOREIGN KEY ([supplier_id]) REFERENCES [Supplier]([id]) ON DELETE CASCADE,
    FOREIGN KEY ([manufacturer_id]) REFERENCES [Manufacturer]([id]) ON DELETE CASCADE
);