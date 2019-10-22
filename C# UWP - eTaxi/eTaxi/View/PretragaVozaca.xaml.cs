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
    public sealed partial class PretragaVozača : Page
    {
        public PretragaVozača()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void MyCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if(MyCheckBox.IsChecked == true)
            {
                MyCheckBox1.IsChecked = MyCheckBox2.IsChecked = MyCheckBox3.IsChecked = true;
                MyCheckBox.Content = "Odznači sve";
            }

            else
            {
                MyCheckBox1.IsChecked = MyCheckBox2.IsChecked = MyCheckBox3.IsChecked = false;
                MyCheckBox.Content = "Označi sve";
            }
        }
    }
}
