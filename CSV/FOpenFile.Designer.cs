namespace CSV
{
    partial class FOpenFile
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
            this.progBarOpening = new System.Windows.Forms.ProgressBar();
            this.btnCancelOpeningFile = new System.Windows.Forms.Button();
            this.labelOpenFileDirection = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbProgressBar = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.gbProgressBar.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progBarOpening
            // 
            this.progBarOpening.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progBarOpening.Location = new System.Drawing.Point(3, 19);
            this.progBarOpening.Name = "progBarOpening";
            this.progBarOpening.Size = new System.Drawing.Size(483, 24);
            this.progBarOpening.TabIndex = 0;
            // 
            // btnCancelOpeningFile
            // 
            this.btnCancelOpeningFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelOpeningFile.Location = new System.Drawing.Point(382, 131);
            this.btnCancelOpeningFile.Name = "btnCancelOpeningFile";
            this.btnCancelOpeningFile.Size = new System.Drawing.Size(95, 23);
            this.btnCancelOpeningFile.TabIndex = 1;
            this.btnCancelOpeningFile.Text = "Cancel";
            this.btnCancelOpeningFile.UseVisualStyleBackColor = true;
            this.btnCancelOpeningFile.Click += new System.EventHandler(this.btnCancelOpeningFile_Click);
            // 
            // labelOpenFileDirection
            // 
            this.labelOpenFileDirection.AutoSize = true;
            this.labelOpenFileDirection.Location = new System.Drawing.Point(6, 19);
            this.labelOpenFileDirection.Name = "labelOpenFileDirection";
            this.labelOpenFileDirection.Size = new System.Drawing.Size(104, 15);
            this.labelOpenFileDirection.TabIndex = 2;
            this.labelOpenFileDirection.Text = "Opening CSV File:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbProgressBar);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnCancelOpeningFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(489, 166);
            this.panel1.TabIndex = 4;
            // 
            // gbProgressBar
            // 
            this.gbProgressBar.Controls.Add(this.progBarOpening);
            this.gbProgressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbProgressBar.Location = new System.Drawing.Point(0, 73);
            this.gbProgressBar.Name = "gbProgressBar";
            this.gbProgressBar.Size = new System.Drawing.Size(489, 46);
            this.gbProgressBar.TabIndex = 5;
            this.gbProgressBar.TabStop = false;
            this.gbProgressBar.Text = "0%";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelOpenFileDirection);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(489, 73);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // FOpenFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 166);
            this.Controls.Add(this.panel1);
            this.Name = "FOpenFile";
            this.Text = "In progress,,,";
            this.panel1.ResumeLayout(false);
            this.gbProgressBar.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnCancelOpeningFile;
        public Label labelOpenFileDirection;
        public ProgressBar progBarOpening;
        private Panel panel1;
        public GroupBox gbProgressBar;
        private GroupBox groupBox1;
    }
}