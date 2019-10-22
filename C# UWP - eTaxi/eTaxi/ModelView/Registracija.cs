using eTaxi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTaxi.ModelView
{
    public class Registracija
    {

    // Osnovni podaci (klijent)
        public String ime { get; set; }
        public String prezime { get; set; }
        public String eMail { get; set; }
        public String lozinka { get; set; }
        public String potvrdaLozinke { get; set; }
        public DateTime datumRodenja { get; set; }
        public Spol spol { get; set; }

    // Dodatni podaci
        public String JMBG { get; set; }
        public String brojTelefona { get; set; }
        public String adresaStanovanja { get; set; }
        public String brojKartice { get; set; }
        public Region region { get; set; }

    // Podaci o zaposlenju
        public String modelAutomobila { get; set; }
        public int brojSjedista { get; set; }
        public String radnoIskustvo { get; set; }

        //private bool validacijaRegistracije()
        //public void DodajRegistriranog (Osoba osoba)

    }
}
