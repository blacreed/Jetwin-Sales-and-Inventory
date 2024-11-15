using Jetwin_Sales_and_Inventory.Utility_Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jetwin_Sales_and_Inventory.Maintenance_Submodules
{
    public partial class AddSupplier : Form
    {
        public event EventHandler SupplierAdded; //NOTIFIY MAIN FORM OF NEW SUPPLIER
        public AddSupplier()
        {
            InitializeComponent();
            tbContactNum.KeyPress += ContactNum_KeyPress;
        }

        private void ContactNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ONLY ALLOW NUMBERS AS INPUT
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(isInputValid() && tryAddSupplierToDatabase())
            {
                MessageBox.Show("Supplier added successfully.");
                SupplierAdded?.Invoke(this, EventArgs.Empty);
                InputValidator.ClearFields(tbSupplierName, tbAgentName, tbContactNum, tbRemarks);
                this.Close();
            }

        }
        private bool isInputValid()
        {
            string supplierName = tbSupplierName.Text;
            string agentName = tbAgentName.Text;
            string contactNum = tbContactNum.Text;

            return InputValidator.IsFieldFilled(supplierName, "Supplier Name") &&
                InputValidator.IsFieldFilled(agentName, "Agent Name") &&
                InputValidator.IsContactNumberValid(contactNum) &&
                InputValidator.IsSupplierValid(supplierName, contactNum);
        }
        private bool tryAddSupplierToDatabase()
        {
            try
            {
                //INSERT THE CONTACT NUMBER AND ADDRESS OF SUPPLIER TO CONTACTINFO TABLE
                //AND THEN GET THE CONTACT ID OF THAT NEW INSERTED CONTACT
                const string insertToContactQuery = "INSERT INTO ContactInfo (ContactNum, Address) " +
                                      "OUTPUT INSERTED.ContactID VALUES (@ContactNum, @Address)";
                int Active = 1; //IN STATUS TABLE, STATUSID: 1 = 'ACTIVE'

                var contactParameters = new Dictionary<string, object>
                {
                    { "@ContactNum", tbContactNum.Text.Trim() },
                    { "@Address", tbAddress.Text }
                };
                int contactId = Convert.ToInt32(DatabaseHelper.ExecuteScalar(insertToContactQuery, contactParameters));
                Console.WriteLine(contactId);
                const string insertToSupplierQuery = "INSERT INTO Supplier (SupplierName, ContactID, Remarks, StatusID) " +
                                       "VALUES (@SupplierName, @ContactID, @Remarks, @StatusID)";
                
                var supplierParameters = new Dictionary<string, object>
                {
                    { "@SupplierName", tbSupplierName.Text },
                    { "@ContactID", contactId },
                    { "@Remarks", tbRemarks.Text },
                    { "@StatusID", Active }
                };

                return DatabaseHelper.ExecuteNonQuery(insertToSupplierQuery, supplierParameters);
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error adding supplier: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        

        //DRAGGABLE FORM
        private bool _dragging = false;
        private Point _offset;
        private Point _start_point = new Point(0, 0);


        //CLOSE FORM
        private void nightControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

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
