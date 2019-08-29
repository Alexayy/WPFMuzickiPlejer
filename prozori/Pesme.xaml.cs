using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
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
    /// Interaction logic for Pesme.xaml
    /// </summary>
    public partial class Pesme : Window
    {
        public Pesme()
        {
            InitializeComponent();
            prikaziPesme();
        }

        private void prikaziPesme()
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM T_Pesme";
                command.Connection = connection;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("T_Pesme");
                dataAdapter.Fill(dataTable);

                DataGridPesme.ItemsSource = dataTable.DefaultView;
            } catch (Exception e)
            {

            } 
            
        }

        private void DataGridPesma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                pesmaId.Text = dr["PesmaId"].ToString();
                txtNaziv.Text = dr["Naziv"].ToString();
                txtDuzina.Text = dr["Duzina"].ToString();
                cbxIzvodjacId.Text = dr["IzvodjacId"].ToString();
                albumId.Text = dr["AlbumId"].ToString();
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
                command.CommandText = "INSERT INTO T_Pesme (Naziv, Duzina, IzvodjacId, AlbumId) VALUES (@Naziv, @Duzina, @IzvodjacId, @AlbumId)";
                command.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
                command.Parameters.AddWithValue("@Duzina", txtDuzina.Text);
                command.Parameters.AddWithValue("@IzvodjacId", cbxIzvodjacId.Text);
                command.Parameters.AddWithValue("@AlbumId", albumId.Text);
                command.Connection = connection;
                int provera = command.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Podaci o pesmi su uspešno upisani");
                    prikaziPesme();
                }

                ponistiUnosTxt();
            } catch (Exception eh)
            {

            }
            
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE T_Pesme SET Naziv = @Naziv, Duzina = @Duzina, IzvodjacId = @IzvodjacId, AlbumId = @AlbumId WHERE PesmaId = @PesmaId";
            command.Parameters.AddWithValue("@PesmaId", pesmaId.Text);
            command.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
            command.Parameters.AddWithValue("@Duzina", txtDuzina.Text);
            command.Parameters.AddWithValue("@IzvodjacId", cbxIzvodjacId.Text);
            command.Parameters.AddWithValue("@AlbumId", albumId.Text);
            command.Connection = connection;

            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o pesmi su uspešno izmenjeni");
                prikaziPesme();
            }

            ponistiUnosTxt();
        }

        private void BtnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM T_Pesme WHERE PesmaId = @PesmaId";
            command.Parameters.AddWithValue("@PesmaId", pesmaId.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o pesmi su uspešno obrisani");
                prikaziPesme();
            }
            ponistiUnosTxt();
        }

        private void CbxIzvodjacId_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand commandCbX = new SqlCommand();
            commandCbX.CommandText = "SELECT * FROM T_Izvodjac ORDER BY IzvodjacId";
            commandCbX.Connection = connection;
            SqlDataAdapter dataAdapterCbx = new SqlDataAdapter(commandCbX);
            DataTable dataTableCbx = new DataTable("T_Pesme");
            dataAdapterCbx.Fill(dataTableCbx);

            for (int i = 0; i < dataTableCbx.Rows.Count; i++)
            {
                cbxIzvodjacId.Items.Add(dataTableCbx.Rows[i]["IzvodjacId"]);
            }
        }

        private void CbxAlbumId_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand commandCbX = new SqlCommand();
            commandCbX.CommandText = "SELECT * FROM T_Album ORDER BY AlbumId";
            commandCbX.Connection = connection;
            SqlDataAdapter dataAdapterCbx = new SqlDataAdapter(commandCbX);
            DataTable dataTableCbx = new DataTable("T_Pesme");
            dataAdapterCbx.Fill(dataTableCbx);

            for (int i = 0; i < dataTableCbx.Rows.Count; i++)
            {
                albumId.Items.Add(dataTableCbx.Rows[i]["AlbumId"]);
            }
        }

        private void ponistiUnosTxt()
        {
            pesmaId.Text = "";
            txtNaziv.Text = "";
            txtDuzina.Text = "";
            cbxIzvodjacId.Text = "";
            albumId.Text = "";
        }
    }
}
