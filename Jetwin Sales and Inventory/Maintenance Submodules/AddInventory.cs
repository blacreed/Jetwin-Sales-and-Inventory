using Jetwin_Sales_and_Inventory.Utility_Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Jetwin_Sales_and_Inventory.Maintenance_Submodules
{
    public partial class AddInventory : Form
    {
        private readonly string _connectionString;
        public event EventHandler InventoryAdded;
        public AddInventory()
        {
            InitializeComponent();

            ConfigureTextBoxInputValidation();
            MaintenanceLoaders.LoadComboBoxData(cbBrand, "SELECT CategoryID, CategoryName FROM Category WHERE StatusID = 1", "CategoryName", "CategoryID");
            MaintenanceLoaders.LoadComboBoxData(cbSupplier, "SELECT SupplierID, SupplierName FROM Supplier WHERE StatusID = 1", "SupplierName", "SupplierID");
        }

        private void ConfigureTextBoxInputValidation()
        {
            //ALLOW NUMBERS AS INPUT ONLY FOR STOCK AND SALE PRICE
            tbStock.KeyPress += (s, e) => e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            tbSalePrice.KeyPress += (s, e) => e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(isInputValid() && tryAddInventoryToDatabase())
            {
                MessageBox.Show("Inventory item added successfully.");
                InventoryAdded?.Invoke(this, EventArgs.Empty);

                //CLEAR FIELDS
                cbBrand.SelectedIndex = -1;
                cbSupplier.SelectedIndex = -1;
                InputValidator.ClearFields(tbProductName, tbStock, tbSalePrice,
                    tbLowLevelLimit, tbLocation, tbPartNumber);

                this.Close();
            }
        }
        private bool tryAddInventoryToDatabase()
        {
            try
            {
                string productName = tbProductName.Text;
                string partName = tbPartNumber.Text;
                string quantityInStock = tbStock.Text;
                string unitPrice = tbSalePrice.Text;
                string lowLevelLimit = tbLowLevelLimit.Text;
                string location = tbLocation.Text;
                int brandID = Convert.ToInt32(cbBrand.SelectedValue);
                int supplierID = Convert.ToInt32(cbSupplier.SelectedValue);
                int Active = 1; //IN STATUS TABLE, STATUSID: 1 = 'ACTIVE'

                //IF PART NUMBER FIELD HAS INPUT THEN INSERT VALUE, ELSE INSERT N/A TO TABLE
                int? partId = null;
                if (!string.IsNullOrWhiteSpace(partName))
                {
                    partId = DatabaseHelper.GetOrCreatePartId(partName);
                    //ERROR HANDLING FOR LOGICAL ERROR IN PARTID INSERT
                    if (partId == null)
                    {
                        throw new InvalidOperationException("PartID could not be created or retrieved.");
                    }
                }

                const string insertToInventoryQuery = "INSERT INTO Inventory (PartID, ProductName, CategoryID, SupplierID, QuantityInStock, UnitPrice, LowLevelLimit, Location, StatusID) " +
                               "VALUES (@PartID, @ProductName, @CategoryID, @SupplierID, @QuantityInStock, @UnitPrice, @LowLevelLimit, @Location, @StatusID)";
                
                var inventoryParameters = new Dictionary<string, object>
                    {
                        { "@CategoryID",  brandID},
                        { "@ProductName", productName},
                        { "@SupplierID", supplierID },
                        { "@QuantityInStock", quantityInStock },
                        { "@UnitPrice", unitPrice },
                        { "@LowLevelLimit", lowLevelLimit },
                        { "@Location", location },
                        { "@StatusID", Active },
                        { "@PartID",  (object)partId ?? DBNull.Value } // Insert NULL if USER DID NOT INPUT DATA IN PARTNUMBER FIELD
                    };

                return DatabaseHelper.ExecuteNonQuery(insertToInventoryQuery, inventoryParameters);
            }

            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the inventory item: " + ex.Message);
                return false;
            }
        }

        private bool isInputValid()
        {
            string brandName = cbBrand.Text;
            string productName = tbProductName.Text;
            string supplier = cbSupplier.Text;
            string stock = tbStock.Text;
            string salePrice = tbSalePrice.Text;

            string lowLevelLimit = tbLowLevelLimit.Text;
            string location = tbLocation.Text;

            return InputValidator.IsFieldFilled(brandName, "Brand Name") &&
                InputValidator.IsFieldFilled(productName, "Product Name") &&
                InputValidator.IsFieldFilled(supplier, "Supplier") &&
                InputValidator.IsFieldFilled(stock, "Stock") &&
                InputValidator.IsFieldFilled(salePrice, "Sale Price") &&
                InputValidator.IsStockValid(stock) &&
                InputValidator.IsSalePriceValid(salePrice) &&
                InputValidator.IsLowLevelLimitValid(lowLevelLimit) &&
                InputValidator.IsPartNumberAndLocationValid(location, "Location");
        }
           
        //CLOSE FORM
        private void nightControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        //DRAGGABLE FORM
        private bool _dragging = false;
        private Point _offset;
        private Point _start_point = new Point(0, 0);

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }
    }
}
