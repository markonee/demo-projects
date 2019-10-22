using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMK.Pacijenti
{
    public class NormalniSlucaj : Pacijent
    {
        public NormalniSlucaj(string ime, string prezime, DateTime datumRodjenja, string jMBG, Spol spol,
            string adresa, bool uBraku, DateTime datumPrijema, bool ziv)
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
        }

        public NormalniSlucaj(NormalniSlucaj n)
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
            ZakazaniPregledi = new Dictionary<Pregled, int>();
        }

        public NormalniSlucaj() : base()
        {
            ZakazaniPregledi = new Dictionary<Pregled, int>();
        }

    }
}
