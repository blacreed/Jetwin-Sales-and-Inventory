using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Jetwin_Sales_and_Inventory.Utility_Class
{
    internal static class InputValidator
    {

        public static void ClearFields(params TextBox[] textboxes)
        {
            foreach (var textbox in textboxes)
            {
                textbox.Clear();
            }
        }

        public static bool IsFieldFilled(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show($"{fieldName} is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public static bool IsContactNumberValid(string input)
        {
            if (!Regex.IsMatch(input, @"^\d{9,15}$"))
            {
                MessageBox.Show("Contact Number must be between 9 and 15 digits.");
                return false;
            }
            return true;
        }

        //FOR AddUser Form
        /* RULES:
         * fields marked with * are required, cannot be empty or whitespace
         * employee name should only contain alphabetic characters and spaces with a maximum of 50 character length
         * username must be unique, allows alphanumeric only and no spaces, with a length constraint of 5-20 characters
         * password has a minimum length of 8 characters allowing a mix of letters, numbers, and special character, (consider hashing for security purposes)
         * contact number has a minimum length of 9 digits and maximum of 15 digits
         */
        public static bool IsNameValid(string employeeName, string username)
        {
            if (!Regex.IsMatch(employeeName, @"^[a-zA-Z\s]{1,50}$"))
            {
                MessageBox.Show("Employee Name must contain only letters and spaces, with a maximum of 50 characters.");
                return false;
            }
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$") || username.Length < 5 || username.Length > 20)
            {
                MessageBox.Show($"Username must be alphanumeric with a length of 5-20 characters.");
                return false;
            }
            return true;
        }
        public static bool IsUserNotExists(string employeeName, string username, string contactNum)
        {
            if (DatabaseHelper.IsEmployeeDuplicate(employeeName) || DatabaseHelper.IsContactNumberDuplicate(contactNum))
            {
                MessageBox.Show("Employee already exists.");
                return false;
            }
            if (DatabaseHelper.IsUsernameTaken(username))
            {
                MessageBox.Show("Username already exists. Choose a different username.");
                return false;
            }
            
            return true;
        }
        
        public static bool IsPasswordValid(string input)
        {
            if (input.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.");
                return false;
            }
            return true;
        }
        public static bool IsPasswordMatching(string input, string input2)
        {
            if(!string.Equals(input, input2))
            {
                MessageBox.Show("Passwords do not match.");
                return false;
            }
            return true;
        }

        //FOR AddClassification FORM
        public static bool IsUniqueClassification(string classificationName, string tableName)
        {
            if(!DatabaseHelper.IsUniqueClassification(classificationName, tableName))
            {
                MessageBox.Show($"{classificationName} already exists.");
                return false;
            }
            return true;
        }

        public static bool IsUniqueAttributeValue(string attributeValue, int attributeTypeID)
        {
            return DatabaseHelper.IsUniqueAttributeValue(attributeValue, attributeTypeID);
        }


        //FOR addSupplier FORM
        public static bool IsSupplierValid(string supplierName, string contactNum)
        {
            if(DatabaseHelper.IsDuplicateSupplier(supplierName, contactNum))
            {
                MessageBox.Show("Supplier already exists.");
                return false;
            }
            return true;
        }

        //FOR addInventory FORM
        public static bool IsSalePriceValid(string input)
        {
            if (!decimal.TryParse(input, out decimal salePrice) || salePrice <= 0)
            {
                MessageBox.Show("Sale Price must be a positive number.");
                return false;
            }
            return true;
        }

        //END OF CLASS----------------------------
    }
}
