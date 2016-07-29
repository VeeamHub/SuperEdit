using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SuperEdit
{
    /*
     * <GlobalTemplates>
  <ObjectTemplate name="Backup VMware">
    <objectfilter select="Name">
      <![CDATA[
        get-vbrjob | ? { $_.jobtype -eq "Backup" -and $_.BackupPlatform -eq "EVmware" }
      ]]>
    </objectfilter>
    <template name="Dirty Block">
      <script>
        <![CDATA[
        $opt = $obj | get-vbrjoboptions
        $opt.ViSourceOptions.ExcludeSwapFile = $value
        $obj | set-vbrjoboptions -Options $opt
        ]]>
      </script>
      <values>
        <value real="1">On</value>
        <value real="0">Off</value>
      </values>
    </template>
  </ObjectTemplate>
</GlobalTemplates>

     * */
    public class TemplateController
    {
        public List<ObjectTemplate> global { get; set; }
        public const string externalFile = "Templates.xml";

        public TemplateController()
        {
            global = new List<ObjectTemplate>();
        }

        public void extractInternal()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var templstream = assembly.GetManifestResourceStream("SuperEdit.Templates.xml");

            try {
                System.IO.FileStream fileStream = new System.IO.FileStream(externalFile, System.IO.FileMode.CreateNew);
                for(int i = 0; i < templstream.Length; i++) {
                    fileStream.WriteByte((byte)templstream.ReadByte());
                }
                fileStream.Close();
            } catch (Exception e) {
                
            }
            
        }

        public void init()
        {
            XmlDocument doc = new XmlDocument();
            

            if (File.Exists(externalFile)) {
                doc.Load(externalFile);
            } else {
                var assembly = Assembly.GetExecutingAssembly();
                var templstream = assembly.GetManifestResourceStream("SuperEdit.Templates.xml");
                doc.Load(templstream);
            }
            
            

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("//objectTemplate");

            foreach (XmlNode node in nodes)
            {
                var objt = new ObjectTemplate();
                objt.Name = node.Attributes["name"].Value;

                var filternode = node.SelectSingleNode("objectfilter");
                var templates = node.SelectNodes("template");

                //Console.WriteLine("Node " + objt.Name);
                //Console.WriteLine("Filter " + filternode);
                //Console.WriteLine("TemplC " + templates.Count);

                if (filternode != null && templates.Count > 0)
                {
                    objt.Filter = filternode.InnerText.Trim();
                    objt.FilterSelect = filternode.Attributes["select"].Value;

                    //Console.WriteLine("Filter Select" + objt.FilterSelect);
                    //Console.WriteLine("Filter txt" + objt.Filter);
                    foreach (XmlNode t in templates)
                    {
                        var templ = new Template();
                        templ.Name = t.Attributes["name"].Value;
                        templ.Script = t.SelectSingleNode("script").InnerText.Trim();

                        var pres = t.SelectSingleNode("prescript");
                        if (pres != null)
                        {
                            templ.PreScript = pres.InnerText.Trim();
                        }
                        var post = t.SelectSingleNode("postscript");
                        if (post != null)
                        {
                            templ.PostScript = post.InnerText.Trim();
                        }


                        var vals = t.SelectNodes("values/value");

                        var dynval = t.SelectSingleNode("dynamicvalue");

                        if (vals.Count > 0)
                        {

                            foreach (XmlNode v in vals)
                            {
                                var real = "";
                                if (v.Attributes["real"] != null)
                                {
                                    real = v.Attributes["real"].Value;
                                }
                                var val = new Value(v.InnerText.Trim(), real);
                                templ.Values.Add(val);
                            }
                            objt.Templates.Add(templ);
                        }
                        else if (dynval != null)
                        {
                            //undocumented
                            //pipeline should return objects with Display (+Real if required)
                            // | select @{N='Display';E={$_.DisplayName}},@{N="Real";E={$_.Name}}
                            templ.DynValScript = dynval.InnerText.Trim();
                            objt.Templates.Add(templ);
                        }

                    }
                    global.Add(objt);
                }

            }
        }
    }
    public class ObjectTemplate
    {
        public string Name { get; set; }
        public string FilterSelect { get; set; }
        public string Filter { get; set; }
        public List<Template> Templates { get; set; }
        public ObjectTemplate()
        {
            Templates = new List<Template>();
        }
        public ObjectTemplate(string Name)
        {
            Templates = new List<Template>();
            this.Name = Name;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
    public class Template
    {
        public string Name { get; set; }
        public string Script { get; set; }
        public string PreScript { get; set; }
        public string PostScript { get; set; }
        public string DynValScript { get; set; }
        public List<Value> Values { get; set; }

        public Template()
        {
            Values = new List<Value>();
            this.PostScript = "";
            this.PreScript = "";
            this.DynValScript = "";
        }

        public bool hasDynamicValues()
        {
            return this.DynValScript != "";
        }
        public override string ToString()
        {
            return this.Name;
        }

    }
    public class Value
    {
        public string Display { get; set; }
        public string Real { get; set; }
        public Value(string d,string r)
        {
            this.Display = d;
            this.Real = r;
        }
        public override string ToString()
        {
            return this.Display;
        }
    }
}
