using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SuperEdit
{
    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var err = "";
            ResController ctr=null;
            try
            {
               ctr = new ResController();
            } catch (Exception e)
            {
                err = e.Message;
            }

            if (ctr != null)
            {
                Application.Run(new SuperEdit(ctr));
            } else
            {
                MessageBox.Show("Init error " + err);
                Application.Exit();
            }
          

        }
    }
}
