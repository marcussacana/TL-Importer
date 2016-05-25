namespace TL_Importer {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OpenOTS = new System.Windows.Forms.Button();
            this.OpenOUS = new System.Windows.Forms.Button();
            this.OpenOOS = new System.Windows.Forms.Button();
            this.PathOTS = new System.Windows.Forms.TextBox();
            this.PathOUS = new System.Windows.Forms.TextBox();
            this.PathOOS = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.OriginalString = new System.Windows.Forms.ListBox();
            this.TranslationString = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processTranslationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editAlgoritmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Status = new System.Windows.Forms.ToolStripTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.OpenOTS);
            this.groupBox1.Controls.Add(this.OpenOUS);
            this.groupBox1.Controls.Add(this.OpenOOS);
            this.groupBox1.Controls.Add(this.PathOTS);
            this.groupBox1.Controls.Add(this.PathOUS);
            this.groupBox1.Controls.Add(this.PathOOS);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 394);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 107);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project:";
            // 
            // OpenOTS
            // 
            this.OpenOTS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenOTS.Location = new System.Drawing.Point(775, 71);
            this.OpenOTS.Name = "OpenOTS";
            this.OpenOTS.Size = new System.Drawing.Size(37, 23);
            this.OpenOTS.TabIndex = 8;
            this.OpenOTS.Text = "...";
            this.OpenOTS.UseVisualStyleBackColor = true;
            this.OpenOTS.Click += new System.EventHandler(this.OpenOTS_Click);
            // 
            // OpenOUS
            // 
            this.OpenOUS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenOUS.Location = new System.Drawing.Point(775, 43);
            this.OpenOUS.Name = "OpenOUS";
            this.OpenOUS.Size = new System.Drawing.Size(37, 23);
            this.OpenOUS.TabIndex = 7;
            this.OpenOUS.Text = "...";
            this.OpenOUS.UseVisualStyleBackColor = true;
            this.OpenOUS.Click += new System.EventHandler(this.OpenOUS_Click);
            // 
            // OpenOOS
            // 
            this.OpenOOS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenOOS.Location = new System.Drawing.Point(775, 15);
            this.OpenOOS.Name = "OpenOOS";
            this.OpenOOS.Size = new System.Drawing.Size(37, 23);
            this.OpenOOS.TabIndex = 6;
            this.OpenOOS.Text = "...";
            this.OpenOOS.UseVisualStyleBackColor = true;
            this.OpenOOS.Click += new System.EventHandler(this.OpenOOS_Click);
            // 
            // PathOTS
            // 
            this.PathOTS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathOTS.Location = new System.Drawing.Point(200, 71);
            this.PathOTS.Name = "PathOTS";
            this.PathOTS.ReadOnly = true;
            this.PathOTS.Size = new System.Drawing.Size(569, 22);
            this.PathOTS.TabIndex = 5;
            this.PathOTS.Text = "Select a script...";
            // 
            // PathOUS
            // 
            this.PathOUS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathOUS.Location = new System.Drawing.Point(200, 43);
            this.PathOUS.Name = "PathOUS";
            this.PathOUS.ReadOnly = true;
            this.PathOUS.Size = new System.Drawing.Size(569, 22);
            this.PathOUS.TabIndex = 4;
            this.PathOUS.Text = "Select a script...";
            // 
            // PathOOS
            // 
            this.PathOOS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathOOS.Location = new System.Drawing.Point(200, 15);
            this.PathOOS.Name = "PathOOS";
            this.PathOOS.ReadOnly = true;
            this.PathOOS.Size = new System.Drawing.Size(569, 22);
            this.PathOOS.TabIndex = 3;
            this.PathOOS.Text = "Select a script...";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Outdated Translated Script:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Original Updated Script:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Original Outdated Script:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 34);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.OriginalString);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TranslationString);
            this.splitContainer1.Size = new System.Drawing.Size(818, 354);
            this.splitContainer1.SplitterDistance = 403;
            this.splitContainer1.TabIndex = 1;
            // 
            // OriginalString
            // 
            this.OriginalString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OriginalString.FormattingEnabled = true;
            this.OriginalString.ItemHeight = 16;
            this.OriginalString.Location = new System.Drawing.Point(3, 3);
            this.OriginalString.Name = "OriginalString";
            this.OriginalString.Size = new System.Drawing.Size(396, 340);
            this.OriginalString.TabIndex = 1;
            this.OriginalString.SelectedIndexChanged += new System.EventHandler(this.OriginalString_SelectedIndexChanged);
            // 
            // TranslationString
            // 
            this.TranslationString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TranslationString.FormattingEnabled = true;
            this.TranslationString.ItemHeight = 16;
            this.TranslationString.Location = new System.Drawing.Point(3, 3);
            this.TranslationString.Name = "TranslationString";
            this.TranslationString.Size = new System.Drawing.Size(405, 340);
            this.TranslationString.TabIndex = 0;
            this.TranslationString.SelectedIndexChanged += new System.EventHandler(this.TranslationString_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.Status});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(842, 31);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processTranslationToolStripMenuItem,
            this.exportScriptToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(67, 27);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // processTranslationToolStripMenuItem
            // 
            this.processTranslationToolStripMenuItem.Name = "processTranslationToolStripMenuItem";
            this.processTranslationToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.processTranslationToolStripMenuItem.Text = "Process Comparsion";
            this.processTranslationToolStripMenuItem.Click += new System.EventHandler(this.processTranslationToolStripMenuItem_Click);
            // 
            // exportScriptToolStripMenuItem
            // 
            this.exportScriptToolStripMenuItem.Name = "exportScriptToolStripMenuItem";
            this.exportScriptToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.exportScriptToolStripMenuItem.Text = "Export Script";
            this.exportScriptToolStripMenuItem.Click += new System.EventHandler(this.exportScriptToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editAlgoritmsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(73, 27);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // editAlgoritmsToolStripMenuItem
            // 
            this.editAlgoritmsToolStripMenuItem.Name = "editAlgoritmsToolStripMenuItem";
            this.editAlgoritmsToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.editAlgoritmsToolStripMenuItem.Text = "Edit Algorithm";
            this.editAlgoritmsToolStripMenuItem.Click += new System.EventHandler(this.editAlgoritmsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.aboutToolStripMenuItem.Text = "About/Help";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Status
            // 
            this.Status.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Size = new System.Drawing.Size(400, 27);
            this.Status.Text = "Waiting...";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 513);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(860, 560);
            this.Name = "Main";
            this.Text = "TL Importer Engine - Patch Updater Tool";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button OpenOTS;
        private System.Windows.Forms.Button OpenOUS;
        private System.Windows.Forms.Button OpenOOS;
        private System.Windows.Forms.TextBox PathOTS;
        private System.Windows.Forms.TextBox PathOUS;
        private System.Windows.Forms.TextBox PathOOS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processTranslationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editAlgoritmsToolStripMenuItem;
        private System.Windows.Forms.ListBox OriginalString;
        private System.Windows.Forms.ListBox TranslationString;
        private System.Windows.Forms.ToolStripTextBox Status;
    }
}

