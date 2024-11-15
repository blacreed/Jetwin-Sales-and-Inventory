CREATE TABLE Status (
    StatusID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    StatusName VARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE SystemUsers (
    UserID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
	Password VARCHAR(50) NOT NULL,
    EmployeeName VARCHAR(50) NOT NULL,
    ContactNum VARCHAR(15),
    Role VARCHAR(20) NOT NULL,
    StatusID INT NOT NULL FOREIGN KEY REFERENCES Status(StatusID),
    DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
    LastLogin DATETIME,
    CreatedBy INT FOREIGN KEY REFERENCES SystemUsers(UserID),
    UpdatedBy INT FOREIGN KEY REFERENCES SystemUsers(UserID)
);

CREATE TABLE ContactInfo (
    ContactID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    ContactNum VARCHAR(15) NOT NULL,
    Address VARCHAR(255)
);

CREATE TABLE Category (
    CategoryID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    CategoryName VARCHAR(50) NOT NULL UNIQUE,
    StatusID INT NOT NULL FOREIGN KEY REFERENCES Status(StatusID),
    CreatedBy INT FOREIGN KEY REFERENCES SystemUsers(UserID),
    UpdatedBy INT FOREIGN KEY REFERENCES SystemUsers(UserID)
);

CREATE TABLE Supplier (
    SupplierID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    SupplierName VARCHAR(50) NOT NULL UNIQUE,
    ContactID INT FOREIGN KEY REFERENCES ContactInfo(ContactID),
    Remarks TEXT,
    LastOrderDate DATETIME,
    StatusID INT FOREIGN KEY REFERENCES Status(StatusID),
    CreatedBy INT FOREIGN KEY REFERENCES SystemUsers(UserID),
    UpdatedBy INT FOREIGN KEY REFERENCES SystemUsers(UserID)
);

CREATE TABLE Part (
    PartID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    PartNumber VARCHAR(50) UNIQUE,
    CreatedBy INT FOREIGN KEY REFERENCES SystemUsers(UserID),
    UpdatedBy INT FOREIGN KEY REFERENCES SystemUsers(UserID)
);

CREATE TABLE Inventory (
    InventoryID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	ProductName VARCHAR(50) NOT NULL,
    PartID INT FOREIGN KEY REFERENCES Part(PartID),
    CategoryID INT NOT NULL FOREIGN KEY REFERENCES Category(CategoryID),
    SupplierID INT NOT NULL FOREIGN KEY REFERENCES Supplier(SupplierID),
    QuantityInStock INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    LowLevelLimit INT NOT NULL,
    Location VARCHAR(20),
    LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),
    StatusID INT NOT NULL FOREIGN KEY REFERENCES Status(StatusID),
    CreatedBy INT FOREIGN KEY REFERENCES SystemUsers(UserID),
    UpdatedBy INT FOREIGN KEY REFERENCES SystemUsers(UserID)
);

INSERT INTO Status
VALUES ('Active'),
('Inactive'),
('Archived');