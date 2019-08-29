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
    /// Interaction logic for Zanrovi.xaml
    /// </summary>
    public partial class Zanrovi : Window
    {
        public Zanrovi()
        {
            InitializeComponent();
            prikaziZanr();
        }

        private void prikaziZanr()
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM T_Zanr";
                command.Connection = connection;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("T_Zanr");
                dataAdapter.Fill(dataTable);

                DataGridPesme.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception e)
            {

            }

        }

        private void DataGridIzdKuca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtZanrId.Text = dr["ZanrId"].ToString();
                txtNaziv.Text = dr["Naziv"].ToString();
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
                command.CommandText = "INSERT INTO T_Zanr (Naziv) VALUES (@Naziv)";
                command.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
                command.Connection = connection;
                int provera = command.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Podaci o zanru su uspešno upisani");
                    prikaziZanr();
                }

                ponistiUnosTxt();
            }
            catch (Exception eh)
            {

            }

        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE T_Zanr SET Naziv = @Naziv WHERE ZanrId = @ZanrId";
            command.Parameters.AddWithValue("@ZanrId", txtZanrId.Text);
            command.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
            command.Connection = connection;

            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o izdavackoj kuci su uspešno izmenjeni");
                prikaziZanr();
            }

            ponistiUnosTxt();
        }

        private void BtnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM T_Zanr WHERE ZanrId = @ZanrId";
            command.Parameters.AddWithValue("@ZanrId", txtZanrId.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o izdavackoj kuci su uspešno obrisani");
                prikaziZanr();
            }
            ponistiUnosTxt();
        }

        private void ponistiUnosTxt()
        {
            txtZanrId.Text = "";
            txtNaziv.Text = "";
        }
    }
}
