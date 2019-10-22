using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using eTaxi.Model;

namespace eTaxi.Model
{
    public class Vozac : Osoba
    {
        public String id { get; set; }
        public String DatumRodenja { get; set; }
        public string Spol { get; set; }
        public String Region { get; set; }
        public String JMBG { get; set; }
        public String BrojTelefona { get; set; }
        public String AdresaStanovanja { get; set; }
        public String BrojKartice { get; set; }
        public String RadnoIskustvo { get; set; }
        public String StatusVoznje { get; set; }
        public String StatusZaposlenja { get; set; }
        public String IdAuta { get; set; }
        public double Ocjena { get; set; }
        public int BrojVoznji { get; set; }

        public Vozac(string ime, string prezime, string eMail, string lozinka, string slika, 
                    string datum, string spol, String region, string jMBG, string brojTelefona, string adresa, 
                    string brojKartice, string radnoIskustvo, String statusVoznje, 
                    String statusZaposlenja, String idAuta, double ocjena, int brojVoznji)
            : base(ime, prezime, eMail, lozinka, slika)
        {
            DatumRodenja = datum;
            Spol = spol;
            Region = region;
            JMBG = jMBG;
            BrojTelefona = brojTelefona;
            AdresaStanovanja = adresa;
            BrojKartice = brojKartice;
            RadnoIskustvo = radnoIskustvo;
            StatusVoznje = statusVoznje;
            StatusZaposlenja = statusZaposlenja;
            IdAuta = idAuta;
            Ocjena = ocjena;
            BrojVoznji = brojVoznji;
        }
    }
}
