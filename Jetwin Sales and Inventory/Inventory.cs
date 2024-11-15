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
    public partial class Inventory : Form
    {
        private DataRefreshManager refreshManager;
        public Inventory()
        {
            InitializeComponent();

            refreshManager = new DataRefreshManager();
            refreshManager.OnDataUpdated += RefreshInventoryData;
            refreshManager.Start();
            RefreshInventoryData(); //INITIAL LOAD
        }

        public DataGridView getInventoryDataGrid()
        {
            return inventoryDataGrid;
        }
        private void RefreshInventoryData()
        {
            if (InvokeRequired) //PREVENT CROSS THREAD ERROR
            {
                Invoke(new Action(RefreshInventoryData));
                return;
            }

            InventoryLoaders.LoadData("Inventory", this);
            totalProductLabel.Text = "Products: " + DatabaseHelper.GetTotalProductCount().ToString();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            refreshManager.Dispose();
            base.OnFormClosed(e);
        }

    }
}
