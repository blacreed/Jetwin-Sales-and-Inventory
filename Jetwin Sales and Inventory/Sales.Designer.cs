namespace Jetwin_Sales_and_Inventory
{
    partial class Sales
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sales));
            this.salesDataGrid = new System.Windows.Forms.DataGridView();
            this.btnSalesAdd = new System.Windows.Forms.Button();
            this.moduleNamePanel = new System.Windows.Forms.Panel();
            this.lblJetwin = new System.Windows.Forms.Label();
            this.searchFieldPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.salesDataGrid)).BeginInit();
            this.moduleNamePanel.SuspendLayout();
            this.searchFieldPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // salesDataGrid
            // 
            this.salesDataGrid.AllowUserToAddRows = false;
            this.salesDataGrid.AllowUserToDeleteRows = false;
            this.salesDataGrid.AllowUserToResizeColumns = false;
            this.salesDataGrid.AllowUserToResizeRows = false;
            this.salesDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.salesDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.salesDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(64)))));
            this.salesDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.salesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.salesDataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(81)))));
            this.salesDataGrid.Location = new System.Drawing.Point(12, 202);
            this.salesDataGrid.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.salesDataGrid.Name = "salesDataGrid";
            this.salesDataGrid.ReadOnly = true;
            this.salesDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.salesDataGrid.Size = new System.Drawing.Size(724, 265);
            this.salesDataGrid.TabIndex = 8;
            // 
            // btnSalesAdd
            // 
            this.btnSalesAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalesAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(107)))), ((int)(((byte)(231)))));
            this.btnSalesAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalesAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalesAdd.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSalesAdd.Location = new System.Drawing.Point(605, 149);
            this.btnSalesAdd.Name = "btnSalesAdd";
            this.btnSalesAdd.Size = new System.Drawing.Size(107, 37);
            this.btnSalesAdd.TabIndex = 9;
            this.btnSalesAdd.Text = "Add";
            this.btnSalesAdd.UseVisualStyleBackColor = false;
            // 
            // moduleNamePanel
            // 
            this.moduleNamePanel.Controls.Add(this.lblJetwin);
            this.moduleNamePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.moduleNamePanel.Location = new System.Drawing.Point(0, 0);
            this.moduleNamePanel.Name = "moduleNamePanel";
            this.moduleNamePanel.Size = new System.Drawing.Size(748, 61);
            this.moduleNamePanel.TabIndex = 10;
            // 
            // lblJetwin
            // 
            this.lblJetwin.AutoSize = true;
            this.lblJetwin.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJetwin.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblJetwin.Location = new System.Drawing.Point(12, 20);
            this.lblJetwin.Name = "lblJetwin";
            this.lblJetwin.Size = new System.Drawing.Size(62, 30);
            this.lblJetwin.TabIndex = 6;
            this.lblJetwin.Text = "Sales";
            // 
            // searchFieldPanel
            // 
            this.searchFieldPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.searchFieldPanel.Controls.Add(this.pictureBox1);
            this.searchFieldPanel.Location = new System.Drawing.Point(17, 146);
            this.searchFieldPanel.Name = "searchFieldPanel";
            this.searchFieldPanel.Size = new System.Drawing.Size(371, 40);
            this.searchFieldPanel.TabIndex = 17;
            // 
            // textBoxUnderline1
            // 
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Sales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(748, 486);
            this.Controls.Add(this.searchFieldPanel);
            this.Controls.Add(this.moduleNamePanel);
            this.Controls.Add(this.btnSalesAdd);
            this.Controls.Add(this.salesDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Sales";
            this.Text = "Sales";
            ((System.ComponentModel.ISupportInitialize)(this.salesDataGrid)).EndInit();
            this.moduleNamePanel.ResumeLayout(false);
            this.moduleNamePanel.PerformLayout();
            this.searchFieldPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView salesDataGrid;
        private System.Windows.Forms.Button btnSalesAdd;
        private System.Windows.Forms.Panel moduleNamePanel;
        private System.Windows.Forms.Label lblJetwin;
        private System.Windows.Forms.Panel searchFieldPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}