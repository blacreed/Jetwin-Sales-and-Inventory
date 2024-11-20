using Jetwin_Sales_and_Inventory.Utility_Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Jetwin_Sales_and_Inventory.Maintenance_Submodules
{
    public partial class AddClassification : Form
    {
        public event EventHandler ClassificationAdded; //NOTIFY MAIN FORM OF NEW CLASSIFICATION
        public AddClassification()
        {
            InitializeComponent();
            ConfigureRadioButtonDefaults();
        }
        private void ConfigureRadioButtonDefaults()
        {
            // Set the default state
            rbBrand.Checked = true;
            tbClassification.Enabled = true;
            cbAttributeType.Enabled = false;
            tbAttributeValue.Enabled = false;

            // Attach event handlers for radio buttons
            rbBrand.CheckedChanged += RadioButton_CheckedChanged;
            rbCategory.CheckedChanged += RadioButton_CheckedChanged;
            rbAttribType.CheckedChanged += RadioButton_CheckedChanged;
            rbAttribValue.CheckedChanged += RadioButton_CheckedChanged;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAttribValue.Checked)
            {
                // Enable Attribute Type and Value fields, disable Classification
                tbClassification.Enabled = false;
                cbAttributeType.Enabled = true;
                tbAttributeValue.Enabled = true;

                // Load existing Attribute Types into cbAttributeType
                LoadAttributeTypes();
            }
            else
            {
                // Enable Classification field, disable Attribute Type and Value fields
                tbClassification.Enabled = true;
                cbAttributeType.Enabled = false;
                tbAttributeValue.Enabled = false;

                // Clear values for Attribute Type and Value fields
                cbAttributeType.SelectedIndex = -1;
                tbAttributeValue.Clear();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rbAttribValue.Checked)
            {
                if (IsAttributeValueInputValid() && TryAddAttributeValueToDatabase())
                {
                    MessageBox.Show("Attribute Value added successfully.");
                    ClassificationAdded?.Invoke(this, EventArgs.Empty);
                    ClearFields();
                    this.Close();
                }
            }
            else
            {
                if (IsClassificationInputValid() && TryAddClassificationToDatabase())
                {
                    MessageBox.Show("Category/Brand/Attribute Type added successfully.");
                    ClassificationAdded?.Invoke(this, EventArgs.Empty);
                    ClearFields();
                    this.Close();
                }
            }
            
        }
        private void ClearFields()
        {
            tbClassification.Clear();
            tbAttributeValue.Clear();
            cbAttributeType.SelectedIndex = -1;
            rbBrand.Checked = true; // Reset to default
        }

        public bool TryAddClassificationToDatabase()
        {
            try
            {
                string insertQuery = "";
                string classificationName = tbClassification.Text;
                int activeStatus = 1; // StatusID for "ACTIVE"

                // Determine which table to insert into
                if (rbBrand.Checked)
                {
                    insertQuery = "INSERT INTO Brand (BrandName, StatusID) VALUES (@Name, @StatusID)";
                }
                else if (rbCategory.Checked)
                {
                    insertQuery = "INSERT INTO Category (CategoryName, StatusID) VALUES (@Name, @StatusID)";
                }
                else if (rbAttribType.Checked)
                {
                    insertQuery = "INSERT INTO AttributeType (AttributeTypeName, StatusID) VALUES (@Name, @StatusID)";
                }

                var parameters = new Dictionary<string, object>
                {
                    { "@Name", classificationName },
                    { "@StatusID", activeStatus }
                };

                return DatabaseHelper.ExecuteNonQuery(insertQuery, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding classification: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool TryAddAttributeValueToDatabase()
        {
            try
            {
                string insertQuery = "INSERT INTO AttributeValue (AttributeValueName, AttributeTypeID, StatusID) VALUES (@ValueName, @TypeID, @StatusID)";
                int activeStatus = 1; // StatusID for "ACTIVE"

                var parameters = new Dictionary<string, object>
                {
                    { "@ValueName", tbAttributeValue.Text },
                    { "@TypeID", Convert.ToInt32(cbAttributeType.SelectedValue) },
                    { "@StatusID", activeStatus }
                };

                return DatabaseHelper.ExecuteNonQuery(insertQuery, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding attribute value: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool IsClassificationInputValid()
        {
            string classificationName = tbClassification.Text;

            return InputValidator.IsFieldFilled(classificationName, "Classification Name") &&
                   InputValidator.IsUniqueClassification(classificationName, GetSelectedTableName());
        }
        private bool IsAttributeValueInputValid()
        {
            string attributeValue = tbAttributeValue.Text;

            return cbAttributeType.SelectedValue != null &&
                   InputValidator.IsFieldFilled(attributeValue, "Attribute Value Name") &&
                   InputValidator.IsUniqueAttributeValue(attributeValue, Convert.ToInt32(cbAttributeType.SelectedValue));
        }
        private void LoadAttributeTypes()
        {
            // Query to fetch all active Attribute Types
            string query = "SELECT AttributeTypeID, AttributeTypeName FROM AttributeType WHERE StatusID = 1";

            var attributeTypes = DatabaseHelper.ExecuteQuery(query, new Dictionary<string, object>());

            if (attributeTypes != null && attributeTypes.Rows.Count > 0)
            {
                cbAttributeType.DataSource = attributeTypes;
                cbAttributeType.DisplayMember = "AttributeTypeName";
                cbAttributeType.ValueMember = "AttributeTypeID";
            }
            else
            {
                cbAttributeType.DataSource = null;
                MessageBox.Show("No active attribute types found. Please add one first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private string GetSelectedTableName()
        {
            if (rbBrand.Checked) return "Brand";
            if (rbCategory.Checked) return "Category";
            if (rbAttribType.Checked) return "AttributeType";
            return null;
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
