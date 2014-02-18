namespace ConfigZilla.Encrypter
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
            this.components = new System.ComponentModel.Container();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.txtSection = new System.Windows.Forms.TextBox();
            this.lblSection = new System.Windows.Forms.Label();
            this.btnReadSection = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabFiles = new System.Windows.Forms.TabControl();
            this.tabMainFile = new System.Windows.Forms.TabPage();
            this.txtMainOnDisk = new System.Windows.Forms.TextBox();
            this.tabSubFile = new System.Windows.Forms.TabPage();
            this.txtSubFileOnDisk = new System.Windows.Forms.TextBox();
            this.radJustTheSection = new System.Windows.Forms.RadioButton();
            this.radEntireFile = new System.Windows.Forms.RadioButton();
            this.lblOnDisk = new System.Windows.Forms.Label();
            this.lblDecrypted = new System.Windows.Forms.Label();
            this.txtDecrypted = new System.Windows.Forms.TextBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabFiles.SuspendLayout();
            this.tabMainFile.SuspendLayout();
            this.tabSubFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(84, 16);
            this.lblFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(30, 16);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "File";
            // 
            // txtFile
            // 
            this.txtFile.AllowDrop = true;
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(123, 12);
            this.txtFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(587, 22);
            this.txtFile.TabIndex = 1;
            this.txtFile.Text = "C:\\Temp\\test.config";
            this.toolTip1.SetToolTip(this.txtFile, "The config file. Always specify the main file, even if you are using \"configSourc" +
        "e\" to specify sub-files. Drag and drop works if you can find a way to run Explor" +
        "er as admin.");
            this.txtFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtFile_DragDrop);
            this.txtFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtFile_DragEnter);
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFile.Location = new System.Drawing.Point(718, 10);
            this.btnFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(48, 28);
            this.btnFile.TabIndex = 4;
            this.btnFile.Text = "...";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // txtSection
            // 
            this.txtSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSection.Location = new System.Drawing.Point(123, 44);
            this.txtSection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSection.Name = "txtSection";
            this.txtSection.Size = new System.Drawing.Size(587, 22);
            this.txtSection.TabIndex = 3;
            this.txtSection.Text = "connectionStrings";
            this.toolTip1.SetToolTip(this.txtSection, "Name of the section, such as appSettings or connectionStrings.");
            // 
            // lblSection
            // 
            this.lblSection.AutoSize = true;
            this.lblSection.Location = new System.Drawing.Point(16, 48);
            this.lblSection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(93, 16);
            this.lblSection.TabIndex = 2;
            this.lblSection.Text = "Section Name";
            // 
            // btnReadSection
            // 
            this.btnReadSection.Location = new System.Drawing.Point(12, 80);
            this.btnReadSection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReadSection.Name = "btnReadSection";
            this.btnReadSection.Size = new System.Drawing.Size(124, 31);
            this.btnReadSection.TabIndex = 5;
            this.btnReadSection.Text = "Read Section";
            this.toolTip1.SetToolTip(this.btnReadSection, "Read the section and display it as it is on disk and unencrypted.");
            this.btnReadSection.UseVisualStyleBackColor = true;
            this.btnReadSection.Click += new System.EventHandler(this.btnReadSection_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(143, 80);
            this.btnEncrypt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(124, 31);
            this.btnEncrypt.TabIndex = 6;
            this.btnEncrypt.Text = "Encrypt Section";
            this.toolTip1.SetToolTip(this.btnEncrypt, "Encrypt the section and save the .config file. A backup is made before encrypting" +
        ".");
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(275, 80);
            this.btnDecrypt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(124, 31);
            this.btnDecrypt.TabIndex = 7;
            this.btnDecrypt.Text = "Decrypt Section";
            this.toolTip1.SetToolTip(this.btnDecrypt, "Decrypt the section and save the .config file. A backup is made before decrypting" +
        ".");
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 131);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabFiles);
            this.splitContainer1.Panel1.Controls.Add(this.radJustTheSection);
            this.splitContainer1.Panel1.Controls.Add(this.radEntireFile);
            this.splitContainer1.Panel1.Controls.Add(this.lblOnDisk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblDecrypted);
            this.splitContainer1.Panel2.Controls.Add(this.txtDecrypted);
            this.splitContainer1.Size = new System.Drawing.Size(778, 492);
            this.splitContainer1.SplitterDistance = 357;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 8;
            // 
            // tabFiles
            // 
            this.tabFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFiles.Controls.Add(this.tabMainFile);
            this.tabFiles.Controls.Add(this.tabSubFile);
            this.tabFiles.Location = new System.Drawing.Point(3, 30);
            this.tabFiles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabFiles.Name = "tabFiles";
            this.tabFiles.SelectedIndex = 0;
            this.tabFiles.Size = new System.Drawing.Size(352, 461);
            this.tabFiles.TabIndex = 2;
            // 
            // tabMainFile
            // 
            this.tabMainFile.Controls.Add(this.txtMainOnDisk);
            this.tabMainFile.Location = new System.Drawing.Point(4, 25);
            this.tabMainFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabMainFile.Name = "tabMainFile";
            this.tabMainFile.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabMainFile.Size = new System.Drawing.Size(344, 432);
            this.tabMainFile.TabIndex = 0;
            this.tabMainFile.Text = "Main config file";
            this.tabMainFile.UseVisualStyleBackColor = true;
            // 
            // txtMainOnDisk
            // 
            this.txtMainOnDisk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMainOnDisk.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMainOnDisk.Location = new System.Drawing.Point(3, 2);
            this.txtMainOnDisk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMainOnDisk.Multiline = true;
            this.txtMainOnDisk.Name = "txtMainOnDisk";
            this.txtMainOnDisk.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMainOnDisk.Size = new System.Drawing.Size(338, 428);
            this.txtMainOnDisk.TabIndex = 0;
            this.txtMainOnDisk.WordWrap = false;
            // 
            // tabSubFile
            // 
            this.tabSubFile.Controls.Add(this.txtSubFileOnDisk);
            this.tabSubFile.Location = new System.Drawing.Point(4, 25);
            this.tabSubFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabSubFile.Name = "tabSubFile";
            this.tabSubFile.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabSubFile.Size = new System.Drawing.Size(348, 432);
            this.tabSubFile.TabIndex = 1;
            this.tabSubFile.Text = "Sub-config file";
            this.tabSubFile.UseVisualStyleBackColor = true;
            // 
            // txtSubFileOnDisk
            // 
            this.txtSubFileOnDisk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubFileOnDisk.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubFileOnDisk.Location = new System.Drawing.Point(3, 2);
            this.txtSubFileOnDisk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSubFileOnDisk.Multiline = true;
            this.txtSubFileOnDisk.Name = "txtSubFileOnDisk";
            this.txtSubFileOnDisk.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubFileOnDisk.Size = new System.Drawing.Size(342, 428);
            this.txtSubFileOnDisk.TabIndex = 0;
            this.txtSubFileOnDisk.WordWrap = false;
            // 
            // radJustTheSection
            // 
            this.radJustTheSection.AutoSize = true;
            this.radJustTheSection.Location = new System.Drawing.Point(196, 6);
            this.radJustTheSection.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radJustTheSection.Name = "radJustTheSection";
            this.radJustTheSection.Size = new System.Drawing.Size(119, 20);
            this.radJustTheSection.TabIndex = 1;
            this.radJustTheSection.Text = "Just the Section";
            this.radJustTheSection.UseVisualStyleBackColor = true;
            // 
            // radEntireFile
            // 
            this.radEntireFile.AutoSize = true;
            this.radEntireFile.Checked = true;
            this.radEntireFile.Location = new System.Drawing.Point(85, 6);
            this.radEntireFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radEntireFile.Name = "radEntireFile";
            this.radEntireFile.Size = new System.Drawing.Size(85, 20);
            this.radEntireFile.TabIndex = 0;
            this.radEntireFile.TabStop = true;
            this.radEntireFile.Text = "Entire File";
            this.radEntireFile.UseVisualStyleBackColor = true;
            // 
            // lblOnDisk
            // 
            this.lblOnDisk.AutoSize = true;
            this.lblOnDisk.Location = new System.Drawing.Point(14, 8);
            this.lblOnDisk.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOnDisk.Name = "lblOnDisk";
            this.lblOnDisk.Size = new System.Drawing.Size(55, 16);
            this.lblOnDisk.TabIndex = 1;
            this.lblOnDisk.Text = "On Disk";
            // 
            // lblDecrypted
            // 
            this.lblDecrypted.AutoSize = true;
            this.lblDecrypted.Location = new System.Drawing.Point(12, 8);
            this.lblDecrypted.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDecrypted.Name = "lblDecrypted";
            this.lblDecrypted.Size = new System.Drawing.Size(71, 16);
            this.lblDecrypted.TabIndex = 1;
            this.lblDecrypted.Text = "Decrypted";
            // 
            // txtDecrypted
            // 
            this.txtDecrypted.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDecrypted.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDecrypted.Location = new System.Drawing.Point(3, 28);
            this.txtDecrypted.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDecrypted.Multiline = true;
            this.txtDecrypted.Name = "txtDecrypted";
            this.txtDecrypted.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDecrypted.Size = new System.Drawing.Size(405, 463);
            this.txtDecrypted.TabIndex = 0;
            this.txtDecrypted.WordWrap = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(406, 80);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(124, 31);
            this.btnHelp.TabIndex = 9;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnReadSection;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 623);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.btnReadSection);
            this.Controls.Add(this.txtSection);
            this.Controls.Add(this.lblSection);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.lblFile);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabFiles.ResumeLayout(false);
            this.tabMainFile.ResumeLayout(false);
            this.tabMainFile.PerformLayout();
            this.tabSubFile.ResumeLayout(false);
            this.tabSubFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.TextBox txtSection;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Button btnReadSection;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtDecrypted;
        private System.Windows.Forms.Label lblDecrypted;
        private System.Windows.Forms.Label lblOnDisk;
        private System.Windows.Forms.RadioButton radJustTheSection;
        private System.Windows.Forms.RadioButton radEntireFile;
        private System.Windows.Forms.TabControl tabFiles;
        private System.Windows.Forms.TabPage tabMainFile;
        private System.Windows.Forms.TextBox txtMainOnDisk;
        private System.Windows.Forms.TabPage tabSubFile;
        private System.Windows.Forms.TextBox txtSubFileOnDisk;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
