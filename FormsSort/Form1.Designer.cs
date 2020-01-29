namespace FormsSort
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
            this.nud_amt = new System.Windows.Forms.NumericUpDown();
            this.btn_Go = new System.Windows.Forms.Button();
            this.btn_how = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_amt)).BeginInit();
            this.SuspendLayout();
            // 
            // nud_amt
            // 
            this.nud_amt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nud_amt.Location = new System.Drawing.Point(12, 12);
            this.nud_amt.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.nud_amt.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nud_amt.Name = "nud_amt";
            this.nud_amt.Size = new System.Drawing.Size(120, 20);
            this.nud_amt.TabIndex = 0;
            this.nud_amt.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // btn_Go
            // 
            this.btn_Go.Location = new System.Drawing.Point(139, 10);
            this.btn_Go.Name = "btn_Go";
            this.btn_Go.Size = new System.Drawing.Size(101, 23);
            this.btn_Go.TabIndex = 1;
            this.btn_Go.Text = "Sort";
            this.btn_Go.UseVisualStyleBackColor = true;
            this.btn_Go.Click += new System.EventHandler(this.btn_Go_Click);
            // 
            // btn_how
            // 
            this.btn_how.Location = new System.Drawing.Point(139, 39);
            this.btn_how.Name = "btn_how";
            this.btn_how.Size = new System.Drawing.Size(101, 23);
            this.btn_how.TabIndex = 2;
            this.btn_how.Text = "HOW";
            this.btn_how.UseVisualStyleBackColor = true;
            this.btn_how.Click += new System.EventHandler(this.btn_how_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 68);
            this.Controls.Add(this.btn_how);
            this.Controls.Add(this.btn_Go);
            this.Controls.Add(this.nud_amt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Sorting Visualiser";
            ((System.ComponentModel.ISupportInitialize)(this.nud_amt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nud_amt;
        private System.Windows.Forms.Button btn_Go;
        private System.Windows.Forms.Button btn_how;
    }
}

