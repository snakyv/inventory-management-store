CREATE DATABASE InventoryLab16;
GO

USE InventoryLab16;
GO

CREATE TABLE Parts
(
    PartID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Invoices
(
    InvoiceID INT IDENTITY(1,1) PRIMARY KEY,
    SupplieID INT NULL,
    SerialNumber NVARCHAR(100) NOT NULL,
    totalValue DECIMAL(18,2) NULL,
    InvoiceDate DATE NULL
);
GO

CREATE TABLE Inventory
(
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NULL,
    Quantity INT NULL,
    PartID INT NULL,
    InvoiceID INT NULL,

    CONSTRAINT FK_Inventory_Parts
        FOREIGN KEY (PartID) REFERENCES Parts(PartID),

    CONSTRAINT FK_Inventory_Invoices
        FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID)
);
GO

CREATE TABLE Operations
(
    OperationID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE InventoryOperations
(
    InventoryOperationID INT IDENTITY(1,1) PRIMARY KEY,
    Explanation NVARCHAR(300) NOT NULL,
    Price DECIMAL(18,2) NULL,
    Quantity INT NULL,
    InventoryID INT NULL,
    OperationID INT NULL,

    CONSTRAINT FK_InventoryOperations_Inventory
        FOREIGN KEY (InventoryID) REFERENCES Inventory(InventoryID),

    CONSTRAINT FK_InventoryOperations_Operations
        FOREIGN KEY (OperationID) REFERENCES Operations(OperationID)
);
GO

INSERT INTO Parts (Name)
VALUES
('Engine parts'),
('Brake system'),
('Transmission parts'),
('Electrical parts'),
('Body parts'),
('Filters');

INSERT INTO Invoices (SupplieID, SerialNumber, totalValue, InvoiceDate)
VALUES
(1, 'INV-001', 1500.00, '2025-01-10'),
(2, 'INV-002', 2300.50, '2025-01-18'),
(3, 'INV-003', 980.75, '2025-02-03'),
(4, 'INV-004', 1750.00, '2025-02-20');

INSERT INTO Inventory (Name, Price, Quantity, PartID, InvoiceID)
VALUES
('Oil Filter', 15.50, 40, 6, 1),
('Air Filter', 18.75, 35, 6, 1),
('Brake Pads', 45.00, 25, 2, 2),
('Brake Disc', 80.00, 16, 2, 2),
('Spark Plug', 9.90, 60, 4, 3),
('Battery', 120.00, 10, 4, 3),
('Clutch Kit', 210.00, 7, 3, 4),
('Front Bumper', 155.00, 5, 5, 4),
('Engine Belt', 33.50, 20, 1, 1),
('Fuel Pump', 95.00, 12, 1, 2);

INSERT INTO Operations (Name)
VALUES
('Arrival'),
('Sale'),
('Return'),
('Write-off');

INSERT INTO InventoryOperations (Explanation, Price, Quantity, InventoryID, OperationID)
VALUES
('Initial stock arrival', 15.50, 40, 1, 1),
('Initial stock arrival', 18.75, 35, 2, 1),
('Initial stock arrival', 45.00, 25, 3, 1),
('Sale to customer', 45.00, 2, 3, 2),
('Return from customer', 18.75, 1, 2, 3),
('Damaged item write-off', 155.00, 1, 8, 4);
GO