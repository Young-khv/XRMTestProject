namespace XTRMVersionsViewer
{
    partial class MainForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.versionsTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.classesListBox = new System.Windows.Forms.ListBox();
            this.usersListBox = new System.Windows.Forms.ListBox();
            this.sourceCodeRTB = new System.Windows.Forms.RichTextBox();
            this.versionsList = new System.Windows.Forms.ListBox();
            this.commentRTB = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.versionsTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.commentRTB);
            this.splitContainer1.Panel1.Controls.Add(this.versionsList);
            this.splitContainer1.Panel1.Controls.Add(this.versionsTab);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sourceCodeRTB);
            this.splitContainer1.Size = new System.Drawing.Size(872, 665);
            this.splitContainer1.SplitterDistance = 241;
            this.splitContainer1.TabIndex = 0;
            // 
            // versionsTab
            // 
            this.versionsTab.Controls.Add(this.tabPage1);
            this.versionsTab.Controls.Add(this.tabPage2);
            this.versionsTab.Dock = System.Windows.Forms.DockStyle.Left;
            this.versionsTab.Location = new System.Drawing.Point(0, 0);
            this.versionsTab.Name = "versionsTab";
            this.versionsTab.SelectedIndex = 0;
            this.versionsTab.Size = new System.Drawing.Size(410, 237);
            this.versionsTab.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.classesListBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(402, 211);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Поиск по классам";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.usersListBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(402, 211);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Посик по пользователям";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // classesListBox
            // 
            this.classesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classesListBox.FormattingEnabled = true;
            this.classesListBox.Location = new System.Drawing.Point(3, 3);
            this.classesListBox.Name = "classesListBox";
            this.classesListBox.Size = new System.Drawing.Size(396, 205);
            this.classesListBox.TabIndex = 0;
            this.classesListBox.SelectedIndexChanged += new System.EventHandler(this.classesListBox_SelectedIndexChanged);
            // 
            // usersListBox
            // 
            this.usersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersListBox.FormattingEnabled = true;
            this.usersListBox.Location = new System.Drawing.Point(3, 3);
            this.usersListBox.Name = "usersListBox";
            this.usersListBox.Size = new System.Drawing.Size(396, 205);
            this.usersListBox.TabIndex = 0;
            this.usersListBox.SelectedIndexChanged += new System.EventHandler(this.usersListBox_SelectedIndexChanged);
            // 
            // sourceCodeRTB
            // 
            this.sourceCodeRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceCodeRTB.Location = new System.Drawing.Point(0, 0);
            this.sourceCodeRTB.Name = "sourceCodeRTB";
            this.sourceCodeRTB.ReadOnly = true;
            this.sourceCodeRTB.Size = new System.Drawing.Size(868, 416);
            this.sourceCodeRTB.TabIndex = 0;
            this.sourceCodeRTB.Text = "";
            // 
            // versionsList
            // 
            this.versionsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.versionsList.FormattingEnabled = true;
            this.versionsList.Location = new System.Drawing.Point(410, 0);
            this.versionsList.Name = "versionsList";
            this.versionsList.Size = new System.Drawing.Size(458, 237);
            this.versionsList.TabIndex = 1;
            this.versionsList.SelectedIndexChanged += new System.EventHandler(this.versionsList_SelectedIndexChanged);
            // 
            // commentRTB
            // 
            this.commentRTB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.commentRTB.Location = new System.Drawing.Point(410, 141);
            this.commentRTB.Name = "commentRTB";
            this.commentRTB.ReadOnly = true;
            this.commentRTB.Size = new System.Drawing.Size(458, 96);
            this.commentRTB.TabIndex = 2;
            this.commentRTB.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 665);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Просмотр истории версий";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.versionsTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl versionsTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox classesListBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox usersListBox;
        private System.Windows.Forms.RichTextBox sourceCodeRTB;
        private System.Windows.Forms.RichTextBox commentRTB;
        private System.Windows.Forms.ListBox versionsList;

    }
}

