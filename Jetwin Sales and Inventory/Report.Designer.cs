namespace Jetwin_Sales_and_Inventory
{
    partial class Report
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
            this.label1 = new System.Windows.Forms.Label();
            this.moduleNamePanel = new System.Windows.Forms.Panel();
            this.lblJetwin = new System.Windows.Forms.Label();
            this.moduleNamePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(271, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 65);
            this.label1.TabIndex = 1;
            this.label1.Text = "Report";
            // 
            // moduleNamePanel
            // 
            this.moduleNamePanel.Controls.Add(this.lblJetwin);
            this.moduleNamePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.moduleNamePanel.Location = new System.Drawing.Point(0, 0);
            this.moduleNamePanel.Name = "moduleNamePanel";
            this.moduleNamePanel.Size = new System.Drawing.Size(748, 61);
            this.moduleNamePanel.TabIndex = 6;
            // 
            // lblJetwin
            // 
            this.lblJetwin.AutoSize = true;
            this.lblJetwin.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJetwin.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblJetwin.Location = new System.Drawing.Point(12, 20);
            this.lblJetwin.Name = "lblJetwin";
            this.lblJetwin.Size = new System.Drawing.Size(80, 30);
            this.lblJetwin.TabIndex = 6;
            this.lblJetwin.Text = "Report";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(748, 486);
            this.Controls.Add(this.moduleNamePanel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Report";
            this.Text = "Report";
            this.moduleNamePanel.ResumeLayout(false);
            this.moduleNamePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel moduleNamePanel;
        private System.Windows.Forms.Label lblJetwin;
    }
}