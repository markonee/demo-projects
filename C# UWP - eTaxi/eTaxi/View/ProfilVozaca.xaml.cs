using eTaxi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace eTaxi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilVozaca : Page
    {
        Auto auto;
        Vozac vozac;
        
        public ProfilVozaca()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Tuple <Vozac, Auto> par = e.Parameter as Tuple<Vozac, Auto>;
            vozac = par.Item1;
            auto = par.Item2;
            ShowData();
        }

        private void ShowData()
        {
            labelaIme.Text = vozac.ime;
            labelaPrezime.Text = vozac.prezime;
            labelaRegion.Text = vozac.Region;
            labelaBrojVoznji.Text = "Broj vožnji: " + vozac.BrojVoznji.ToString();
            labelaOcjena.Text = "Ocjena: " + vozac.Ocjena.ToString();
            labelaBrojSjedista.Text = "Broj sjedišta: " + auto.BrojSjedista.ToString();
            labelaModelAuta.Text = "Model auta: " + auto.Model;
            labelaLinkAuta.Text = "Link auta: " + auto.Link;
            labelaStanjeVoznje.Text = vozac.StatusVoznje;

        }

        async void linkAuta_Click(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = linkAuta.Content.ToString();
            Uri uri = new Uri(uriToLaunch);
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
