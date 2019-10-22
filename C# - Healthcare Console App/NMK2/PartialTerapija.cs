using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMK.Uposlenici;

namespace NMK
{
    partial class Terapija
    {
        public Doktor GarancijaDoktora { get => garancijaDoktora; set => garancijaDoktora = value; }
        public string Propisano { get => propisano; set => propisano = value; }
        public bool DugorocnaTerapija { get => dugorocnaTerapija; set => dugorocnaTerapija = value; }
        public DateTime DatumPropisivanja { get => datumPropisivanja; set
            {
                if (value > DateTime.Now) throw new Exception("Odaberite korektan datum - manji od današnjeg!");
                datumPropisivanja = value;
            }
        }
        public DateTime DatumKontrole { get => datumKontrole; set
            {
                if (value < DateTime.Now) throw new Exception("Odaberite korektan datum - današnji ili neki veći!");
                datumKontrole = value;
            }
        }
    }
}
