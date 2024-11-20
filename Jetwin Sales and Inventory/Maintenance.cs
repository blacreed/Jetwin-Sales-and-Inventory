using System;
using System.Windows.Forms;
using Jetwin_Sales_and_Inventory.Utility_Class;
using Jetwin_Sales_and_Inventory.Maintenance_sub_modules;
using Jetwin_Sales_and_Inventory.Maintenance_Submodules;

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
            if (userDataGrid == null)
            {
                Console.WriteLine("user data grid is null");
            }
            else
            {
                Console.WriteLine("use data grid is not null");
            }
            return userDataGrid;
        }
        public DataGridView getUserHistoryDataGrid()
        {
            return historyDataGrid;
        }
        public DataGridView getProductDataGrid()
        {
            return productDataGrid;
        }
        
        public DataGridView getCategoryDataGrid()
        {
            return categoryDataGrid;
        }
        public DataGridView getBrandDataGrid()
        {
            return brandDataGrid;
        }
        public DataGridView getAttributeDataGrid()
        {
            return attributeDataGrid;
        }
        public DataGridView getInventoryDataGrid()
        {
            return inventoryDataGrid;
        }
        public DataGridView getSupplierDataGrid()
        {
            return supplierDataGrid;
        }
        private void ShowPanel(Panel panel) //DISPLAY THE PANEL OF THE SUB-MODULE BUTTON CLICKED
        {
            maintenanceUserPanel.Visible = false;
            maintenanceProductPanel.Visible = false;
            maintenanceClassificationPanel.Visible = false;
            maintenanceInventoryPanel.Visible = false;
            maintenanceSupplierPanel.Visible = false;
            maintenanceAuditTrail.Visible = false;
            maintenanceBackupPanel.Visible = false;

            panel.Visible = true;
        }
        
        //DISPLAY SUB-MODULES ON CLICK METHODS
        private void btnUserMaintenance_Click(object sender, EventArgs e) => ShowPanel(maintenanceUserPanel);
        private void btnProductMaintenance_Click(object sender, EventArgs e) => ShowPanel(maintenanceProductPanel);
        private void btnClassificationMaintenance_Click(object sender, EventArgs e) => ShowPanel(maintenanceClassificationPanel);
        private void btnInventoryMaintenance_Click(object sender, EventArgs e) => ShowPanel(maintenanceInventoryPanel);
        private void btnSupplierMaintenance_Click(object sender, EventArgs e) => ShowPanel(maintenanceSupplierPanel);
        private void btnAuditMaintenance_Click(object sender, EventArgs e) => ShowPanel(maintenanceAuditTrail);
        
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
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            using (AddProduct addProductForm = new AddProduct())
            {
                addProductForm.ProductAdded += (s, args) => MaintenanceLoaders.LoadData("Products");
                addProductForm.ShowDialog();
            }

        }
        private void btnAddClassification_Click(object sender, EventArgs e)
        {
            using (AddClassification addClassificationForm = new AddClassification())
            {
                addClassificationForm.ClassificationAdded += (s, args) => MaintenanceLoaders.LoadData("Categories", "Brands", "Attributes");
                addClassificationForm.ShowDialog();
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

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            using (AddSupplier addSupplierForm = new AddSupplier())
            {
                addSupplierForm.SupplierAdded += (s, args) => MaintenanceLoaders.LoadData("Suppliers");
                addSupplierForm.ShowDialog();
            }
        }
        //#region end--
    }
}
