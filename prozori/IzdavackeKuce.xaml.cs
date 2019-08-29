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
    /// Interaction logic for IzdavackeKuce.xaml
    /// </summary>
    public partial class IzdavackeKuce : Window
    {
        public IzdavackeKuce()
        {
            InitializeComponent();
            prikaziIzdKuce();
        }

        private void prikaziIzdKuce()
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM T_IzdavackaKuca";
                command.Connection = connection;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("T_IzdavackaKuca");
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
                txtIzdavackaKucaId.Text = dr["IzdavackaKucaId"].ToString();
                txtNaziv.Text = dr["Naziv"].ToString();
                txtGrad.Text = dr["Grad"].ToString();
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
                command.CommandText = "INSERT INTO T_IzdavackaKuca (Naziv, Grad) VALUES (@Naziv, @Grad)";
                command.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
                command.Parameters.AddWithValue("@Grad", txtGrad.Text);
                command.Connection = connection;
                int provera = command.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Podaci o Izdavackoj kuci su uspešno upisani");
                    prikaziIzdKuce();
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
            command.CommandText = "UPDATE T_IzdavackaKuca SET Naziv = @Naziv, Grad = @Grad WHERE IzdavackaKucaId = @IzdavackaKucaId";
            command.Parameters.AddWithValue("@IzdavackaKucaId", txtIzdavackaKucaId.Text);
            command.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
            command.Parameters.AddWithValue("@Grad", txtGrad.Text);
            command.Connection = connection;

            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o izdavackoj kuci su uspešno izmenjeni");
                prikaziIzdKuce();
            }

            ponistiUnosTxt();
        }

        private void BtnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM T_IzdavackaKuca WHERE IzdavackaKucaId = @IzdavackaKucaId";
            command.Parameters.AddWithValue("@IzdavackaKucaId", txtIzdavackaKucaId.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o izdavackoj kuci su uspešno obrisani");
                prikaziIzdKuce();
            }
            ponistiUnosTxt();
        }

        private void ponistiUnosTxt()
        {
            txtIzdavackaKucaId.Text = "";
            txtNaziv.Text = "";
            txtGrad.Text = "";
        }
    }
}
