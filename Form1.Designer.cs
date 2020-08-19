namespace filder
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
            this.Bt_Proc = new System.Windows.Forms.Button();
            this.Bt_Clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Bt_Proc
            // 
            this.Bt_Proc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Bt_Proc.Location = new System.Drawing.Point(1115, 506);
            this.Bt_Proc.Name = "Bt_Proc";
            this.Bt_Proc.Size = new System.Drawing.Size(75, 23);
            this.Bt_Proc.TabIndex = 0;
            this.Bt_Proc.Text = "Process";
            this.Bt_Proc.UseVisualStyleBackColor = true;
            this.Bt_Proc.Click += new System.EventHandler(this.Bt_Proc_Click);
            // 
            // Bt_Clear
            // 
            this.Bt_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Bt_Clear.Location = new System.Drawing.Point(1115, 465);
            this.Bt_Clear.Name = "Bt_Clear";
            this.Bt_Clear.Size = new System.Drawing.Size(75, 23);
            this.Bt_Clear.TabIndex = 1;
            this.Bt_Clear.Text = "Clear";
            this.Bt_Clear.UseVisualStyleBackColor = true;
            this.Bt_Clear.Click += new System.EventHandler(this.Bt_Clear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1202, 541);
            this.Controls.Add(this.Bt_Clear);
            this.Controls.Add(this.Bt_Proc);
            this.Name = "Form1";
            this.Opacity = 0.5D;
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MousClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Bt_Proc;
        private System.Windows.Forms.Button Bt_Clear;
    }
}

