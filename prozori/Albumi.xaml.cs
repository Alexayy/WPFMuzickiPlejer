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
    /// Interaction logic for Albumi.xaml
    /// </summary>
    public partial class Albumi : Window
    {
        public Albumi()
        {
            InitializeComponent();
            prikaziAlbume();
        }

        private void prikaziAlbume()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM T_Album";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("T_Album");
            dataAdapter.Fill(dataTable);

            DataGridAlbum.ItemsSource = dataTable.DefaultView;
        }

        private void DataGridAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtAlbumId.Text = dr["AlbumId"].ToString();
                txtNaziv.Text = dr["Naziv"].ToString();
                txtGodina.Text = dr["Godina"].ToString();
                txtIzdKucaId.Text = dr["IzdavackaKucaId"].ToString();
                txtIzdKucaId.Text = dr["ZanrId"].ToString();
            }
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO T_Album (Naziv, Godina, IzdavackaKucaId, ZanrId) VALUES (@Naziv, @Godina, @IzdavackaKucaId, @ZanrId)";
            command.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
            command.Parameters.AddWithValue("@Godina", txtGodina.Text);
            command.Parameters.AddWithValue("@IzdavackaKucaId", txtIzdKucaId.Text);
            command.Parameters.AddWithValue("@ZanrId", txtZanrId.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o Albumu su uspešno upisani");
                prikaziAlbume();
            }
            ponistiUnosTxt();
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE T_Album SET Naziv = @Naziv, Godina = @Godina, IzdavackaKucaId = @IzdavackaKucaId, ZanrId = @ZanrId WHERE AlbumId = @AlbumId";
            command.Parameters.AddWithValue("@AlbumId", txtAlbumId.Text);
            command.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
            command.Parameters.AddWithValue("@Godina", txtGodina.Text);
            command.Parameters.AddWithValue("@IzdavackaKucaId", txtIzdKucaId.Text);
            command.Parameters.AddWithValue("@ZanrId", txtZanrId.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o Albumu su uspešno izmenjeni");
                prikaziAlbume();
            }
            ponistiUnosTxt();
        }

        private void BtnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM T_Album WHERE AlbumId = @AlbumId";
            command.Parameters.AddWithValue("@AlbumId", txtAlbumId.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o Albumu su uspešno obrisani");
                prikaziAlbume();
            }
            ponistiUnosTxt();
        }

        private void CbxIzdKucaId_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand commandCbX = new SqlCommand();
            commandCbX.CommandText = "SELECT * FROM T_IzdavackaKuca ORDER BY IzdavackaKucaId";
            commandCbX.Connection = connection;
            SqlDataAdapter dataAdapterCbx = new SqlDataAdapter(commandCbX);
            DataTable dataTableCbx = new DataTable("T_Album");
            dataAdapterCbx.Fill(dataTableCbx);

            for (int i = 0; i < dataTableCbx.Rows.Count; i++)
            {
                txtIzdKucaId.Items.Add(dataTableCbx.Rows[i]["IzdavackaKucaId"]);
            }
        }

        private void CbxZanrId_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connMuzika"].ConnectionString;
            connection.Open();
            SqlCommand commandCbX = new SqlCommand();
            commandCbX.CommandText = "SELECT * FROM T_Zanr ORDER BY ZanrId";
            commandCbX.Connection = connection;
            SqlDataAdapter dataAdapterCbx = new SqlDataAdapter(commandCbX);
            DataTable dataTableCbx = new DataTable("T_Album");
            dataAdapterCbx.Fill(dataTableCbx);

            for (int i = 0; i < dataTableCbx.Rows.Count; i++)
            {
                txtZanrId.Items.Add(dataTableCbx.Rows[i]["ZanrId"]);
            }
        }

        private void ponistiUnosTxt()
        {
            txtNaziv.Text = "";
            txtGodina.Text = "";
            txtIzdKucaId.Text = "";
            txtZanrId.Text = "";
        }
    }
}
