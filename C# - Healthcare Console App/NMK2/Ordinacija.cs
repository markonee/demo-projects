using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMK2;

namespace NMK
{
    abstract public class Ordinacija
    {
        private List<Pacijent> listaCekanja;
        private Aparat aparat;
        private string odjel;
        bool doktorPrisutan;
        int broj;

        public Ordinacija()
        {
            broj = 0;
            listaCekanja = new List<Pacijent>();
            odjel = null;
        }

        public Ordinacija(List<Pacijent> listaCekanja, Aparat aparat, string odjel, bool doktorPrisutan, int broj)
        {
            this.listaCekanja = listaCekanja;
            this.aparat = aparat;
            this.odjel = odjel;
            this.doktorPrisutan = doktorPrisutan;
            this.broj = broj;
        }

        public string Odjel { get => odjel; set => odjel = value; }
        public bool DoktorPrisutan { get => doktorPrisutan; set => doktorPrisutan = value; }
        public List<Pacijent> ListaCekanja { get => listaCekanja; set => listaCekanja = value; }
        public int Broj { get => broj; set => broj = value; }
        public bool AparatFunkcionalan { get => aparat.Funkcionalan; set => aparat.Funkcionalan = value; }
        public Aparat Aparat { get => aparat; set => aparat = value; }

        public int RedCekanja()
        {
            return listaCekanja.Count;
        }

        public void DodajURedCekanja(Pacijent p)
        {
            listaCekanja.Add(p);
        }

        public void SkiniIzRedaCekanja()
        {
            listaCekanja.RemoveAt(0);
        }
    }

}
