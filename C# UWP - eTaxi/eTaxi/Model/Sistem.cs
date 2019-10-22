using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;

namespace eTaxi.Model
{
    public class Sistem
    {
  
        List<Klijent> Klijenti { get; set; }
        List<Vozac> Vozaci { get; set; }
        List<Voznja> Voznje { get; set; }
        Administrator Administrator { get; set; }

        

        public Sistem(List<Klijent> klijenti, List<Vozac> vozaci, List<Voznja> voznje, Administrator administrator)
        {
            Klijenti = klijenti;
            Vozaci = vozaci;
            Voznje = voznje;
            Administrator = administrator;
        }

        public Sistem()
        {
            Klijenti = new List<Klijent>();
            Vozaci = new List<Vozac>();
            Voznje = new List<Voznja>();
            Administrator = new Administrator("Ime", "Prezime", "admin@etf.unsa.ba", "admin", null);
        }
    
    }
}
