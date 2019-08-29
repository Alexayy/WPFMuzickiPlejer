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
using System.Windows.Shapes;

namespace MuzickiPlejer
{
    /// <summary>
    /// Interaction logic for Izvodjaci.xaml
    /// </summary>
    public partial class Izvodjaci : Window
    {
        public Izvodjaci()
        {
            InitializeComponent();
            prikaziIzv();
        }

        private void prikaziIzv()
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM T_Izvodjac";
                command.Connection = connection;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("T_Izvodjac");
                dataAdapter.Fill(dataTable);

                DataGridIzvodjac.ItemsSource = dataTable.DefaultView;
            }
            
            catch (Exception ex)
            {
               
            }
        }

        private void DataGridIzvodjac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                DataGrid dg = sender as DataGrid;
                DataRowView dr = dg.SelectedItem as DataRowView;
                if (dr != null)
                {
                    txtIzvodjacId.Text = dr["IzvodjacId"].ToString();
                    txtImePrezime.Text = dr["ImePrezime"].ToString();
                    txtGodinaNastanka.Text = dr["Nastali"].ToString();
                    txtBrojClanova.Text = dr["BrojClanova"].ToString();
                }
            }
            
            catch (Exception ex)
            {

            }
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO T_Izvodjac (ImePrezime, GodinaNastanka, BrojClanova) VALUES (@ImePrezime, @GodinaNastanka, @BrojClanova)";
                command.Parameters.AddWithValue("@ImePrezime", txtImePrezime.Text);
                command.Parameters.AddWithValue("@GodinaNastanka", txtGodinaNastanka.Text);
                command.Parameters.AddWithValue("@BrojClanova", txtBrojClanova.Text);
                command.Connection = connection;
                int provera = command.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Podaci o izvodjacu su uspešno upisani");
                    prikaziIzv();
                }
                ponistiUnosTxt();
            }

            catch (Exception ex)
            {
                
            }
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE T_Izvodjac SET ImePrezime = @ImePrezime, GodinaNastanka = @GodinaNastanka, BrojClanova = @BrojClanova WHERE IzvodjacId = @IzvodjacId";
            command.Parameters.AddWithValue("@IzvodjacId", txtIzvodjacId.Text);
            command.Parameters.AddWithValue("@ImePrezime", txtImePrezime.Text);
            command.Parameters.AddWithValue("@GodinaNastanka", txtGodinaNastanka.Text);
            command.Parameters.AddWithValue("@BrojClanova", txtBrojClanova.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o izvodjacu su uspešno izmenjeni");
                prikaziIzv();
            }
            ponistiUnosTxt();
        }

        private void BtnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM T_Izvodjac WHERE IzvodjacId = @IzvodjacId";
            command.Parameters.AddWithValue("@IzvodjacId", txtIzvodjacId.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o ligi su uspešno obrisani");
                prikaziIzv();
            }
            ponistiUnosTxt();
        }

        private void ponistiUnosTxt()
        {
            txtIzvodjacId.Text = "";
            txtImePrezime.Text = "";
            txtGodinaNastanka.Text = "";
            txtBrojClanova.Text = "";
        }
    }
}
