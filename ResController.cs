using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperEdit
{
    public class ResController
    {
        public TemplateController templateController { get; set; }
        public VeeamPS  veeamPSController { get; set; }

        public ResController()
        {
            veeamPSController = new VeeamPS();
            veeamPSController.init();
            templateController = new TemplateController();
            templateController.init();
            


        }

    }
}
