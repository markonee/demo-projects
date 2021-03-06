﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMK2;

namespace NMK.Ordinacije
{
    public class Ortopedija : Ordinacija
    {
        public Ortopedija() : base()
        {
            Aparat = new Aparat(false);
            ListaCekanja = new List<Pacijent>();
            Odjel = "Ortopedija";
        }
        public Ortopedija(List<Pacijent> listaCekanja, Aparat aparat, string odjel, bool doktorPrisutan, int broj) :
            base(listaCekanja, aparat, odjel, doktorPrisutan, broj)
        { }
    }
}
