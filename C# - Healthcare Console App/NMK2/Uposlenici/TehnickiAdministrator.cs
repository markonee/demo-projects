using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace NMK.Uposlenici
{
    public class TehnickiAdministrator : Uposlenik
    {
        private static double plata = 1500;
        public TehnickiAdministrator() : base() { }
        public TehnickiAdministrator(string ime, string prezime, string odjel, double plata, string korisnickoIme, string lozinka)
            : base(ime, prezime, odjel, plata, korisnickoIme, lozinka) { }


        public static double Plata { get => plata; set => plata = value; }
        public override string Odjel { get => base.Odjel; set => base.Odjel = value; }
        public override string KorisnickoIme { get => base.KorisnickoIme; set => base.KorisnickoIme = value; }
        public override string Lozinka { get => base.Lozinka; set => base.Lozinka = value; }
    }
}
