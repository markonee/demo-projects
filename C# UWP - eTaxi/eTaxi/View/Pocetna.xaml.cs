using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using eTaxi.Model;
using Microsoft.WindowsAzure.MobileServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace eTaxi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        IMobileServiceTable<Klijent> klijentTabela;
        IMobileServiceTable<Vozac> vozacTabela;
        IMobileServiceTable<Auto> autoTabela;

        public static MainPage Glavna;
        public static Sistem sistem = new Sistem();

        public MainPage()
        {
            this.InitializeComponent();
            Glavna = this;
            klijentTabela = App.MobileService.GetTable<Klijent>();
            vozacTabela = App.MobileService.GetTable<Vozac>();
            autoTabela = App.MobileService.GetTable<Auto>();

        }

        private void HardkodirajAdmina()
        {
            Klijent k = new Klijent("Anja", "Plakalović", "aplakalovi1@etf.unsa.ba", "anjaanja", null, Spol.Zensko.ToString(), "10.10.1997", false, 0);
            klijentTabela.InsertAsync(k);
        }

        private async Task UpdateAdmina(string email)
        {
            await klijentTabela.ToListAsync().ContinueWith(adminTask =>
            {
                List<Klijent> klijenti = adminTask.Result;

                foreach (Klijent k in klijenti)
                {
                    if (k.email.Equals(email))
                    {
                        k.administrator = true;
                        klijentTabela.UpdateAsync(k);
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
              
        } 

        private async Task AzurirajStatuse()
        {
            await klijentTabela.ToListAsync().ContinueWith(adminTask =>
            {
                List<Klijent> dobavljeniKlijenti = adminTask.Result;

                foreach(Klijent k in dobavljeniKlijenti)
                {
                    k.administrator = false;
                    klijentTabela.UpdateAsync(k);
                }
                
            }, TaskScheduler.FromCurrentSynchronizationContext());                              // nakon izvršenja idi na UI thread.
        }

        private void hyper_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Registracija));
        }

        private void dugmePrijaviSe_Click(object sender, RoutedEventArgs e)
        {
            bool validacijaUspjesna = false;

            string pgreske = "";

            if (!ValidirajEMail(textBoxEMail.Text, ref pgreske))
            {
                Greska.Text = pgreske;
            validacijaUspjesna = false;
            }
            else if (!ValidirajPassword(passwordBox.Password, ref pgreske))
            {
                Greska.Text = pgreske;
            validacijaUspjesna = false;
            }
            else
            {
                Greska.Text = "";
            validacijaUspjesna = true;
            }

            if(validacijaUspjesna)
            {
                string unesenEMail = textBoxEMail.Text;
                string unesenaLozinka = passwordBox.Password;

                dugmePrijaviSe.IsEnabled = false;
                hyper.IsEnabled = false;

                klijentTabela.Where(k => k.email == unesenEMail).ToListAsync().ContinueWith(klijentTask =>
                {
                    Klijent klijent = klijentTask.Result.FirstOrDefault(k => k.lozinka == unesenaLozinka);

                    if(klijent != null && klijent.administrator == false)
                    {
                        this.Frame.Navigate(typeof(ProfilKlijenta), klijent);
                        return;
                    }
                    else if(klijent != null && klijent.administrator == true)
                    {
                        this.Frame.Navigate(typeof(ProfilAdministratora), klijent);
                        return;
                    }

                    vozacTabela.Where(v => v.email == unesenEMail).ToListAsync().ContinueWith(vozacTask =>
                    {
                        Vozac vozac = vozacTask.Result.FirstOrDefault(v => v.lozinka == unesenaLozinka);

                        if(vozac != null)
                        {
                            autoTabela.LookupAsync(vozac.IdAuta).ContinueWith(autoTask =>
                            {
                                var auto = autoTask.Result;

                                Tuple<Vozac, Auto> par = new Tuple<Vozac, Auto>(vozac, auto);

                                this.Frame.Navigate(typeof(ProfilVozaca), par);
                                
                            }, TaskScheduler.FromCurrentSynchronizationContext());
                            return;
                        }
                        Greska.Text = "Uneseni e-mail ili lozinka nisu tačni!";
                    }, TaskScheduler.FromCurrentSynchronizationContext());                    
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
             
        }

        public bool ValidirajEMail(string tekst, ref string pgreske)
        {
            bool imaLudogA = false;
            pgreske = "";

            if (tekst.Length == 0)
            {
                pgreske = "Unesite e-mail!";
                return false;
            }

            foreach(char c in tekst)
            {
                if(c == '@')
                {
                    imaLudogA = true;
                }
            }

            if(tekst.Length < 8 || !imaLudogA)
            {
                pgreske = "Neispravan unos e-maila!";
                return false;
            }

            return true;
        }

        public bool ValidirajPassword(string tekst, ref string pgreske2)
        {
            pgreske2 = "";

            if(tekst.Length == 0)
            {
                pgreske2 = "Unesite password!";
                return false;
            }

            if(tekst.Length < 8)
            {
                pgreske2 = "Neispravan unos passworda!";
                return false;
            }

            return true;
        }

    }
}
