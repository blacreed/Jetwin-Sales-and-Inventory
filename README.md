# Computerized Sales and Inventory System for Jetwin Motorcycle Parts and Auto Supply

This is a **Computerized Sales and Inventory System** developed for **Jetwin Motorcycle Parts and Auto Supply**. The system is designed to streamline and automate the sales and inventory management processes of the business, replacing their previous manual methods with a robust, user-friendly interface.

**Note**: This project is still a **work in progress** and is continually being developed and improved. Features may be subject to changes and updates in future versions.


## Features

### User Authentication
- **Login System**: Secure login functionality with different access levels for **Admin** and **Staff** roles.

### Dashboard Module
- Displays an overview of business operations:
  - Total number of transactions made.
  - Total number of products.
  - Number of low stock and out-of-stock items.
  - View tables for:
    - Latest sales.
    - Highest-selling products.
    - Sales performance (with time filters for Today, Yesterday, Last Month, and This Month).
    - Top 10 recently added products.

### Sales Module
- Create and manage sales transactions.
  - Input GCash reference numbers (no integration with GCash API).
  - View transaction history with a searchable table.

### Inventory Module
- Manage and view inventory details:
  - Product Name, Category, Brand, Part Number, Supplier, Unit Price, Quantity in Stock, Low Stock Level, Location, and Status.
  - Search functionality for easier navigation.
  - Add and update stock levels.

### Report Module
- Generate and view sales reports in **PDF** format.

### Maintenance Module
- Sub-modules for system maintenance:
  - **Users**: Add, edit, archive users, and view their activities.
  - **Inventory**: Manage and update product inventory.
  - **Category**: Manage product categories.
  - **Brand**: Manage brands.
  - **Supplier**: Manage suppliers.
  - **Backup and Recovery**: Create backups and restore database.

### Roles and Permissions
- **Admin**: Full access to all modules.
- **Staff**: Restricted to Dashboard, Sales, Inventory, and Report modules.

## Technologies Used
- **Microsoft Visual Studio** (C# Windows Forms)
- **SQL Server Management Studio**

- 
## Database Tables

The system uses the following database tables:

### **1. Status**
Holds the statuses; Active, Inactive, and Archived
### **2. SystemUsers**
Stores information about the users of the system, including login credentials, roles, and status.
### **3. ContactInfo**
Stores contact details for suppliers.
### **4. Category**
Stores product categories.
### **5. Supplier**
Stores information about suppliers, including contact details and remarks.
### **6. Part**
Stores part numbers for the inventory items.
### **7. Inventory**
Contains information about the products in stock, including quantity, price, and related details.


## Setup Instructions

1. Clone the repository.
2. Open the project in **Microsoft Visual Studio**.
3. Set up the database using the provided schema in **SQL Server Management Studio**.
4. Build and run the application locally.

## Limitations
- This is an **offline application** and does not support integration with **barcode scanners** or external APIs like **GCash**.