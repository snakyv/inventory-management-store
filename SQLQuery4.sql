USE InventoryLab16;
GO

IF COL_LENGTH('Orders', 'Shipped') IS NULL
BEGIN
    ALTER TABLE Orders
    ADD Shipped BIT NOT NULL
        CONSTRAINT DF_Orders_Shipped DEFAULT 0;
END;
GO