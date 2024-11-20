using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

namespace Jetwin_Sales_and_Inventory.Utility_Class
{
    internal static class MaintenanceLoaders
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        private static Dictionary<string, (string Query, DataGridView GridView)> dataMap;
        //FOR GETTING SUPPLIER AND BRAND DATA AND LOADING TO COMBO BOX INPUT FIELD
        public static void LoadComboBoxData(ComboBox comboBox, string query, string displayMember, string valueMember)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                comboBox.DataSource = dataTable;
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = valueMember;
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }
        public static void LoadAllData(Maintenance maintenance)
        {
            // Define the queries and map them to DataGridView controls
            dataMap = new Dictionary<string, (string Query, DataGridView GridView)>
        {
            { "Users", (
                @"SELECT Username, EmployeeName, ContactNum, RoleName, S.StatusName AS Status
                FROM Staff U
                JOIN Status S ON U.StatusID = S.StatusID
                ORDER BY DateCreated DESC",
                maintenance.getUserDataGrid()) },

            { "Products", (
                @"SELECT p.ProductName,
                at.AttributeTypeName,
                av.AttributeValueName,
                i.QuantityInStock
                FROM Inventory i
                JOIN Product p ON i.ProductID = p.ProductID
                JOIN ProductAttributes pa ON i.AttributeCombinationID = pa.ProductAttributeID
                JOIN AttributeValue av ON pa.AttributeValueID = av.AttributeValueID
                JOIN AttributeType at ON pa.AttributeTypeID = at.AttributeTypeID",
                maintenance.getProductDataGrid()
                )},

            { "Inventory", (
                @"SELECT P.ProductName AS [Product Name],
                C.CategoryName AS [Category],
                ISNULL(P.PartNumber, 'N/A') AS [Part Number],
                I.Location AS Location,
                S.Supplier AS Supplier,
                P.UnitPrice AS [Unit Price],
                I.QuantityInStock AS [Quantity In Stock],
                I.LowLevelLimit AS [Low Level Limit],
                I.LastUpdated AS [Last Updated],
                ST.StatusName AS Status
                FROM Inventory I
                JOIN Product P ON I.ProductID = P.ProductID
                JOIN Category C ON P.CategoryID = C.CategoryID
                JOIN Supplier S ON P.SupplierID = S.SupplierID
                JOIN Status ST ON P.StatusID = ST.StatusID
                ORDER BY I.LastUpdated DESC",
                maintenance.getInventoryDataGrid()) },

            { "Categories", (
                @"SELECT C.CategoryName, S.StatusName AS Status
                FROM Category C
                JOIN Status S ON C.StatusID = S.StatusID
                ORDER BY C.CategoryID DESC",
                maintenance.getCategoryDataGrid()) },

            { "Suppliers", (
                @"SELECT Supplier, CI.Address, CI.ContactNum, Remarks, ST.StatusName AS Status
                FROM Supplier S
                JOIN SupplierContactInfo CI ON S.ContactID = CI.ContactID
                JOIN Status ST ON S.StatusID = ST.StatusID
                ORDER BY CASE WHEN LastOrderDate IS NULL THEN 1 ELSE 0 END, LastOrderDate DESC",
                maintenance.getSupplierDataGrid()) },

            { "Brands", (
                @"SELECT BrandName, S.StatusName AS Status
                FROM Brand B
                JOIN Status S ON B.StatusID = S.StatusID
                ORDER BY B.BrandID DESC",
                maintenance.getBrandDataGrid()) },

            { "Attributes", (
                @"SELECT AttributeTypeName, S.StatusName AS Status
                FROM AttributeType AT
                JOIN Status S ON AT.StatusID = S.StatusID
                ORDER BY AT.AttributeTypeID DESC",
                maintenance.getAttributeDataGrid()) }
        };
            Console.WriteLine($"Users Query: {dataMap["Users"].Query}");
            Console.WriteLine($"Users GridView: {dataMap["Users"].GridView?.Name ?? "null"}");

            var userGrid = maintenance.getUserDataGrid();
            Console.WriteLine($"GridView Name: {userGrid?.Name ?? "null"}");
            if (userGrid == null)
            {
                throw new InvalidOperationException("getUserDataGrid() returned null.");
            }

            // Load data for each table
            foreach (var dataType in dataMap.Keys)
            {
                LoadData(dataType);
            }
        }
        public static void LoadData(params string[] dataTypes)
        {
            foreach (var dataType in dataTypes)
            {
                Console.WriteLine($"Attempting to load data for: {dataType}");

                if (dataMap.TryGetValue(dataType, out var data))
                {
                    Console.WriteLine($"Query: {data.Query}");
                    Console.WriteLine($"GridView: {data.GridView?.Name ?? "null"}");
                    if (data.Query == null || data.GridView == null)
                        throw new InvalidOperationException($"DataMap entry for '{dataType}' is incomplete.");

                    LoadDataForGrid(data.Query, data.GridView);
                }
                else
                {
                    throw new ArgumentException($"Invalid data type: {dataType}");
                }
            }
        }
        public static void LoadDataForGrid(string query, DataGridView gridView)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    gridView.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
                throw;
            }
        }
            
    }
}
