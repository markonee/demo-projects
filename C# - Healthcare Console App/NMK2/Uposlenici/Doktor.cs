using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace NMK.Uposlenici
{
    public class Doktor : Uposlenik
    {
        private Ordinacija ordinacija;
        int brojacPacijenata;
        double[] bonusi;                             // velicina je 12
        DateTime registracijaPrvogUDanu;             // registrovao prvog pacijenta u danu
        int kalendarskiMjesec;
        private static double plata = 2000;

        public Doktor() : base()
        {
            brojacPacijenata = 1;                   // zbog prvog poziva -> 1         
            kalendarskiMjesec = DateTime.Now.Month;
            registracijaPrvogUDanu = default(DateTime);
            bonusi = new double[12];
        }

        public Doktor(Ordinacija ordinacija)
        {
            bonusi = new double[12];
            this.ordinacija = ordinacija;
            Odjel = ordinacija.Odjel;
            brojacPacijenata = 1;                   // zbog prvog poziva -> 1          
            kalendarskiMjesec = DateTime.Now.Month;
            registracijaPrvogUDanu = default(DateTime);
        }

        public void ObavioPregled()
        {
            brojacPacijenata++;
            if (kalendarskiMjesec != DateTime.Now.Month) kalendarskiMjesec = DateTime.Now.Month;

            if (registracijaPrvogUDanu.Date != DateTime.Now.Date)
            {
                if (registracijaPrvogUDanu != default(DateTime))
                {
                    if (brojacPacijenata <= 20) bonusi[kalendarskiMjesec - 1] += 1 * (brojacPacijenata - 1); // marka za svakog pacijenta [1,20]
                    brojacPacijenata = 1;
                }
                registracijaPrvogUDanu = DateTime.Now;
            }
            registracijaPrvogUDanu = registracijaPrvogUDanu.AddDays(1); // simulacija dodana za provjeru 
                                                                        // testiranja racunanja bonusa na plate

        }

        public double Plata     // racunamo platu i bonuse
        {
            get
            {
                return plata + bonusi[kalendarskiMjesec - 1];
            }
        }

        override public string Odjel { get => ordinacija.Odjel; set => ordinacija.Odjel = value; }

        public Ordinacija Ordinacija { get => ordinacija; set => ordinacija = value; }
        public override string KorisnickoIme { get => base.KorisnickoIme; set => base.KorisnickoIme = value; }
        public override string Lozinka { get => base.Lozinka; set => base.Lozinka = value; }
    }
}
