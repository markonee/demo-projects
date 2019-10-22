using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMK
{
    abstract public class Osoba
    {
        private string ime;
        private string prezime;

        public Osoba(string ime, string prezime)
        {
            this.ime = ime;
            this.prezime = prezime;
        }

        public Osoba()
        {
            ime = prezime = null;
        }

        public string Ime { get => ime; set
            {
                if (value.Length == 0) throw new Exception("Prazno polje!");
                if (ImalBroja(value)) throw new Exception("Ne smije sadržavati broj!");
                ime = value;
            }
        }
        public string Prezime { get => prezime; set
            {
                if (value.Length == 0) throw new Exception("Prazno polje!");
                if (ImalBroja(value)) throw new Exception("Ne smije sadržavati broj!");
                prezime = value;
            }
        }
        public bool ImalBroja(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= '0' && s[i] <= '9') return true;
            }
            return false;
        }
    }
}
