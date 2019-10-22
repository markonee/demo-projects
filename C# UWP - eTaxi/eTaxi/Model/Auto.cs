using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using eTaxi.Model;

namespace eTaxi.Model
{
    public class Auto
    {
    
        public int BrojSjedista { get; set; }
        public String Model { get; set; }
        public String Link { get; set; }
        public String Id { get; set; }

        public Auto()
        {

        }

        public Auto(int brojSjedista, string model, string link)
        {
            BrojSjedista = brojSjedista;
            Model = model;
            Link = link;
        }

    }
}
