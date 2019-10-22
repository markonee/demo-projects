using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMK
{
    public class Pregled : IToString
    {
        Pacijent pacijent;
        Ordinacija ordinacija;
        private DateTime vrijemePregleda;
        bool placeno;

        public Pregled()
        {
            pacijent = null;
            ordinacija = null;
            vrijemePregleda = default(DateTime);
            placeno = false;
        }

        public Pregled(Pacijent pacijent, Ordinacija ordinacija, DateTime vrijemePregleda)
        {
            this.pacijent = pacijent;
            this.ordinacija = ordinacija;
            this.vrijemePregleda = vrijemePregleda;
            placeno = false;
        }

        public override string ToString()
        {
            string pacijentovIspis = pacijent.ToString();
            string povratni = "Ordinacija: " + ordinacija.Odjel;
            if (vrijemePregleda == default(DateTime)) povratni += "\r\nPacijent još uvijek čeka na pregled.";
            else povratni += ", pregledan: " + vrijemePregleda;
            return povratni;
        }

        public Ordinacija Ordinacija { get => ordinacija; set => ordinacija = value; }
        public DateTime VrijemePregleda { get => vrijemePregleda; set => vrijemePregleda = value; }
        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }
        public bool Placeno { get => placeno; set => placeno = value; }
    }
}
