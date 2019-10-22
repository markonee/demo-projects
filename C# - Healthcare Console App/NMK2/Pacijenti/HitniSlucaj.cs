using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMK.Pacijenti
{
    public class HitniSlucaj : Pacijent
    {
        private string prvaPomoc;
        private string razlogPomoci;

        private DateTime vrijemeSmrti;
        private string uzrokSmrti;
        private bool obdukcija;
        private DateTime vrijemeObdukcije;

        public HitniSlucaj(string ime, string prezime, DateTime datumRodjenja, string jMBG, Spol spol, string adresa,
            bool uBraku, DateTime datumPrijema, bool ziv, string prvaPomoc, string razlogPomoci)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            Jmbg = jMBG;
            SpolPacijenta = spol;
            Adresa = adresa;
            UBraku = uBraku;
            DatumPrijema = datumPrijema;
            Ziv = ziv;
            ZakazaniPregledi = new Dictionary<Pregled, int>();
            this.prvaPomoc = prvaPomoc;
            this.razlogPomoci = razlogPomoci;
        }

        public HitniSlucaj() : base()
        {
            ZakazaniPregledi = new Dictionary<Pregled, int>();
        }

        public HitniSlucaj(HitniSlucaj n)
        {
            Ime = n.Ime;
            Prezime = n.Prezime;
            DatumRodjenja = n.DatumRodjenja;
            Jmbg = n.Jmbg;
            SpolPacijenta = n.SpolPacijenta;
            Adresa = n.Adresa;
            UBraku = n.UBraku;
            DatumPrijema = n.DatumPrijema;
            Ziv = n.Ziv;
            ZakazaniPregledi = new Dictionary<Pregled, int>(n.ZakazaniPregledi);
        }

        public string PrvaPomoc { get => prvaPomoc; set => prvaPomoc = value; }
        public string RazlogPomoci { get => razlogPomoci; set => razlogPomoci = value; }
        public DateTime VrijemeSmrti { get => vrijemeSmrti; set
            {
                if (value > DateTime.Now) throw new Exception("Odaberite korektan datum i vrijeme!");
                vrijemeSmrti = value;
            }
        }
        public string UzrokSmrti { get => uzrokSmrti; set => uzrokSmrti = value; }
        public bool Obdukcija { get => obdukcija; set => obdukcija = value; }
        public DateTime VrijemeObdukcije { get => vrijemeObdukcije; set
            {
                if (value > DateTime.Now) throw new Exception("Odaberite korektan datum i vrijeme!");
                vrijemeObdukcije = value;
            }
        }
    }
}
