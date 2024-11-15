using Jetwin_Sales_and_Inventory.Utility_Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Jetwin_Sales_and_Inventory.Maintenance_Submodules
{
    public partial class AddCategory : Form
    {
        public event EventHandler BrandAdded; //NOTIFY MAIN FORM OF NEW BRAND
        public AddCategory()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string categoryName = tbBrandName.Text;
            if (isInputValid() && tryAddCategoryToDatabase())
            {
                MessageBox.Show("Category added successfully.");
                BrandAdded?.Invoke(this, EventArgs.Empty);
                InputValidator.ClearFields(tbBrandName);
                this.Close();
            } 
        }
        public bool tryAddCategoryToDatabase()
        {
            try
            {
                string insertToCategoryQuery = "INSERT INTO Category (CategoryName, StatusID) VALUES (@CategoryName, @StatusID)";
                int Active = 1; //IN STATUS TABLE, STATUSID: 1 = 'ACTIVE'

                var categoryParameters = new Dictionary<string, object>
                {
                    { "@CategoryName", tbBrandName.Text },
                    { "@StatusID", Active }
                };

                return DatabaseHelper.ExecuteNonQuery(insertToCategoryQuery, categoryParameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool isInputValid()
        {
            string brandName = tbBrandName.Text;

            return InputValidator.IsFieldFilled(brandName, "Category Name") &&
                InputValidator.IsCategoryValid(brandName);
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
