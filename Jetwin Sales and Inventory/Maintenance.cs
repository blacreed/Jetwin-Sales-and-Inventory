using Jetwin_Sales_and_Inventory.Maintenance_Submodules;
using System;
using System.Windows.Forms;
using Jetwin_Sales_and_Inventory.Utility_Class;

namespace Jetwin_Sales_and_Inventory
{
    public partial class Maintenance : Form
    {
        public Maintenance()
        {
            InitializeComponent();

            //START METHODS
            ShowPanel(maintenanceUserPanel); //DISPLAY USER SUB-MODULE AFTER RUN
            MaintenanceLoaders.LoadAllData(this); //LOAD DATA IN TABLES TO THEIR RESPECTIVE DATA GRIDS
        }
        public DataGridView getUserDataGrid()
        {
            return userDataGrid;
        }
        public DataGridView getInventoryDataGrid()
        {
            return inventoryDataGrid;
        }
        public DataGridView getCategoryDataGrid()
        {
            return categoryDataGrid;
        }
        public DataGridView getSupplierDataGrid()
        {
            return supplierDataGrid;
        }
        private void ShowPanel(Panel panel) //DISPLAY THE PANEL OF THE SUB-MODULE BUTTON CLICKED
        {
            maintenanceUserPanel.Visible = false;
            maintenanceInventoryPanel.Visible = false;
            maintenanceCategoryPanel.Visible = false;
            maintenanceSupplierPanel.Visible = false;
            maintenanceBackupPanel.Visible = false;

            panel.Visible = true;
        }
        
        //DISPLAY SUB-MODULES ON CLICK METHODS
        private void btnUserMaintenance_Click(object sender, EventArgs e) => ShowPanel(maintenanceUserPanel);

        private void btnInventoryMaintenance_Click(object sender, EventArgs e) => ShowPanel(maintenanceInventoryPanel);

        private void btnCategoryMaintenance_Click(object sender, EventArgs e) => ShowPanel(maintenanceCategoryPanel);

        private void btnSupplierMaintenance_Click(object sender, EventArgs e) => ShowPanel(maintenanceSupplierPanel);

        private void btnBackupMaintenance_Click(object sender, EventArgs e) => ShowPanel(maintenanceBackupPanel);

        //EVENT METHODS--------------------------------------------
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            using (AddUser addUserForm = new AddUser())
            {
                addUserForm.UserAdded += (s, args) => MaintenanceLoaders.LoadData("Users");
                addUserForm.ShowDialog();
            }

        }

        private void btnAddInventory_Click(object sender, EventArgs e)
        {
            using (AddInventory addInventoryForm = new AddInventory())
            {
                addInventoryForm.InventoryAdded += (s, args) => MaintenanceLoaders.LoadData("Inventory");
                addInventoryForm.ShowDialog();
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            using (AddCategory addCategoryForm = new AddCategory())
            {
                addCategoryForm.BrandAdded += (s, args) => MaintenanceLoaders.LoadData("Categories");
                addCategoryForm.ShowDialog();
            }
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            using (AddSupplier addSupplierForm = new AddSupplier())
            {
                addSupplierForm.SupplierAdded += (s, args) => MaintenanceLoaders.LoadData("Suppliers");
                addSupplierForm.ShowDialog();
            }
        }
    }
}
