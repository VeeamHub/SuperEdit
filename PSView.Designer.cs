namespace SuperEdit
{
    partial class PSView
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
            this.txtPS = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnPsExec = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPS
            // 
            this.txtPS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPS.Location = new System.Drawing.Point(12, 12);
            this.txtPS.Multiline = true;
            this.txtPS.Name = "txtPS";
            this.txtPS.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPS.Size = new System.Drawing.Size(760, 410);
            this.txtPS.TabIndex = 0;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(691, 429);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "Copy All";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPsExec
            // 
            this.btnPsExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPsExec.Location = new System.Drawing.Point(610, 429);
            this.btnPsExec.Name = "btnPsExec";
            this.btnPsExec.Size = new System.Drawing.Size(75, 23);
            this.btnPsExec.TabIndex = 2;
            this.btnPsExec.Text = "PS Exec";
            this.btnPsExec.UseVisualStyleBackColor = true;
            this.btnPsExec.Click += new System.EventHandler(this.btnPsExec_Click);
            // 
            // PSView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnPsExec);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtPS);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "PSView";
            this.Text = "PSView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPS;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnPsExec;
    }
}