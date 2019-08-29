using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MuzickiPlejer
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void otvoriPesme(object sender, RoutedEventArgs e)
        {
            Pesme pesme = new Pesme();
            pesme.Show();
        }

        private void otvoriIzv(object sender, RoutedEventArgs e)
        {
            Izvodjaci izvodjaci = new Izvodjaci();
            izvodjaci.Show();
        }

        private void otvoriAlb(object sender, RoutedEventArgs e)
        {
            Albumi albumi = new Albumi();
            albumi.Show();
        }

        private void otvoriIzd(object sender, RoutedEventArgs e)
        {
            IzdavackeKuce izdKuce = new IzdavackeKuce();
            izdKuce.Show();
        }

        private void otvoriZanr(object sender, RoutedEventArgs e)
        {
            Zanrovi zanrovi = new Zanrovi();
            zanrovi.Show();
        }
    }
}
