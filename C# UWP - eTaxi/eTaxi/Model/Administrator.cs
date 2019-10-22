using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace eTaxi.Model
{
    public class Administrator : Osoba
    {

        public Administrator(string ime, string prezime, string eMail, string lozinka, string slika) 
            : base(ime, prezime, eMail, lozinka, slika) { }


    }
}
