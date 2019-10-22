using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace eTaxi.Model
{
    public class Klijent : Osoba
    {
        public String spol { get; set; }
        public String datum_rodenja { get; set; }
        public Boolean stalni { get; set; }
        public int brojVoznji { get; set; }
        public String id { get; set; }

        public Klijent(string ime, string prezime, string email, string lozinka, string slika, string spol, String datumRodjenja, bool stalni, int broj)
            : base(ime, prezime, email, lozinka, slika)
        {
            this.spol = spol;
            this.datum_rodenja = datumRodjenja;
            this.stalni = stalni;
            this.brojVoznji = broj;
        }
    }
}
