namespace AddressingModes
{
    partial class Form1
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
            this.txtLine = new System.Windows.Forms.TextBox();
            this.txtTuples = new System.Windows.Forms.TextBox();
            this.btnParse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLine
            // 
            this.txtLine.BackColor = System.Drawing.SystemColors.WindowText;
            this.txtLine.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLine.ForeColor = System.Drawing.Color.Lime;
            this.txtLine.Location = new System.Drawing.Point(12, 41);
            this.txtLine.Multiline = true;
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(924, 256);
            this.txtLine.TabIndex = 1;
            // 
            // txtTuples
            // 
            this.txtTuples.BackColor = System.Drawing.SystemColors.WindowText;
            this.txtTuples.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTuples.ForeColor = System.Drawing.Color.Lime;
            this.txtTuples.Location = new System.Drawing.Point(12, 312);
            this.txtTuples.Multiline = true;
            this.txtTuples.Name = "txtTuples";
            this.txtTuples.Size = new System.Drawing.Size(924, 240);
            this.txtTuples.TabIndex = 2;
            // 
            // btnParse
            // 
            this.btnParse.BackColor = System.Drawing.SystemColors.InfoText;
            this.btnParse.ForeColor = System.Drawing.Color.Lime;
            this.btnParse.Location = new System.Drawing.Point(942, 41);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(75, 23);
            this.btnParse.TabIndex = 0;
            this.btnParse.Text = "Parse";
            this.btnParse.UseVisualStyleBackColor = false;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(1016, 562);
            this.Controls.Add(this.txtTuples);
            this.Controls.Add(this.txtLine);
            this.Controls.Add(this.btnParse);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 596);
            this.MinimumSize = new System.Drawing.Size(1024, 596);
            this.Name = "Form1";
            this.Text = "LL(1) Table Driven ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.TextBox txtTuples;
        private System.Windows.Forms.Button btnParse;
    }
}

