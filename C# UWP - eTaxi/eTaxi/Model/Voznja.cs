using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTaxi.Model
{
    public class Voznja
    {
        String TackaA { get; set; }
        String TackaB { get; set; }
        Vozac Vozac { get; set; }
        Klijent Klijent { get; set; }
        double Cijena { get; set; }
        double Kilometraza { get; set; }
        int BrojPutnika { get; set; }
        StatusVoznje StatusVoznje { get; set; }
        int Id { get; set; }
       
    }
}
