using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SuperEdit
{
   public class VeeamPS
    {
        private PowerShell PowerShellInstance;
        private bool VeeamPsLoaded;

        public VeeamPS()
        {
            PowerShellInstance  = PowerShell.Create();
            VeeamPsLoaded = false;

        }
        public void init()
        {
            PowerShellInstance.AddScript("if ( (Get-PSSnapin -Name VeeamPSSnapIn -ErrorAction SilentlyContinue) -eq $null ) { Add-PSSnapin VeeamPSSnapIn;}");
            PowerShellInstance.Invoke();
            

            if (PowerShellInstance.Streams.Error.Count > 0)
            {
                var err = PowerShellInstance.Streams.Error[0].ToString();
                System.Console.WriteLine(err);
                //throw new Exception(err);
            } else
            {
                this.VeeamPsLoaded = true;
            }
        }
        public BindingList<SelectObject> GetObjects(string selectScript,string selectVal)
        {
            ResetPs();
            var objects = new BindingList<SelectObject>();
            var scr = selectScript + " | select " + selectVal;

            if (!this.VeeamPsLoaded) {
                //throw new Exception("Instance not loaded");
                
                for (int i=0;i<100;i++)
                {
                    objects.Add(new SelectObject("job"+i));
                }
                System.Console.WriteLine("Running in stupid dev mode");
                System.Console.WriteLine(scr);
                return objects;
            }

           
            PowerShellInstance.AddScript(scr);
            var PSOutput = PowerShellInstance.Invoke();

            foreach (PSObject outputItem in PSOutput)
            {
                if (outputItem != null)
                {
                    objects.Add(new SelectObject((string)outputItem.Properties[selectVal].Value));
                }
            }
            return objects;
        }


        private void ResetPs()
        {
            this.PowerShellInstance.Commands.Clear();
            this.PowerShellInstance.Streams.Error.Clear();
            this.PowerShellInstance.Streams.Debug.Clear();

        }
        public void DirectExecuteBlock(string script)
        {
            System.Console.WriteLine("scr"+script);

            ResetPs();

            PowerShellInstance.AddScript(script);
            PowerShellInstance.Invoke();


            if (PowerShellInstance.Streams.Error.Count > 0)
            {
                var err = PowerShellInstance.Streams.Error[0].ToString();
                System.Console.WriteLine(err);
                throw new Exception(err);
            }
        }

        public List<string> BuildStack(ObjectTemplate ot, Template t, string valReal, BindingList<SelectObject> objects)
        {
            var sb = new List<string>();
            sb.Add("if ( (Get-PSSnapin -Name VeeamPSSnapIn -ErrorAction SilentlyContinue) -eq $null ) { Add-PSSnapin VeeamPSSnapIn;}");
            sb.Add("");
            sb.Add("$all = " + ot.Filter);
            sb.Add("$value = " + valReal);
            sb.Add("");
            sb.Add("");
            if (t.PreScript != "")
            {
                sb.Add(t.PreScript);
                sb.Add("");
            }
            foreach (var obj in objects)
            {
                if (obj.selected)
                {
                    sb.Add("$obj = $all | ? { $_." + ot.FilterSelect + " -eq \"" + obj.name + "\" }");



                    sb.Add(t.Script);
                    sb.Add("");
                }
            }
            if (t.PostScript != "")
            {
                sb.Add(t.PostScript);
                sb.Add("");
            }
            return sb;
        }
    }
}
