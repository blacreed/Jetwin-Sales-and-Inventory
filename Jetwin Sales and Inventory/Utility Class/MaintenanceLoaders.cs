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
            if(dataMap == null)
            {
                dataMap = new Dictionary<string, (string, DataGridView)>
            {
                { "Users", ("SELECT Username, EmployeeName, ContactNum, Role, S.StatusName AS Status " +
                            "FROM SystemUsers U JOIN Status S ON U.StatusID = S.StatusID ORDER BY DateCreated DESC", maintenance.getUserDataGrid()) },

                { "Inventory", ("SELECT I.ProductName AS [Product Name], " +
                                "C.CategoryName AS [Category Name], " +
                                "ISNULL(P.PartNumber, 'N/A') AS [Part Number], " +
                                "I.Location AS Location, " +
                                "S.SupplierName AS Supplier, " +
                                "I.UnitPrice AS [Unit Price], " +
                                "I.QuantityInStock AS [Quantity in Stock], " +
                                "I.LowLevelLimit AS [Low Level Limit], " +
                                "I.LastUpdated AS [Last Updated], " +
                                "ST.StatusName AS Status " +
                                "FROM Inventory I " +
                                "LEFT JOIN Part P ON I.PartID = P.PartID " +
                                "JOIN Category C ON I.CategoryID = C.CategoryID " +
                                "JOIN Supplier S ON I.SupplierID = S.SupplierID " +
                                "JOIN Status ST ON I.StatusID = ST.StatusID " +
                                "ORDER BY I.LastUpdated DESC", maintenance.getInventoryDataGrid()) },

                { "Categories", ("SELECT C.CategoryName, S.StatusName AS Status " +
                                "FROM Category C JOIN Status S ON C.StatusID = S.StatusID ORDER BY C.CategoryID DESC", maintenance.getCategoryDataGrid()) },

                { "Suppliers", ("SELECT SupplierName, CI.Address, CI.ContactNum, Remarks, ST.StatusName AS Status " +
                                "FROM Supplier S " +
                                "JOIN ContactInfo CI ON S.ContactID = CI.ContactID " +
                                "JOIN Status ST ON S.StatusID = ST.StatusID " +
                                "ORDER BY CASE WHEN LastOrderDate IS NULL THEN 1 ELSE 0 END, LastOrderDate DESC", maintenance.getSupplierDataGrid()) }
            };
            }
            
            foreach (var dataType in dataMap.Keys)
            {
                LoadData(dataType);
            }
        }
        public static void LoadData(string dataType)
        {

            if (dataMap.TryGetValue(dataType, out var data))
            {
                LoadDataForGrid(data.Query, data.GridView);
            }
            else
            {
                throw new ArgumentException($"Invalid data type: {dataType}");
            }
        }
        public static void LoadDataForGrid(string query, DataGridView gridView)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                gridView.DataSource = table;
            }
        }
            
    }
}
