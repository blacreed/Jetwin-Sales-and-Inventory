namespace Jetwin_Sales_and_Inventory
{
    partial class Inventory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventory));
            this.moduleNamePanel = new System.Windows.Forms.Panel();
            this.lblJetwin = new System.Windows.Forms.Label();
            this.inventoryDataGrid = new System.Windows.Forms.DataGridView();
            this.inventoryFooterPanel = new System.Windows.Forms.Panel();
            this.totalProductLabel = new System.Windows.Forms.Label();
            this.searchFieldPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.moduleNamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryDataGrid)).BeginInit();
            this.inventoryFooterPanel.SuspendLayout();
            this.searchFieldPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // moduleNamePanel
            // 
            this.moduleNamePanel.Controls.Add(this.lblJetwin);
            this.moduleNamePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.moduleNamePanel.Location = new System.Drawing.Point(0, 0);
            this.moduleNamePanel.Name = "moduleNamePanel";
            this.moduleNamePanel.Size = new System.Drawing.Size(1024, 61);
            this.moduleNamePanel.TabIndex = 6;
            // 
            // lblJetwin
            // 
            this.lblJetwin.AutoSize = true;
            this.lblJetwin.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJetwin.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblJetwin.Location = new System.Drawing.Point(12, 20);
            this.lblJetwin.Name = "lblJetwin";
            this.lblJetwin.Size = new System.Drawing.Size(109, 30);
            this.lblJetwin.TabIndex = 6;
            this.lblJetwin.Text = "Inventory";
            // 
            // inventoryDataGrid
            // 
            this.inventoryDataGrid.AllowUserToAddRows = false;
            this.inventoryDataGrid.AllowUserToDeleteRows = false;
            this.inventoryDataGrid.AllowUserToResizeColumns = false;
            this.inventoryDataGrid.AllowUserToResizeRows = false;
            this.inventoryDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inventoryDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.inventoryDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(64)))));
            this.inventoryDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.inventoryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.inventoryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.inventoryDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.inventoryDataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(81)))));
            this.inventoryDataGrid.Location = new System.Drawing.Point(17, 177);
            this.inventoryDataGrid.Name = "inventoryDataGrid";
            this.inventoryDataGrid.ReadOnly = true;
            this.inventoryDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.inventoryDataGrid.ShowCellErrors = false;
            this.inventoryDataGrid.ShowCellToolTips = false;
            this.inventoryDataGrid.ShowEditingIcon = false;
            this.inventoryDataGrid.ShowRowErrors = false;
            this.inventoryDataGrid.Size = new System.Drawing.Size(995, 528);
            this.inventoryDataGrid.TabIndex = 13;
            // 
            // inventoryFooterPanel
            // 
            this.inventoryFooterPanel.Controls.Add(this.totalProductLabel);
            this.inventoryFooterPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inventoryFooterPanel.Location = new System.Drawing.Point(0, 726);
            this.inventoryFooterPanel.Name = "inventoryFooterPanel";
            this.inventoryFooterPanel.Size = new System.Drawing.Size(1024, 42);
            this.inventoryFooterPanel.TabIndex = 14;
            // 
            // totalProductLabel
            // 
            this.totalProductLabel.AutoSize = true;
            this.totalProductLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalProductLabel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.totalProductLabel.Location = new System.Drawing.Point(13, 13);
            this.totalProductLabel.Name = "totalProductLabel";
            this.totalProductLabel.Size = new System.Drawing.Size(81, 20);
            this.totalProductLabel.TabIndex = 0;
            this.totalProductLabel.Text = "Products: 0";
            // 
            // searchFieldPanel
            // 
            this.searchFieldPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.searchFieldPanel.Controls.Add(this.pictureBox1);
            this.searchFieldPanel.Location = new System.Drawing.Point(17, 120);
            this.searchFieldPanel.Name = "searchFieldPanel";
            this.searchFieldPanel.Size = new System.Drawing.Size(350, 40);
            this.searchFieldPanel.TabIndex = 17;
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
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.searchFieldPanel);
            this.Controls.Add(this.inventoryFooterPanel);
            this.Controls.Add(this.inventoryDataGrid);
            this.Controls.Add(this.moduleNamePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Inventory";
            this.Text = "Inventory";
            this.moduleNamePanel.ResumeLayout(false);
            this.moduleNamePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryDataGrid)).EndInit();
            this.inventoryFooterPanel.ResumeLayout(false);
            this.inventoryFooterPanel.PerformLayout();
            this.searchFieldPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel moduleNamePanel;
        private System.Windows.Forms.Label lblJetwin;
        private System.Windows.Forms.DataGridView inventoryDataGrid;
        private System.Windows.Forms.Panel inventoryFooterPanel;
        private System.Windows.Forms.Label totalProductLabel;
        private System.Windows.Forms.Panel searchFieldPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}