using Jetwin_Sales_and_Inventory.Utility_Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace Jetwin_Sales_and_Inventory.Maintenance_Submodules
{
    public partial class AddProduct : Form
    {
        public event EventHandler ProductAdded;
        public AddProduct()
        {
            InitializeComponent();

            ConfigureTextBoxInputValidation();
            MaintenanceLoaders.LoadComboBoxData(cbBrand, "SELECT CategoryID, CategoryName FROM Category WHERE StatusID = 1", "CategoryName", "CategoryID");
            MaintenanceLoaders.LoadComboBoxData(cbSupplier, "SELECT SupplierID, Supplier FROM Supplier WHERE StatusID = 1", "Supplier", "SupplierID");
            MaintenanceLoaders.LoadComboBoxData(cbAttribute, "SELECT AttributeTypeID, AttributeTypeName FROM AttributeType WHERE StatusID = 1", "AttributeTypeName", "AttributeTypeID");

            attributeDataGrid.Columns.Add("AttributeType", "Attribute Type");
            attributeDataGrid.Columns.Add("AttributeValue", "Attribute Value");
            attributeDataGrid.Columns.Add("AttributeTypeID", "Attribute Type ID");
            attributeDataGrid.Columns.Add("AttributeValueID", "Attribute Value ID");

            // Hide ID columns (used for database operations but not displayed)
            attributeDataGrid.Columns["AttributeTypeID"].Visible = false;
            attributeDataGrid.Columns["AttributeValueID"].Visible = false;

            cbAttribute.SelectedIndexChanged += CbAttribute_SelectedIndexChanged;
        }

        private void CbAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAttribute.SelectedValue == null)
                return;

            // Retrieve the selected AttributeTypeID
            int attributeTypeID = Convert.ToInt32(cbAttribute.SelectedValue);

            // Query to fetch AttributeValues based on the selected AttributeTypeID
            string query = @"
            SELECT AttributeValueID, AttributeValueName 
            FROM AttributeValue 
            WHERE AttributeTypeID = @AttributeTypeID AND StatusID = 1";

            var parameters = new Dictionary<string, object>
            {
                { "@AttributeTypeID", attributeTypeID }
            };
        
            // Use DatabaseHelper.ExecuteQuery to fetch data
            DataTable attributeValues = DatabaseHelper.ExecuteQuery(query, parameters);

            if (attributeValues != null)
            {
                cbAttributeValue.DataSource = attributeValues;
                cbAttributeValue.DisplayMember = "AttributeValueName";
                cbAttributeValue.ValueMember = "AttributeValueID";
            }
            else
            {
                cbAttributeValue.DataSource = null; // Clear the ComboBox if no values are found
            }
        }

        private void ConfigureTextBoxInputValidation()
        {
            //ALLOW NUMBERS AND DECIMAL AS INPUT ONLY FOR SALE PRICE
            tbSalePrice.KeyPress += (s, e) => e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        //#region button save product start--
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(isInputValid() && tryAddProductToDatabase())
            {
                MessageBox.Show("Product added successfully.");
                ProductAdded?.Invoke(this, EventArgs.Empty);

                //CLEAR FIELDS
                cbBrand.SelectedIndex = -1;
                cbSupplier.SelectedIndex = -1;
                cbAttribute.SelectedIndex = -1;
                cbAttributeValue.SelectedIndex = -1;
                InputValidator.ClearFields(tbProductName, tbDescription, tbSalePrice);

                this.Close();
            }
        }
        //#region button save product end--

        private bool tryAddProductToDatabase()
        {
            try
            {
                // Step 1: Gather input data
                var productData = GetProductData();
                var attributes = GetAttributesFromGrid();

                // Step 2: Insert product and get ProductID
                int productID = InsertProduct(productData);

                if (productID <= 0)
                {
                    MessageBox.Show("Failed to add product. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Step 3: Validate and insert attributes
                if (!ValidateAndInsertAttributes(productID, attributes))
                {
                    return false;
                }

                MessageBox.Show("Product and attributes added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding product: " + ex.Message);
                return false;
            }
        }

        private (string ProductName, int BrandID, int SupplierID, string Description, decimal UnitPrice) GetProductData()
        {
            return (
                ProductName: tbProductName.Text.Trim(),
                BrandID: Convert.ToInt32(cbBrand.SelectedValue),
                SupplierID: Convert.ToInt32(cbSupplier.SelectedValue),
                Description: tbDescription.Text.Trim(),
                UnitPrice: Convert.ToDecimal(tbSalePrice.Text)
            );
        }

        private List<(int AttributeTypeID, int AttributeValueID, string AttributeType, string AttributeValue)> GetAttributesFromGrid()
        {
            var attributes = new List<(int, int, string, string)>();

            foreach (DataGridViewRow row in attributeDataGrid.Rows)
            {
                if (row.Cells["AttributeTypeID"].Value != null && row.Cells["AttributeValueID"].Value != null)
                {
                    attributes.Add((
                        AttributeTypeID: Convert.ToInt32(row.Cells["AttributeTypeID"].Value),
                        AttributeValueID: Convert.ToInt32(row.Cells["AttributeValueID"].Value),
                        AttributeType: row.Cells["AttributeType"].Value.ToString(),
                        AttributeValue: row.Cells["AttributeValue"].Value.ToString()
                    ));
                }
            }

            return attributes;
        }

        private int InsertProduct((string ProductName, int BrandID, int SupplierID, string Description, decimal UnitPrice) productData)
        {
            const string insertToProductQuery = @"
            INSERT INTO Product 
            (ProductName, ProductDescription, CategoryID, BrandID, UnitPrice, SupplierID, StatusID) 
            VALUES 
            (@ProductName, @ProductDescription, 1, @BrandID, @UnitPrice, @SupplierID, 1);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var productParameters = new Dictionary<string, object>
            {
                { "@ProductName", productData.ProductName },
                { "@ProductDescription", productData.Description },
                { "@BrandID", productData.BrandID },
                { "@UnitPrice", productData.UnitPrice },
                { "@SupplierID", productData.SupplierID }
            };

            return (int)DatabaseHelper.ExecuteScalar(insertToProductQuery, productParameters);
        }
        private bool ValidateAndInsertAttributes(int productID, List<(int AttributeTypeID, int AttributeValueID, string AttributeType, string AttributeValue)> attributes)
        {
            const string checkDuplicateAttributeQuery = @"
            SELECT COUNT(*) 
            FROM ProductAttributes 
            WHERE ProductID != @ProductID 
                AND AttributeTypeID = @AttributeTypeID 
                AND AttributeValueID = @AttributeValueID";

            const string insertAttributeQuery = @"
            INSERT INTO ProductAttributes 
            (ProductID, AttributeTypeID, AttributeValueID) 
            VALUES 
            (@ProductID, @AttributeTypeID, @AttributeValueID);";

            foreach (var attribute in attributes)
            {
                // Check for duplicates
                var duplicateCheckParams = new Dictionary<string, object>
                {
                    { "@ProductID", productID },
                    { "@AttributeTypeID", attribute.AttributeTypeID },
                    { "@AttributeValueID", attribute.AttributeValueID }
                };

                int duplicateCount = (int)DatabaseHelper.ExecuteScalar(checkDuplicateAttributeQuery, duplicateCheckParams);

                if (duplicateCount > 0)
                {
                    MessageBox.Show($"Duplicate attribute detected: {attribute.AttributeType} - {attribute.AttributeValue}.",
                                    "Duplicate Attribute", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Insert attribute
                var attributeParams = new Dictionary<string, object>
                {
                    { "@ProductID", productID },
                    { "@AttributeTypeID", attribute.AttributeTypeID },
                    { "@AttributeValueID", attribute.AttributeValueID }
                };

                if (!DatabaseHelper.ExecuteNonQuery(insertAttributeQuery, attributeParams))
                {
                    MessageBox.Show($"Failed to add attribute: {attribute.AttributeType} - {attribute.AttributeValue}.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }
        //#region input field validator start--
        private bool isInputValid()
        {
            string productName = tbProductName.Text;
            string brandName = cbBrand.Text;
            string supplier = cbSupplier.Text;   
            string salePrice = tbSalePrice.Text;

            return InputValidator.IsFieldFilled(brandName, "Brand Name") &&
                InputValidator.IsFieldFilled(productName, "Product Name") &&
                InputValidator.IsFieldFilled(supplier, "Supplier") &&
                InputValidator.IsFieldFilled(salePrice, "Sale Price") &&
                InputValidator.IsSalePriceValid(salePrice);
        }
        //#region input field validator end--

        //#region btn add attribute start--
        private void btnAddAttribute_Click(object sender, EventArgs e)
        {
            if (cbAttribute.SelectedValue == null || cbAttributeValue.SelectedValue == null)
            {
                MessageBox.Show("Please select both Attribute Type and Attribute Value before adding.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string attributeType = cbAttribute.Text;
            string attributeValue = cbAttributeValue.Text;

            // Check if the attribute already exists in the DataGridView
            foreach (DataGridViewRow row in attributeDataGrid.Rows)
            {
                if (row.Cells["AttributeType"].Value.ToString() == attributeType &&
                    row.Cells["AttributeValue"].Value.ToString() == attributeValue)
                {
                    MessageBox.Show("This attribute type and value have already been added.", "Duplicate Attribute", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Add to DataGridView
            attributeDataGrid.Rows.Add(attributeType, attributeValue, cbAttribute.SelectedValue, cbAttributeValue.SelectedValue);

            // Clear ComboBox inputs
            cbAttribute.SelectedIndex = -1;
            cbAttributeValue.DataSource = null;
        }
        //#region btn add attribute end--


        //#region unimportant events start
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
        //#region unimportant events end
    }
}
