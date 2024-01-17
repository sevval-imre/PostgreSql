using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace postgresql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server = localHost; port=5432; Database=dbproje; username=postgres; password=12345;");
        private void btnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from kategoriler order by kategoriid asc";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into kategoriler (kategoriid, kategoriad) values (@p1, @p2)", baglanti);
            komut1.Parameters.AddWithValue("@p1", int.Parse(Txtid.Text));
            komut1.Parameters.AddWithValue("@p2", Txtad.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori ekleme işlemi başarılı bir şekilde gerçekleşti");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("delete from kategoriler where kategoriid=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(Txtid.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori silme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update kategoriler set kategoriad=@p1 where kategoriid=@p2", baglanti);
            komut3.Parameters.AddWithValue("@p1", Txtad.Text);
            komut3.Parameters.AddWithValue("@p2", int.Parse(Txtid.Text));
            komut3.ExecuteNonQuery();
            MessageBox.Show("Kategori güncelleme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string sorgu = "select* from kategoriler where kategoriad like '" + txtAra.Text + "%' ";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "kategoriler");
            dataGridView1.DataSource = ds.Tables["kategoriler"];
        }
    }
}
