using System.ComponentModel;
using System.Windows.Forms;

namespace SuperEdit
{
    partial class SuperEdit
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
            this.dgvJobs = new System.Windows.Forms.DataGridView();
            this.dgvJobSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvJobName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboVal = new System.Windows.Forms.ComboBox();
            this.btnExec = new System.Windows.Forms.Button();
            this.comboTemp = new System.Windows.Forms.ComboBox();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.btnDirectExec = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobs)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvJobs
            // 
            this.dgvJobs.AllowUserToAddRows = false;
            this.dgvJobs.AllowUserToDeleteRows = false;
            this.dgvJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvJobs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJobs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvJobSelect,
            this.dgvJobName});
            this.dgvJobs.EnableHeadersVisualStyles = false;
            this.dgvJobs.Location = new System.Drawing.Point(12, 46);
            this.dgvJobs.Name = "dgvJobs";
            this.dgvJobs.ShowEditingIcon = false;
            this.dgvJobs.Size = new System.Drawing.Size(820, 389);
            this.dgvJobs.TabIndex = 0;
            this.dgvJobs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJobs_CellContentClick);
            // 
            // dgvJobSelect
            // 
            this.dgvJobSelect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvJobSelect.HeaderText = "Select";
            this.dgvJobSelect.Name = "dgvJobSelect";
            this.dgvJobSelect.Width = 50;
            // 
            // dgvJobName
            // 
            this.dgvJobName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvJobName.HeaderText = "Name";
            this.dgvJobName.Name = "dgvJobName";
            // 
            // comboVal
            // 
            this.comboVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboVal.FormattingEnabled = true;
            this.comboVal.Location = new System.Drawing.Point(225, 451);
            this.comboVal.Name = "comboVal";
            this.comboVal.Size = new System.Drawing.Size(200, 21);
            this.comboVal.TabIndex = 1;
            // 
            // btnExec
            // 
            this.btnExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExec.Location = new System.Drawing.Point(440, 451);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(93, 23);
            this.btnExec.TabIndex = 2;
            this.btnExec.Text = "Execute";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // comboTemp
            // 
            this.comboTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboTemp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTemp.FormattingEnabled = true;
            this.comboTemp.Location = new System.Drawing.Point(12, 451);
            this.comboTemp.Name = "comboTemp";
            this.comboTemp.Size = new System.Drawing.Size(200, 21);
            this.comboTemp.TabIndex = 3;
            this.comboTemp.SelectedIndexChanged += new System.EventHandler(this.comboTemp_SelectedIndexChanged);
            // 
            // comboType
            // 
            this.comboType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FormattingEnabled = true;
            this.comboType.Location = new System.Drawing.Point(575, 12);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(257, 21);
            this.comboType.TabIndex = 4;
            this.comboType.SelectedIndexChanged += new System.EventHandler(this.comboType_SelectedIndexChanged);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(12, 12);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(70, 17);
            this.chkAll.TabIndex = 5;
            this.chkAll.Text = "Select All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // btnDirectExec
            // 
            this.btnDirectExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDirectExec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDirectExec.Location = new System.Drawing.Point(539, 451);
            this.btnDirectExec.Name = "btnDirectExec";
            this.btnDirectExec.Size = new System.Drawing.Size(93, 23);
            this.btnDirectExec.TabIndex = 6;
            this.btnDirectExec.Text = "Potential Nuke";
            this.btnDirectExec.UseVisualStyleBackColor = false;
            this.btnDirectExec.Click += new System.EventHandler(this.btnDirectExec_Click);
            // 
            // SuperEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 486);
            this.Controls.Add(this.btnDirectExec);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.comboType);
            this.Controls.Add(this.comboTemp);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.comboVal);
            this.Controls.Add(this.dgvJobs);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "SuperEdit";
            this.Text = "SuperEdit";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SuperEdit_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvJobs;
        private DataGridViewCheckBoxColumn dgvJobSelect;
        private DataGridViewTextBoxColumn dgvJobName;
        private ComboBox comboVal;
        private Button btnExec;
        private ComboBox comboTemp;
        private ComboBox comboType;
        private CheckBox chkAll;
        private Button btnDirectExec;
    }
}

