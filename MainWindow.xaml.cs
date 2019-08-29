using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MuzickiPlejer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer player = new MediaPlayer();
        string fileName;
        bool isOnRepeat = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void pauziraj(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void pusti(object sender, RoutedEventArgs e)
        {
            player.Play();
        }

        private void stopiraj(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }

        private void otvori(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Multiselect = true,
                DefaultExt = ".mp3"
            }; bool? dialogOk = fileDialog.ShowDialog();

            if (dialogOk == true)
            {
                fileName = fileDialog.FileName;
                displej.Text = fileDialog.SafeFileName;
                heder.Content = fileDialog.FileName;
                player.Open(new Uri(fileName));
            }

            player.Play();
        }

        private void izadji(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void postavke(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            player.Volume = (double)volumeSlider.Value;
        }
    }
}
