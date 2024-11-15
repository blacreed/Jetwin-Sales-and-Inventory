using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
namespace Jetwin_Sales_and_Inventory.Utility_Class
{
    internal static class DatabaseHelper
    {

        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        
        //DML (INSERT, UPDATE, DELETE)
        //FOR INSERTING, UPDATING, OR DELETING A DATA INTO A SPECIFIC TABLE IN DATABASE
        public static bool ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //DQL (SELECT)
        //FOR RETRIEVING DATA IN A SPECIFIC TABLE IN DATABASE
        public static object ExecuteScalar(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    if(parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //DQL (SELECT)
        //FOR RETRIEVING ALL DATA OF A SPECIFIC TABLE IN DATABASE AND PUTTING IT IN INTERFACE TABLE FOR VIEWING
        public static DataTable ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //DATA RETRIEVERS------------------------
        public static int GetTotalProductCount()
        {
            const string query = "SELECT COUNT(*) FROM Inventory";
            return Convert.ToInt32(ExecuteScalar(query, new Dictionary<string, object>()));
        }

        public static DataTable GetRecentProducts(int count = 10)
        {
            string query = @"
            SELECT TOP(@Count) ProductName AS [Product Name], LastUpdated AS [Last Updated], QuantityInStock AS Stock
            FROM Inventory 
            ORDER BY LastUpdated DESC";

            var parameters = new Dictionary<string, object> { { "@Count", count } };
            return ExecuteQuery(query, parameters);
        }
        public static int GetLowStockCount()
        {
            const string query = "SELECT COUNT(*) FROM Inventory WHERE QuantityInStock < LowLevelLimit";

            object result = ExecuteScalar(query, null);
            return result != null ? Convert.ToInt32(result) : 0;
        }
        public static int GetOutOfStockCount()
        {
            const string query = "SELECT COUNT(*) FROM Inventory WHERE QuantityInStock = 0";

            object result = ExecuteScalar(query, null);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        //INPUT VALIDATOR--------------------------
        //--FOR addCategory FORM
        public static bool IsCategoryNameDuplicate(string categoryName)
        {
            string query = "SELECT COUNT(1) FROM Category WHERE CategoryName = @CategoryName";
            var parameters = new Dictionary<string, object> { { "@CategoryName", categoryName } };

            return Convert.ToInt32(ExecuteScalar(query, parameters)) > 0;
        }

        //--FOR addUser FORM
        public static bool IsUsernameTaken(string username) //CHECK IF USERNAME IN DATABASE ALREADY EXISTS
        {
            string query = "SELECT COUNT(1) FROM SystemUsers WHERE Username = @Username";
            var parameters = new Dictionary<string, object> { { "@Username", username } };

            return Convert.ToInt32(ExecuteScalar(query, parameters)) > 0;
        }

        //--FOR addSupplier FORM
        public static bool IsDuplicateSupplier(string supplierName, string contactNum)
        {
            string query = @"
                    SELECT COUNT(1)
                    FROM Supplier S
                    JOIN ContactInfo C ON S.ContactID = C.ContactID
                    WHERE S.SupplierName = @SupplierName OR C.ContactNum = @ContactNum";

            var parameters = new Dictionary<string, object>
            {
                { "@SupplierName", supplierName },
                { "@ContactNum", contactNum }
            };

            return Convert.ToInt32(ExecuteScalar(query,parameters)) > 0;
        }

        //--FOR addInventory FORM
        public static int? GetOrCreatePartId(string partNumber)
        {
            string getPartIdQuery = "SELECT PartID FROM Part WHERE PartNumber = @PartNumber";

            var partNumberParameter = new Dictionary<string, object>
            {
                { "@PartNumber", partNumber }
            };

            object partID = ExecuteScalar(getPartIdQuery, partNumberParameter);

            if (partID != DBNull.Value && partID != null)
            {
                return Convert.ToInt32(partID);
            }
            else
            {
                string insertToPartQuery = "INSERT INTO Part (PartNumber) OUTPUT INSERTED.PartID VALUES (@PartNumber)";
                return Convert.ToInt32(ExecuteScalar(insertToPartQuery, partNumberParameter));
            }
        }

        //END OF CLASS----------------------------------------
    }
}
