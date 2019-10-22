using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTaxi.Model
{
    public class Glupa
    {
        public Glupa(string imeGLupa, string p)
        {
            this.imeGLupa = imeGLupa;
            this.prezimeGlupa = p;
        }



        public string imeGLupa { get; set; }
        public string id { get; set; }
        public string prezimeGlupa { get; set; }
    }
}
