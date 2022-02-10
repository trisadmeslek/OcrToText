namespace OcrToText
{
    partial class frmOcrToText
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
            this.iconOcrToText = new System.Windows.Forms.NotifyIcon(this.components);
            this.cxtMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbLang = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.cxtMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // iconOcrToText
            // 
            this.iconOcrToText.ContextMenuStrip = this.cxtMenu;
            this.iconOcrToText.Text = "Ocr To Text";
            this.iconOcrToText.Visible = true;
            // 
            // cxtMenu
            // 
            this.cxtMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemExit});
            this.cxtMenu.Name = "cxtMenu";
            this.cxtMenu.Size = new System.Drawing.Size(94, 26);
            this.cxtMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cxtMenu_ItemClicked);
            // 
            // itemExit
            // 
            this.itemExit.Name = "itemExit";
            this.itemExit.Size = new System.Drawing.Size(93, 22);
            this.itemExit.Text = "Exit";
            // 
            // cmbLang
            // 
            this.cmbLang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbLang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLang.FormattingEnabled = true;
            this.cmbLang.Location = new System.Drawing.Point(82, 12);
            this.cmbLang.Name = "cmbLang";
            this.cmbLang.Size = new System.Drawing.Size(316, 21);
            this.cmbLang.TabIndex = 1;
            this.cmbLang.SelectedIndexChanged += new System.EventHandler(this.cmbLang_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Language : ";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(174, 39);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "Choose";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // frmOcrToText
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 62);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbLang);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmOcrToText";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OCR To Text";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOcrToText_FormClosing);
            this.cxtMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon iconOcrToText;
        private System.Windows.Forms.ContextMenuStrip cxtMenu;
        private System.Windows.Forms.ToolStripMenuItem itemExit;
        private System.Windows.Forms.ComboBox cmbLang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAccept;
    }
}

