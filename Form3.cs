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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            label6.Visible = false;
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        SqlConnection baglantiNesnesi = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Baran\Documents\restaurantOtomasyon\restaurant.mdf;Integrated Security=True;Connect Timeout=30");

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komutNesnesi = new SqlCommand("insert into urunler(urunKategori,urunIsmi) values(@urunKategori,@urunIsmi)", baglantiNesnesi);
            komutNesnesi.Parameters.AddWithValue("@urunKategori", comboBox1.SelectedItem.ToString());
            komutNesnesi.Parameters.AddWithValue("@urunIsmi", textBox2.Text);
            baglantiNesnesi.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesi.Close();
            MessageBox.Show("Ürün eklendi!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komutNesnesi = new SqlCommand("delete from urunler where urunID='" + Convert.ToInt32(label6.Text) + "'", baglantiNesnesi);
            baglantiNesnesi.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesi.Close();
            MessageBox.Show("Ürün silindi!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand komutNesnesi = new SqlCommand("update urunler set urunKategori=@urunKategori,urunIsmi=@urunIsmi where urunID='"+ Convert.ToInt32(label6.Text) + "'", baglantiNesnesi);
            komutNesnesi.Parameters.AddWithValue("@urunKategori", comboBox2.SelectedItem.ToString());
            komutNesnesi.Parameters.AddWithValue("@urunIsmi", textBox3.Text);
            baglantiNesnesi.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesi.Close();
            MessageBox.Show("Ürün güncellendi!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komutNesnesi = new SqlCommand("select * from urunler where urunIsmi LIKE  '" + textBox1.Text + "'+'%'", baglantiNesnesi);
            DataTable dt = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi);
            baglantiNesnesi.Open();
            adaptor.Fill(dt);
            baglantiNesnesi.Close();
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                label6.Text = row.Cells[0].Value.ToString();
                comboBox2.SelectedItem = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
            }
        }
    }
}
