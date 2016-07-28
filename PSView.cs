using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperEdit
{
    public partial class PSView : Form
    {
        private ResController resController;

        public PSView(string txt,ResController res)
        {
            InitializeComponent();
            this.SetTopLevel(true);
            this.resController = res;
            txtPS.Text = txt;
            txtPS.Select(txtPS.TextLength, 0);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtPS.Text);
        }

        private void btnPsExec_Click(object sender, EventArgs e)
        {
            var prg = new Progress(resController);
            prg.Show();
            prg.exec(txtPS.Text);
            prg.Dispose();
        }
    }
}
