using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Jetwin_Sales_and_Inventory.Utility_Class
{
    internal static class InventoryLoaders
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        private static readonly Dictionary<string, (string Query, DataGridView GridView)> dataMap;
        
        static InventoryLoaders()
        {
            dataMap = new Dictionary<string, (string, DataGridView)>();
        }

        public static void LoadData(string dataType, Inventory inventory)
        {
            if (!dataMap.ContainsKey("Inventory"))
            {
                dataMap["Inventory"] = (
                    "SELECT I.ProductName AS [Product Name], " +
                    "C.CategoryName AS [Category Name], " +
                    "ISNULL(P.PartNumber, 'N/A') AS [Part Number], " +
                    "I.Location AS Location, " +
                    "S.SupplierName AS Supplier, " +
                    "I.UnitPrice AS [Unit Price], " +
                    "I.QuantityInStock AS [Quantity in Stock], " +
                    "I.LowLevelLimit AS [Low Level Limit] " +
                    "FROM Inventory I " +
                    "LEFT JOIN Part P ON I.PartID = P.PartID " +
                    "JOIN Category C ON I.CategoryID = C.CategoryID " +
                    "JOIN Supplier S ON I.SupplierID = S.SupplierID " +
                    "ORDER BY I.QuantityInStock DESC",
                    inventory.getInventoryDataGrid()
                    
                );
            }
            if (dataMap.TryGetValue(dataType, out var data))
            {
                LoadDataForGrid(data.Query, data.GridView);
            }
            else
            {
                throw new ArgumentException($"Invalid data type: {dataType}");
            }
        }
        private static void LoadDataForGrid(string query, DataGridView gridView)
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
