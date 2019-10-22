using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NMK
{

    abstract public class Pacijent : Osoba, IToString
    {
        public enum Spol { musko, zensko };

        private DateTime datumRodjenja;
        private string JMBG;
        private Spol spol;
        private string adresa;
        private bool uBraku;
        private DateTime datumPrijema;
        private bool ziv;
        private Dictionary<Pregled, int> zakazaniPregledi;      // pregled - redni broj
        private Image slikaPacijenta;                           // novi atribut dodan za Z2

        public bool Ziv { get => ziv; set => ziv = value; }
        public DateTime DatumPrijema { get => datumPrijema; set => datumPrijema = value; }
        public bool UBraku { get => uBraku; set => uBraku = value; }
        public string Adresa { get => adresa; set
            {
                if (value.Length == 0) throw new Exception("Prazno polje!");
                adresa = value;
            }
        }
        public Spol SpolPacijenta { get => spol; set => spol = value; }
        public string Jmbg
        {
            get => JMBG; set
            {
                if (value.Length == 0) throw new Exception("Prazno polje!");
                if (value.Length != 13) throw new Exception("JMBG mora imati tačno 13 cifara!");
                JMBG = value;
            }
        }
        public DateTime DatumRodjenja { get => datumRodjenja; set
            {
                DateTime dt = new DateTime();
                dt = value;
                if (value > DateTime.Now) throw new Exception("Datum veći od današnjeg!");
                datumRodjenja = value;
            }
        }
        public Dictionary<Pregled, int> ZakazaniPregledi { get => zakazaniPregledi; set => zakazaniPregledi = value; }
        public Image SlikaPacijenta { get => slikaPacijenta; set => slikaPacijenta = value; }

        public override string ToString()
        {
            string povratni = "";
            povratni = "\r\nIme: " + Ime + "\r\nPrezime: " + Prezime + "\r\nAdresa stanovanja: " + Adresa +
                "\r\nDatum rodjenja: " + DatumRodjenja + "\r\nJMBG: " + Jmbg + "\r\nDatum prijema: " + datumPrijema;
            return povratni;
        }

        public void ZakaziPregled(Pregled p, int brojPacijenataIspred)
        {
            zakazaniPregledi.Add(p, brojPacijenataIspred);
        }

        public void SortirajPreglede()
        {
            zakazaniPregledi = zakazaniPregledi.OrderByDescending(x => x.Key.Ordinacija.DoktorPrisutan).ThenBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public bool ImaLiBroja(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= '0' && s[i] <= '9') return true;
            }
            return false;
        }

        public bool ProvjeriJmbg(string jmbg, DateTime datRodjenja)
        {
            if (jmbg.Length == 0) return false;
            int danJmbg = 0, mjesecJmbg = 0, godinaJmbg = 0, dan = datRodjenja.Day, mjesec = datRodjenja.Month, godina = datRodjenja.Year;
            string s1 = string.Empty, s2 = string.Empty, s3 = string.Empty;

            for (int i = 0; i < 7; i++)
            {
                if (Char.IsDigit(jmbg[i]))
                {
                    if (i < 2) s1 += jmbg[i];
                    else if (i < 4) s2 += jmbg[i];
                    else s3 += jmbg[i];
                }
            }
            danJmbg = Int32.Parse(s1); mjesecJmbg = Int32.Parse(s2); godinaJmbg = Int32.Parse(s3);
            if (godinaJmbg % 100 > 0) godinaJmbg += 1000;
            else godinaJmbg += 2000;

            if (dan != danJmbg || mjesec != mjesecJmbg || godina != godinaJmbg) return false;
            return true;
        }

    }
}
