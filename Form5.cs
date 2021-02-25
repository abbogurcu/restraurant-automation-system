using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restaurantOtomasyon
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection baglantiNesnesi = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Baran\Documents\restaurantOtomasyon\restaurant.mdf;Integrated Security=True;Connect Timeout=30");

        private void button1_Click(object sender, EventArgs e)
        {
            yonetici.yoneticiCheck = true;
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            SqlCommand komutNesnesi = new SqlCommand("select * from urunler", baglantiNesnesi);
            DataTable dt = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi);
            baglantiNesnesi.Open();
            adaptor.Fill(dt);
            baglantiNesnesi.Close();
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            label1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komutNesnesi = new SqlCommand("update urunler set urunFiyat=@urunFiyat where urunID=@urunID", baglantiNesnesi);
            komutNesnesi.Parameters.AddWithValue("@urunID", dataGridView1.CurrentRow.Cells[0].Value);
            komutNesnesi.Parameters.AddWithValue("@urunFiyat", textBox1.Text);
            baglantiNesnesi.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesi.Close();
            MessageBox.Show("Ürün fiyati girildi.");
            this.Hide();
            Form5 form5 = new Form5();
            form5.Show();
        }
    }
}
