namespace MultiNuGetPackageInstaller.SettingsWindow
{
    partial class SettingsWindow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SaveBtn = new System.Windows.Forms.Button();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.TemplateNameTxt = new System.Windows.Forms.TextBox();
            this.PackageBoxTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NewBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.ErrorLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(211, 237);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(72, 23);
            this.SaveBtn.TabIndex = 1;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.AutoScroll = true;
            this.ButtonsPanel.Location = new System.Drawing.Point(3, 60);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(130, 171);
            this.ButtonsPanel.TabIndex = 3;
            // 
            // TemplateNameTxt
            // 
            this.TemplateNameTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TemplateNameTxt.Location = new System.Drawing.Point(139, 18);
            this.TemplateNameTxt.Name = "TemplateNameTxt";
            this.TemplateNameTxt.Size = new System.Drawing.Size(100, 20);
            this.TemplateNameTxt.TabIndex = 4;
            // 
            // PackageBoxTxt
            // 
            this.PackageBoxTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PackageBoxTxt.Location = new System.Drawing.Point(139, 60);
            this.PackageBoxTxt.Multiline = true;
            this.PackageBoxTxt.Name = "PackageBoxTxt";
            this.PackageBoxTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PackageBoxTxt.Size = new System.Drawing.Size(400, 171);
            this.PackageBoxTxt.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Template name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Packages";
            // 
            // NewBtn
            // 
            this.NewBtn.Location = new System.Drawing.Point(24, 237);
            this.NewBtn.Name = "NewBtn";
            this.NewBtn.Size = new System.Drawing.Size(75, 23);
            this.NewBtn.TabIndex = 8;
            this.NewBtn.Text = "New";
            this.NewBtn.UseVisualStyleBackColor = true;
            this.NewBtn.Click += new System.EventHandler(this.NewBtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Location = new System.Drawing.Point(361, 237);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(75, 23);
            this.DeleteBtn.TabIndex = 9;
            this.DeleteBtn.Text = "Delete";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // ErrorLbl
            // 
            this.ErrorLbl.ForeColor = System.Drawing.Color.Red;
            this.ErrorLbl.Location = new System.Drawing.Point(136, 263);
            this.ErrorLbl.Name = "ErrorLbl";
            this.ErrorLbl.Size = new System.Drawing.Size(400, 25);
            this.ErrorLbl.TabIndex = 11;
            this.ErrorLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ExtensionUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.ErrorLbl);
            this.Controls.Add(this.NewBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PackageBoxTxt);
            this.Controls.Add(this.TemplateNameTxt);
            this.Controls.Add(this.ButtonsPanel);
            this.Name = "ExtensionUserControl";
            this.Size = new System.Drawing.Size(688, 310);
            this.Load += new System.EventHandler(this.ExtensionUserControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Panel ButtonsPanel;
        private System.Windows.Forms.TextBox TemplateNameTxt;
        private System.Windows.Forms.TextBox PackageBoxTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button NewBtn;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.Label ErrorLbl;
    }
}
