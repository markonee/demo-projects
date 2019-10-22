using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMK.Uposlenici;
using NMK.Pacijenti;

namespace NMK
{
    public partial class Terapija
    {
        private string propisano;
        private Doktor garancijaDoktora;
        private bool dugorocnaTerapija;
        private DateTime datumPropisivanja;
        private DateTime datumKontrole;

        public Terapija()
        {
            propisano = null;
            dugorocnaTerapija = false;
            garancijaDoktora = null;
            datumPropisivanja = datumKontrole = default(DateTime);
        }

        public Terapija(string propisano, Doktor garancijaDoktora, bool dugorocnaTerapija, DateTime datumPropisivanja, DateTime datumKontrole)
        {
            this.propisano = propisano;
            this.garancijaDoktora = garancijaDoktora;
            this.dugorocnaTerapija = dugorocnaTerapija;
            this.datumPropisivanja = datumPropisivanja;
            this.datumKontrole = datumKontrole;
        }
    }
}
