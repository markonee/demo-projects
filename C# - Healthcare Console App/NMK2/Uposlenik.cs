using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace NMK
{
    abstract public class Uposlenik : Osoba
    {
        private string odjel;
        private string korisnickoIme;
        private string lozinka;

        public Uposlenik() : base() { }
        public Uposlenik(string ime, string prezime, string odjel, double plata, string korisnickoIme, string lozinka)
            : base(ime, prezime)
        {
            this.odjel = odjel;
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
        }

        virtual public string Odjel { get => odjel; set => odjel = value; }
        virtual public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        virtual public string Lozinka { get => lozinka; set => lozinka = value; }
    }

}
