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
        private IAsyncResult asyncInvoke;

        public VeeamPS()
        {
            PowerShellInstance = PowerShell.Create();
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
        public BindingList<SelectObject> GetObjects(string selectScript, string selectVal)
        {
            ResetPs();
            var objects = new BindingList<SelectObject>();
            var scr = selectScript + " | select " + selectVal;

            if (!this.VeeamPsLoaded) {
                //throw new Exception("Instance not loaded");

                for (int i = 0; i < 5000; i++)
                {
                    objects.Add(new SelectObject("job" + i));
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
            this.PowerShellInstance.Streams.Verbose.Clear();
            this.PowerShellInstance.Streams.Progress.Clear();
            this.PowerShellInstance.Streams.Warning.Clear();
            this.PowerShellInstance.Streams.ClearStreams();

        }
        public void DirectExecuteBlock(string script)
        {
            System.Console.WriteLine("scr" + script);

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

        public void AsyncExecute(string script)
        {
            ResetPs();

            PowerShellInstance.AddScript(script);
            this.asyncInvoke = PowerShellInstance.BeginInvoke();

        }
        public int AsyncProgress()
        {
            var vbs = PowerShellInstance.Streams.Verbose.ReadAll();
            if (vbs.Count > 0)
            {
                for (var i = vbs.Count; i > 0; i--)
                {
                    var msg = vbs[i - 1].Message;
                    if (msg.Length == 11 && msg.Substring(0, 4) == "PROG" && msg.Substring(8, 3) == "PCT")
                    {
                        return Int32.Parse(msg.Substring(4, 4));
                    }
                }
            }
            return -1;

        }
        public bool AsyncDone()
        {
            if (this.asyncInvoke != null)
            {
                return this.asyncInvoke.IsCompleted;
            }
            return true;
        }
        public void AsyncError()
        {
            if (PowerShellInstance.Streams.Error.Count > 0)
            {
                var err = PowerShellInstance.Streams.Error[0].ToString();
                System.Console.WriteLine(err);
                throw new Exception(err);
            }
        }

        public string BuildStack(ObjectTemplate ot, Template t, string valReal, BindingList<SelectObject> objects)
        {
            var sb = new StringBuilder(1024);
            sb.AppendLine("$verbosepreference='continue';function progvb($prog) { write-verbose (\"PROG{0,4}PCT\" -f $prog); }");
            sb.AppendLine("progvb 5");
            sb.AppendLine("if ( (Get-PSSnapin -Name VeeamPSSnapIn -ErrorAction SilentlyContinue) -eq $null ) { Add-PSSnapin VeeamPSSnapIn;}");
            sb.AppendLine("progvb 10");
            sb.AppendLine("");
            sb.AppendLine("$all = " + ot.Filter);
            sb.AppendLine("$value = " + valReal);
            sb.AppendLine("progvb 20");
            sb.AppendLine("");
            sb.AppendLine("");
            if (t.PreScript != "")
            {
                sb.AppendLine(t.PreScript);
                sb.AppendLine("");
            }
            sb.AppendLine("progvb 30");

            int sels = 0;
            foreach (var obj in objects) { if (obj.selected) { sels++; } }
            double stepsize = 58.0 / sels;
            double cstep = 30;
            double prevemit = cstep;
            double emitinterval = 5;

            foreach (var obj in objects)
            {
                if (obj.selected)
                {
                    sb.AppendLine("$obj = $all | ? { $_." + ot.FilterSelect + " -eq \"" + obj.name + "\" }");


                    sb.AppendLine(t.Script);
                    sb.AppendLine("");

                    if ((cstep - emitinterval) >= prevemit)
                    {
                        sb.AppendLine("progvb " + Math.Round(cstep));
                        prevemit = cstep;
                    }
                    cstep += stepsize;
                }
            }
            sb.AppendLine("progvb 90");
            if (t.PostScript != "")
            {
                sb.AppendLine(t.PostScript);
                sb.AppendLine("");
            }
            sb.AppendLine("progvb 100");
            return sb.ToString();
        }
    }
}
