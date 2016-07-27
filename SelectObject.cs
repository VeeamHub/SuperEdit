using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperEdit
{
    public class SelectObject
    {
        public String name { get; set; }
        public bool selected { get; set; }
        public SelectObject()
        {
            this.name = "";
            this.selected = false;
        }
        public SelectObject(String name)
        {
            this.name = name;
            this.selected = false;
        }
        public SelectObject(String name, bool selected)
        {
            this.name = name;
            this.selected = selected;
        }
    }
    
}
