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
    public partial class Progress : Form
    {
        private ResController res;

        public Progress(ResController res)
        {
            InitializeComponent();
            this.res = res;
        }

        public void exec(string script)
        {
            this.pbar.Value = 2;
            this.pbar.Refresh();
            try
            {
                var c = res.veeamPSController;
                c.AsyncExecute(script);
                while (!c.AsyncDone())
                {
                    var testprog = c.AsyncProgress();
                    if (testprog > 0 && testprog < 100)
                    {
                        this.pbar.Value = testprog;
                    }
                    System.Threading.Thread.Sleep(100);
                }
                c.AsyncError();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("First Error occured : " + ex.Message);
            }
        }

        private void Progress_Load(object sender, EventArgs e)
        {

        }
    }
}
