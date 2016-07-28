using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace SuperEdit
{
    public partial class SuperEdit : Form
    {


        public BindingList<SelectObject> objects { get; set; }
        public ResController res;
         

        public SuperEdit(ResController res)
        {
            this.res = res;

            this.objects = new BindingList<SelectObject>();
            

            InitializeComponent();

            this.KeyPreview = true;
        }

        private void listRefresh()
        {
            this.dgvJobs.AutoGenerateColumns = false;
            this.dgvJobs.DataSource = objects;
            this.dgvJobSelect.DataPropertyName = "selected";
            this.dgvJobName.DataPropertyName = "name";
            this.dgvJobs.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboType.DataSource = this.res.templateController.global;
            listRefresh();
        }

        private string getScriptStack()
        {
            var stack = "";
            var ot = ((ObjectTemplate)this.comboType.SelectedValue);
            var t = ((Template)this.comboTemp.SelectedValue);

            var valReal = "";
            try
            {
                var v = ((Value)this.comboVal.SelectedValue);
                valReal = v.Real;
                //no real def, take display val
                if (valReal == "")
                {
                    valReal = v.Display;
                }
            }
            catch (Exception ex)
            {
                valReal = this.comboVal.Text;
            }
            //if not a number, quote
            Int64 c = 0;
            if (!Int64.TryParse(valReal, out c))
            {
                valReal = "\"" + valReal + "\"";
            }

            if (ot != null)
            {
                stack = res.veeamPSController.BuildStack(ot, t, valReal,this.objects);
            }
            return stack;
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            var psv = new PSView(getScriptStack(), this.res);
            psv.ShowDialog();
        }

        private void btnDirectExec_Click(object sender, EventArgs e)
        {
            var scr = getScriptStack();
            var prg = new Progress(res);
            prg.Show();
            prg.exec(scr);
            prg.Dispose();
            
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.CheckState != CheckState.Indeterminate)
            {
                foreach (var obj in objects)
                {
                    obj.selected = chkAll.Checked;
                }
                dgvJobs.Refresh();
            }
        }

        private void thirdstateChkAll()
        {
            if (chkAll.Checked)
            {
                chkAll.ThreeState = true;
                chkAll.CheckState = CheckState.Indeterminate;
                chkAll.ThreeState = false;
            }
        }
        private void dgvJobs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            thirdstateChkAll();
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ot = ((ObjectTemplate)this.comboType.SelectedValue);
            if (ot != null) {
                this.objects = res.veeamPSController.GetObjects(ot.Filter, ot.FilterSelect);
                listRefresh();
                
                comboTemp.DataSource = ot.Templates;
            }
        }

        private void comboTemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboVal.DataSource = ((Template)this.comboTemp.SelectedValue).Values;
        }



        private void SuperEdit_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.F11:
                    MessageBox.Show("Writing template.xml for editing in working directory. Edit and restart app to activate. Remove file to reset");
                    res.templateController.extractInternal();
                    this.Dispose();
                    break;
           
            }
        }


    }
}
