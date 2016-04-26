namespace MultiNuGetPackageInstaller.MainWindow
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.InstallBtn = new System.Windows.Forms.Button();
            this.PackagesBox = new System.Windows.Forms.RichTextBox();
            this.infoBtn = new System.Windows.Forms.PictureBox();
            this.ProjectsPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.TemplatesPanel = new System.Windows.Forms.Panel();
            this.ErrorLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.HelpPanel = new System.Windows.Forms.Panel();
            this.BackBtn = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoBtn)).BeginInit();
            this.HelpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(316, -19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(201, 201);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.Title.Location = new System.Drawing.Point(251, 166);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(331, 28);
            this.Title.TabIndex = 1;
            this.Title.Text = "Multi NuGet package installer (BETA)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label1.Location = new System.Drawing.Point(281, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Developed by Francisco López Sánchez";
            // 
            // InstallBtn
            // 
            this.InstallBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.InstallBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InstallBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InstallBtn.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.InstallBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.InstallBtn.Location = new System.Drawing.Point(338, 442);
            this.InstallBtn.Name = "InstallBtn";
            this.InstallBtn.Size = new System.Drawing.Size(150, 57);
            this.InstallBtn.TabIndex = 3;
            this.InstallBtn.Text = "Install";
            this.InstallBtn.UseVisualStyleBackColor = false;
            this.InstallBtn.EnabledChanged += new System.EventHandler(this.InstallBtn_EnabledChanged);
            this.InstallBtn.Click += new System.EventHandler(this.InstallBtn_Click);
            // 
            // PackagesBox
            // 
            this.PackagesBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PackagesBox.EnableAutoDragDrop = true;
            this.PackagesBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.PackagesBox.Location = new System.Drawing.Point(200, 240);
            this.PackagesBox.Name = "PackagesBox";
            this.PackagesBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.PackagesBox.Size = new System.Drawing.Size(435, 173);
            this.PackagesBox.TabIndex = 5;
            this.PackagesBox.Text = "";
            // 
            // infoBtn
            // 
            this.infoBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.infoBtn.Image = ((System.Drawing.Image)(resources.GetObject("infoBtn.Image")));
            this.infoBtn.Location = new System.Drawing.Point(775, 12);
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(47, 41);
            this.infoBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.infoBtn.TabIndex = 6;
            this.infoBtn.TabStop = false;
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // ProjectsPanel
            // 
            this.ProjectsPanel.AutoScroll = true;
            this.ProjectsPanel.Location = new System.Drawing.Point(641, 240);
            this.ProjectsPanel.Name = "ProjectsPanel";
            this.ProjectsPanel.Size = new System.Drawing.Size(181, 173);
            this.ProjectsPanel.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(638, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Solution projects";
            // 
            // TemplatesPanel
            // 
            this.TemplatesPanel.AutoScroll = true;
            this.TemplatesPanel.Location = new System.Drawing.Point(12, 240);
            this.TemplatesPanel.Name = "TemplatesPanel";
            this.TemplatesPanel.Size = new System.Drawing.Size(182, 173);
            this.TemplatesPanel.TabIndex = 8;
            // 
            // ErrorLbl
            // 
            this.ErrorLbl.ForeColor = System.Drawing.Color.Red;
            this.ErrorLbl.Location = new System.Drawing.Point(12, 426);
            this.ErrorLbl.Name = "ErrorLbl";
            this.ErrorLbl.Size = new System.Drawing.Size(810, 13);
            this.ErrorLbl.TabIndex = 9;
            this.ErrorLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "My templates";
            // 
            // ClearBtn
            // 
            this.ClearBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClearBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearBtn.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ClearBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClearBtn.Location = new System.Drawing.Point(506, 456);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(60, 32);
            this.ClearBtn.TabIndex = 11;
            this.ClearBtn.Text = "Clear";
            this.ClearBtn.UseVisualStyleBackColor = false;
            this.ClearBtn.Visible = false;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ExitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitBtn.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ExitBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ExitBtn.Location = new System.Drawing.Point(762, 467);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(60, 32);
            this.ExitBtn.TabIndex = 12;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.EnabledChanged += new System.EventHandler(this.InstallBtn_EnabledChanged);
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // HelpPanel
            // 
            this.HelpPanel.Controls.Add(this.linkLabel1);
            this.HelpPanel.Controls.Add(this.label7);
            this.HelpPanel.Controls.Add(this.richTextBox3);
            this.HelpPanel.Controls.Add(this.richTextBox1);
            this.HelpPanel.Controls.Add(this.label6);
            this.HelpPanel.Controls.Add(this.label5);
            this.HelpPanel.Controls.Add(this.label4);
            this.HelpPanel.Location = new System.Drawing.Point(12, 217);
            this.HelpPanel.Name = "HelpPanel";
            this.HelpPanel.Size = new System.Drawing.Size(810, 282);
            this.HelpPanel.TabIndex = 13;
            this.HelpPanel.Visible = false;
            // 
            // BackBtn
            // 
            this.BackBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackBtn.Image = ((System.Drawing.Image)(resources.GetObject("BackBtn.Image")));
            this.BackBtn.Location = new System.Drawing.Point(775, 12);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(47, 41);
            this.BackBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BackBtn.TabIndex = 14;
            this.BackBtn.TabStop = false;
            this.BackBtn.Visible = false;
            this.BackBtn.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(291, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Each textarea line represents a single package to install";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(195, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "To install latest version of a package:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "To install specific version:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(15, 81);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(255, 57);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "EntityFramework\nBootstrap\njQuery";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Enabled = false;
            this.richTextBox3.Location = new System.Drawing.Point(15, 166);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(255, 57);
            this.richTextBox3.TabIndex = 5;
            this.richTextBox3.Text = "EntityFramework 6.1.3    // One space separator\nBootstrap 3.3.6\njQuery 2.2.3";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(411, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(373, 123);
            this.label7.TabIndex = 6;
            this.label7.Text = "You can create, modify or delete your own templates in:\r\nOptions > Multi NuGet Pa" +
    "ckage Installer > Templates\r\n\r\n";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(411, 209);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(131, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Go to GitHub repository";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 511);
            this.Controls.Add(this.BackBtn);
            this.Controls.Add(this.HelpPanel);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ErrorLbl);
            this.Controls.Add(this.TemplatesPanel);
            this.Controls.Add(this.ProjectsPanel);
            this.Controls.Add(this.infoBtn);
            this.Controls.Add(this.PackagesBox);
            this.Controls.Add(this.InstallBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoBtn)).EndInit();
            this.HelpPanel.ResumeLayout(false);
            this.HelpPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button InstallBtn;
        private System.Windows.Forms.RichTextBox PackagesBox;
        private System.Windows.Forms.PictureBox infoBtn;
        private System.Windows.Forms.Panel ProjectsPanel;
        private System.Windows.Forms.Panel TemplatesPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ErrorLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Panel HelpPanel;
        private System.Windows.Forms.PictureBox BackBtn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}