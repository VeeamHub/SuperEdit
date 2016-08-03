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
using System.Text.RegularExpressions;

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
            if (e.ColumnIndex == dgvJobSelect.Index)
            {
                thirdstateChkAll();
            }
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
            var templ = (Template)this.comboTemp.SelectedValue;
            
            if (templ.hasDynamicValues())
            {
                comboVal.DataSource = res.veeamPSController.GetDynamicValues(templ.DynValScript);
            } else
            {
                comboVal.DataSource = templ.Values;
            }

            
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
                case Keys.Add:
                    
                    if (dgvJobs.SelectedRows.Count == 1)
                    {
                        var ind = dgvJobs.SelectedRows[0].Index;
                        if (ind > 0)
                        {
                            
                            var o = objects[ind];
                            this.objects.Remove(o);
                            this.objects.Insert(ind - 1, o);
                            foreach(DataGridViewRow row in dgvJobs.Rows)
                            {
                                if (row.Selected)
                                {
                                    row.Selected = false;
                                }
                            }
                            dgvJobs.Rows[ind - 1].Selected = true;
                            dgvJobs.CurrentCell = dgvJobs.Rows[ind - 1].Cells[0];
                           
                        }
                        
                    }
                    break;
                case Keys.Subtract:

                    if (dgvJobs.SelectedRows.Count == 1)
                    {
                        var ind = dgvJobs.SelectedRows[0].Index;
                        if (ind < (dgvJobs.Rows.Count-1))
                        {

                            var o = objects[ind];
                            this.objects.Remove(o);
                            this.objects.Insert(ind + 1, o);
                            foreach (DataGridViewRow row in dgvJobs.Rows)
                            {
                                if (row.Selected)
                                {
                                    row.Selected = false;
                                }
                            }
                            dgvJobs.Rows[ind + 1].Selected = true;
                            dgvJobs.CurrentCell = dgvJobs.Rows[ind + 1].Cells[0];

                        }

                    }
                    break;
                default:
                    //MessageBox.Show(e.KeyCode.ToString());
                    break;
            }
        }

        private void ctxTop_Click(object sender, EventArgs e)
        {
            var rs = dgvJobs.SelectedRows;
            var movelist = new List<SelectObject>();
            
            //selection is inversed what you should expect
            //that is ok, we will insert at location 0 so the first ones inserted will be pushed down
            //store them first in a list not to change the order
            for(int ri= 0;ri < rs.Count; ri++)
            {
                movelist.Add(objects[rs[ri].Index]);     
            }
            foreach(var o in movelist)
            {
                this.objects.Remove(o);
                this.objects.Insert(0, o);
            }
            
           
        }

        private void ctxBottom_Click(object sender, EventArgs e)
        {
            var rs = dgvJobs.SelectedRows;
            var movelist = new List<SelectObject>();

            //selection is inversed what you should expect
            //that is ok, we will insert at location 0 so the first ones inserted will be pushed down
            //store them first in a list not to change the order
            for (int ri = 0; ri < rs.Count; ri++)
            {
                movelist.Add(objects[rs[ri].Index]);
            }

            foreach (var o in movelist)
            {
                this.objects.Remove(o);
                this.objects.Add(o);
            }
        }

        private void ctxRegex_Click(object sender, EventArgs e)
        {
            InputFormObject ifo = new InputFormObject();
            var ib = new Inputbox(ifo,"Regex");
            ib.ShowDialog();

            if (ifo.gotOk)
            {
                var rp = ifo.value;

                Regex r = new Regex(rp, RegexOptions.IgnoreCase);

                var removelist = new List<SelectObject>();
                foreach (var o in objects)
                {
                    if (!r.IsMatch(o.name))
                    {
                        removelist.Add(o);
                    }
                }
                foreach (var o in removelist)
                {
                    objects.Remove(o);
                }
            }
        }

        private void dgvJobs_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex == dgvJobName.DisplayIndex)
            {
                
            }
        }
    }

}
