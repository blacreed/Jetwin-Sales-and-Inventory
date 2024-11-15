using Jetwin_Sales_and_Inventory.Utility_Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jetwin_Sales_and_Inventory.Maintenance_Submodules
{
    public partial class AddUser : Form
    {
        public event EventHandler UserAdded;

        public AddUser()
        {
            InitializeComponent();
            this.FormClosing += (s, e) => { e.Cancel = true; this.Hide(); }; //CLOSE THE FORM
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isInputValid() && tryAddUserToDatabase())
            {
                MessageBox.Show("User added successfully.");
                UserAdded?.Invoke(this, EventArgs.Empty);
                InputValidator.ClearFields(tbEmployeeName, tbUsername, tbPassword, tbContactNum);
                this.Close();
            }
        }
        
        private bool isInputValid()
        {
            string employeeName = tbEmployeeName.Text;
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            string contactNum = tbContactNum.Text;

            return InputValidator.IsFieldFilled(employeeName, "Employee Name") &&
                InputValidator.IsFieldFilled(username, "Username") &&
                InputValidator.IsFieldFilled(password, "Password") &&
                InputValidator.IsFieldFilled(contactNum, "Contact Number") &&
                InputValidator.IsEmployeeNameValid(employeeName) &&
                InputValidator.IsUsernameValid(username) &&
                InputValidator.IsContactNumberValid(contactNum) &&
                InputValidator.IsPasswordValid(password);
        }
        
        private bool tryAddUserToDatabase()
        {
            try
            {
                const string insertToUserQuery = "INSERT INTO SystemUsers (Username, EmployeeName, Password, ContactNum, Role, StatusID, DateCreated) " +
                        "VALUES (@Username, @EmployeeName, @Password, @ContactNum, 'Staff', @StatusID, GETDATE())";
                int Active = 1; //IN STATUS TABLE, STATUSID: 1 = 'ACTIVE'

                var userParameters = new Dictionary<string, object>
                {
                    { "@EmployeeName", tbEmployeeName.Text },
                    { "@Username", tbUsername.Text.Trim() },
                    { "@Password", tbPassword.Text },
                    { "@ContactNum", tbContactNum.Text.Trim() },
                    { "@StatusID", Active }
                };

                return DatabaseHelper.ExecuteNonQuery(insertToUserQuery, userParameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //DRAGGABLE FORM---------------------------------------
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

        //HIDE THE FORM
        private void nightControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
