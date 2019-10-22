using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace eTaxi.Model
{
    abstract public class Osoba
    {
        public String ime { get; set; }
        public String prezime { get; set; }
        public String email { get; set; }
        public String lozinka { get; set; }
        public String slika { get; set; }
        public bool administrator { get; set; }

        protected Osoba(string ime, string prezime, string email, string lozinka, string slika)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.email = email;
            this.lozinka = lozinka;
            this.slika = slika;
            this.administrator = false;
        }

        public void PostaviOsobuZaAdministratora (Klijent k)
        {
            k.administrator = true;
        }
    }
}
