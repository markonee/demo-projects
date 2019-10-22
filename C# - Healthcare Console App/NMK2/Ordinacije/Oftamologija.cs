using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMK2;

namespace NMK.Ordinacije
{
    public class Oftamologija : Ordinacija
    {
        public Oftamologija() : base()
        {
            Aparat = new Aparat(true);
            ListaCekanja = new List<Pacijent>();
            Odjel = "Oftamologija";
        }
        public Oftamologija(List<Pacijent> listaCekanja, Aparat aparat, string odjel, bool doktorPrisutan, int broj) :
            base(listaCekanja, aparat, odjel, doktorPrisutan, broj)
        { }
    }
}
