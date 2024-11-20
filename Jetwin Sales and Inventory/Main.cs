using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Jetwin_Sales_and_Inventory
{
    public partial class Main : Form
    {
        private Dictionary<Type, Form> openForms = new Dictionary<Type, Form>();
        private Button activeButton;

        public Main()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.IsMdiContainer = true;
        }
        private void OpenForm<T>(Button btn) where T : Form, new()
        {
            SetActiveButton(btn);

            if (!openForms.ContainsKey(typeof(T)) || openForms[typeof(T)] == null)
            {
                T form = new T();
                form.MdiParent = this;
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                form.FormClosed += (s, args) => openForms[typeof(T)] = null;

                openForms[typeof(T)] = form;

                form.BringToFront();
                form.Show();
            }
            else
            {
                openForms[typeof(T)].BringToFront();
                openForms[typeof(T)].Activate();
            }
        }

        private void SetActiveButton(Button btn)
        {
            if(activeButton != null)
            {
                activeButton.BackColor = Color.FromArgb(39, 39, 47);
            }
            activeButton = btn;
            activeButton.BackColor = Color.Green;
        }
        private void btnDashboard_Click(object sender, EventArgs e) => OpenForm<Dashboard>((Button)sender);

        private void btnSales_Click(object sender, EventArgs e) => OpenForm<Sales>((Button)sender);

        private void btnInventory_Click(object sender, EventArgs e) => OpenForm<Inventory>((Button)sender);

        private void btnReport_Click(object sender, EventArgs e) => OpenForm<Report>((Button)sender);

        private void btnMaintenance_Click(object sender, EventArgs e) => OpenForm<Maintenance>((Button)sender);

        private void Main_Load(object sender, EventArgs e)
        {
            OpenForm<Dashboard>(btnDashboard);
        }
        //DRAGGABLE FORM
        private bool _dragging = false;
        private Point _offset;
        private Point _start_point = new Point(0, 0);
        private void mainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }

        private void mainPanel_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void mainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }
    }
}
