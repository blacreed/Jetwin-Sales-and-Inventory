using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jetwin_Sales_and_Inventory
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
        }

        private void validateUser()
        {
            String query = "SELECT role from users WHERE username = @username and password = @password";
            String returnValue = "";

            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\users.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = tbUsername.Text;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = tbPassword.Text;
                    con.Open();
                    returnValue = (string)cmd.ExecuteScalar();
                }
            }
            if (String.IsNullOrEmpty(returnValue))
            {
                MessageBox.Show("Incorrect username or password.");
                return;
            }
            returnValue = returnValue.Trim();
            if (returnValue == "admin")
            {
                //MessageBox.Show("You are logged in as admin");
                Main mainForm = new Main();
                mainForm.Show();
                this.Hide();
            }
            else if (returnValue == "user")
            {
                //MessageBox.Show("You are logged in as user.");
                //main form but limited access to modules only
                //
                this.Hide();
            }
            if(cbRemember.Checked)
            {
                Properties.Settings.Default.Username = tbUsername.Text;
                Properties.Settings.Default.Password = tbPassword.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            validateUser();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.Username != string.Empty)
            {
                tbUsername.Text = Properties.Settings.Default.Username;
                tbPassword.Text = Properties.Settings.Default.Password;
                cbRemember.Checked = true;
            }
        }

        //DRAGGABLE FORM
        private bool _dragging = false;
        private Point _offset;
        private Point _start_point = new Point(0, 0);

        private void loginPanel_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }

        private void loginPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void loginPanel_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
    }
}
