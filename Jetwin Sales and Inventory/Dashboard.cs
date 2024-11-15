using Jetwin_Sales_and_Inventory.Utility_Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jetwin_Sales_and_Inventory
{
    public partial class Dashboard : Form
    {
        private DataRefreshManager refreshManager;
        public Dashboard()
        {
            InitializeComponent();

            refreshManager = new DataRefreshManager();
            refreshManager.OnDataUpdated += RefreshDashboardData;
            refreshManager.Start();
            RefreshDashboardData(); //INITIAL LOAD
        }
        private void RefreshDashboardData()
        {
            if (InvokeRequired) //PREVENT CROSS THREAD ERROR
            {
                Invoke(new Action(RefreshDashboardData));
                return;
            }
            lblTotalProducts.Text = DatabaseHelper.GetTotalProductCount().ToString();
            lblLowStock.Text = DatabaseHelper.GetLowStockCount().ToString();
            lblOutOfStock.Text = DatabaseHelper.GetOutOfStockCount().ToString();

            recentsDataGrid.DataSource = DatabaseHelper.GetRecentProducts();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            refreshManager.Dispose();
            base.OnFormClosed(e);
        }
    }
}
