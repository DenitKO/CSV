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
            this.labelOpenPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progBarOpening
            // 
            this.progBarOpening.Location = new System.Drawing.Point(12, 84);
            this.progBarOpening.Name = "progBarOpening";
            this.progBarOpening.Size = new System.Drawing.Size(447, 23);
            this.progBarOpening.TabIndex = 0;
            // 
            // btnCancelOpeningFile
            // 
            this.btnCancelOpeningFile.Location = new System.Drawing.Point(364, 127);
            this.btnCancelOpeningFile.Name = "btnCancelOpeningFile";
            this.btnCancelOpeningFile.Size = new System.Drawing.Size(95, 23);
            this.btnCancelOpeningFile.TabIndex = 1;
            this.btnCancelOpeningFile.Text = "Cancel";
            this.btnCancelOpeningFile.UseVisualStyleBackColor = true;
            // 
            // labelOpenFileDirection
            // 
            this.labelOpenFileDirection.AutoSize = true;
            this.labelOpenFileDirection.Location = new System.Drawing.Point(14, 20);
            this.labelOpenFileDirection.Name = "labelOpenFileDirection";
            this.labelOpenFileDirection.Size = new System.Drawing.Size(104, 15);
            this.labelOpenFileDirection.TabIndex = 2;
            this.labelOpenFileDirection.Text = "Opening CSV File:";
            // 
            // labelOpenPercent
            // 
            this.labelOpenPercent.AutoSize = true;
            this.labelOpenPercent.Location = new System.Drawing.Point(12, 66);
            this.labelOpenPercent.Name = "labelOpenPercent";
            this.labelOpenPercent.Size = new System.Drawing.Size(24, 15);
            this.labelOpenPercent.TabIndex = 3;
            this.labelOpenPercent.Text = "0%";
            // 
            // FOpenFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 162);
            this.Controls.Add(this.labelOpenPercent);
            this.Controls.Add(this.labelOpenFileDirection);
            this.Controls.Add(this.btnCancelOpeningFile);
            this.Controls.Add(this.progBarOpening);
            this.Name = "FOpenFile";
            this.Text = "In progress,,,";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProgressBar progBarOpening;
        private Button btnCancelOpeningFile;
        private Label labelOpenFileDirection;
        private Label labelOpenPercent;
    }
}