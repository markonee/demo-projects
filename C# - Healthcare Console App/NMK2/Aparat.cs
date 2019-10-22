using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMK2
{
    public class Aparat
    {
        bool funkcionalan;

        public Aparat()
        {
            funkcionalan = true;
        }

        public Aparat(bool funkcionalan)
        {
            this.funkcionalan = funkcionalan;
        }

        public bool Funkcionalan
        {
            get
            {
                return funkcionalan;
            }
            set
            {
                funkcionalan = value;
            }
        }
    }
}
