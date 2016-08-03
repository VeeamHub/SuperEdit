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

    public partial class Inputbox : Form
    {
        private InputFormObject ifo;

        public Inputbox(InputFormObject ifo,String topText)
        {
            InitializeComponent();
            this.ifo = ifo;
            this.Text = topText;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.ifo.gotOk = true;
            this.ifo.value = this.txtInput.Text;
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

    public class InputFormObject
    {
        public bool gotOk { get; set; }
        public string value { get; set; }

        public InputFormObject()
        {
            this.gotOk = false;
            this.value = "";
        }
    }
}
