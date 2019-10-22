using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMK
{
    sealed public class Karton : IToString
    {
        private int brojKartona;
        private Pacijent pacijent;
        private int brojacPosjeta;
        private string sadasnjaBolest;
        private string sadasnjeAlergije;
        private string ranijeBolesti;
        private string ranijeAlergije;
        private string zdravstvenoStanjePorodice;

        private List<string> misljenjeDoktora;
        private List<string> rezultatPregleda;
        private List<Terapija> evidencijaTerapija;

        private double dugovi; 

        public Karton(Pacijent pacijent, int brojKartona, int brojacPosjeta, string sadasnjaBolest, string sadasnjeAlergije, string ranijeBolesti, string ranijeAlergije, string zdravstvenoStanjePorodice, double dugovi)
        {
            this.brojKartona = brojKartona;
            this.pacijent = pacijent;
            BrojacPosjeta = brojacPosjeta;
            this.sadasnjaBolest = sadasnjaBolest;
            this.sadasnjeAlergije = sadasnjeAlergije;
            this.ranijeBolesti = ranijeBolesti;
            this.ranijeAlergije = ranijeAlergije;
            this.zdravstvenoStanjePorodice = zdravstvenoStanjePorodice;
            BrojacPosjeta = brojacPosjeta;

            misljenjeDoktora = rezultatPregleda = new List<string>();
            evidencijaTerapija = new List<Terapija>();
            this.dugovi = dugovi;
        }

        public Karton()
        {
            misljenjeDoktora = rezultatPregleda = new List<string>();
            evidencijaTerapija = new List<Terapija>();
            dugovi = 0;
        }

        public int BrojacPosjeta { get => brojacPosjeta; set => brojacPosjeta = value; }
        public int BrojKartona { get => brojKartona; set => brojKartona = value; }
        public string JMBGPacijenta { get => pacijent.Jmbg; }
        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }
        public string SadasnjaBolest { get => sadasnjaBolest; set => sadasnjaBolest = value; }
        public string SadasnjeAlergije { get => sadasnjeAlergije; set => sadasnjeAlergije = value; }
        public string RanijeBolesti { get => ranijeBolesti; set => ranijeBolesti = value; }
        public string RanijeAlergije { get => ranijeAlergije; set => ranijeAlergije = value; }
        public string ZdravstvenoStanjePorodice { get => zdravstvenoStanjePorodice; set => zdravstvenoStanjePorodice = value; }
        public List<string> MisljenjeDoktora { get => misljenjeDoktora; set => misljenjeDoktora = value; }
        public List<string> RezultatPregleda { get => rezultatPregleda; set => rezultatPregleda = value; }
        public List<Terapija> EvidencijaTerapija { get => evidencijaTerapija; set => evidencijaTerapija = value; }
        public double Dugovi { get => dugovi; set => dugovi = value; }

        public void DodajMisljenje(string s)
        {
            misljenjeDoktora.Add(s);
        }

        public void DodajRezultat(string s)
        {
            rezultatPregleda.Add(s);
        }

        public void DodajTerapiju(Terapija t)
        {
            evidencijaTerapija.Add(t);
        }
    }
}
