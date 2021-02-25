using System;
using System.Collections;
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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        SqlConnection baglantiNesnesi = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Baran\Documents\restaurantOtomasyon\restaurant.mdf;Integrated Security=True;Connect Timeout=30");


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand komutNesnesi = new SqlCommand("select urunIsmi from urunler where urunKategori=@urunKategori", baglantiNesnesi);
            komutNesnesi.Parameters.AddWithValue("@urunKategori", comboBox1.SelectedItem.ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi);
            baglantiNesnesi.Open();
            adaptor.Fill(dt);
            baglantiNesnesi.Close();
            dataGridView1.DataSource = dt;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            comboBox1.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            textBox2.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label11.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null) {
                label11.Text = comboBox2.SelectedItem.ToString();

                SqlCommand komutNesnesi = new SqlCommand("select * from siparisOnay where masaNo='" + Convert.ToInt32(label11.Text) + "'", baglantiNesnesi);
                DataTable dt = new DataTable();
                SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi);
                baglantiNesnesi.Open();
                adaptor.Fill(dt);
                baglantiNesnesi.Close();
                if (dt.Rows.Count < 1)
                {
                    comboBox2.Visible = false;
                    label11.Visible = true;
                    label1.Visible = true;
                    comboBox1.Visible = true;
                    comboBox3.SelectedIndex = 0;
                    comboBox3.Visible = true;
                    comboBox4.Visible = true;
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    label7.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                    label10.Visible = true;
                    textBox2.Visible = true;
                    textBox1.Visible = true;
                    button1.Visible = true;
                    button6.Visible = false;
                    SqlCommand komutNesnesi2 = new SqlCommand("select siparisAdi,siparisAdedi,siparisAciklama,siparisID from siparis where siparisMasasi='" + Convert.ToInt32(label11.Text) + "'", baglantiNesnesi);
                    DataTable dt2 = new DataTable();
                    SqlDataAdapter adaptor2 = new SqlDataAdapter(komutNesnesi2);
                    baglantiNesnesi.Open();
                    adaptor2.Fill(dt2);
                    baglantiNesnesi.Close();
                    if (dt2.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dt2;
                        button5.Visible = true;
                        button2.Visible = true;
                        button3.Visible = true;
                    }
                }
                else {
                    MessageBox.Show("Bu masanın siparişi onaylanmış!");
                    label1.Visible = false;
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                    comboBox3.Visible = false;
                    comboBox4.Visible = false;
                    dataGridView1.Visible = false;
                    dataGridView2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label7.Visible = false;
                    label8.Visible = false;
                    label9.Visible = false;
                    label10.Visible = false;
                    textBox2.Visible = false;
                    textBox1.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    label11.Visible = true;
                    button5.Visible = false;
                    button6.Visible = true;
                    SqlCommand komutNesnesi2 = new SqlCommand("select siparisAdi,siparisAdedi,siparisAciklama,siparisID from siparis where siparisMasasi='" + Convert.ToInt32(label11.Text) + "'", baglantiNesnesi);
                    DataTable dt2 = new DataTable();
                    SqlDataAdapter adaptor2 = new SqlDataAdapter(komutNesnesi2);
                    baglantiNesnesi.Open();
                    adaptor2.Fill(dt2);
                    baglantiNesnesi.Close();
                    dataGridView2.DataSource = dt2;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            this.Hide();
            frm4.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()))
                label10.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(label10.Text))
            {
                SqlCommand komutNesnesi = new SqlCommand("insert into siparis(siparisAdi,siparisAdedi,siparisAciklama,siparisMasasi) values(@siparisAdi,@siparisAdedi,@siparisAciklama,@siparisMasasi)", baglantiNesnesi);
                komutNesnesi.Parameters.AddWithValue("@siparisAdi", label10.Text);
                komutNesnesi.Parameters.AddWithValue("@siparisAdedi", comboBox3.SelectedItem.ToString());
                komutNesnesi.Parameters.AddWithValue("@siparisAciklama", textBox1.Text);
                komutNesnesi.Parameters.AddWithValue("@siparisMasasi", label11.Text);
                baglantiNesnesi.Open();
                komutNesnesi.ExecuteNonQuery();
                baglantiNesnesi.Close();
                MessageBox.Show("Siparis eklendi!");

                SqlCommand komutNesnesi2 = new SqlCommand("select siparisAdi,siparisAdedi,siparisAciklama,siparisID from siparis where siparisMasasi='" + Convert.ToInt32(label11.Text) + "'", baglantiNesnesi);
                DataTable dt = new DataTable();
                SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi2);
                baglantiNesnesi.Open();
                adaptor.Fill(dt);
                baglantiNesnesi.Close();
                dataGridView2.DataSource = dt;
                button5.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
            }
            else {
                MessageBox.Show("Lütfen ürün seçiniz!");
            }


        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dataGridView2.CurrentRow.Cells[0].Value.ToString()))
            {
                label9.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                comboBox4.SelectedItem = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komutNesnesi = new SqlCommand("update siparis set siparisAdedi=@siparisAdedi,siparisAciklama=@siparisAciklama where siparisID='" + dataGridView2.CurrentRow.Cells[3].Value + "'", baglantiNesnesi);
            komutNesnesi.Parameters.AddWithValue("@siparisAdedi", Convert.ToInt32(comboBox4.SelectedItem.ToString()));
            komutNesnesi.Parameters.AddWithValue("@siparisAciklama", textBox2.Text);
            baglantiNesnesi.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesi.Close();
            MessageBox.Show("Siparis güncellendi!");
            SqlCommand komutNesnesi2 = new SqlCommand("select siparisAdi,siparisAdedi,siparisAciklama,siparisID from siparis where siparisMasasi='" + Convert.ToInt32(label11.Text) + "'", baglantiNesnesi);
            DataTable dt = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi2);
            baglantiNesnesi.Open();
            adaptor.Fill(dt);
            baglantiNesnesi.Close();
            dataGridView2.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand komutNesnesi = new SqlCommand("delete from siparis where siparisID='" + dataGridView2.CurrentRow.Cells[3].Value + "'", baglantiNesnesi);
            baglantiNesnesi.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesi.Close();
            MessageBox.Show("Siparis silindi!");
            SqlCommand komutNesnesi2 = new SqlCommand("select siparisAdi,siparisAdedi,siparisAciklama,siparisID from siparis where siparisMasasi='" + Convert.ToInt32(label11.Text) + "'", baglantiNesnesi);
            DataTable dt = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi2);
            baglantiNesnesi.Open();
            adaptor.Fill(dt);
            baglantiNesnesi.Close();
            dataGridView2.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String siparis=null;
            String siparisAciklama = null;
            SqlCommand komutNesnesi2 = new SqlCommand("select siparisAdi,siparisAdedi,siparisAciklama,siparisID from siparis where siparisMasasi='" + Convert.ToInt32(label11.Text) + "'", baglantiNesnesi);
            baglantiNesnesi.Open();
            SqlDataReader reader = komutNesnesi2.ExecuteReader();
            while (reader.Read()) {
                if (!string.IsNullOrEmpty(reader[2].ToString())){
                    siparisAciklama = reader[2].ToString();
                }
                else
                {
                    siparisAciklama = "Yok";
                }
                siparis+= "|     " + reader[0].ToString() + " // Adedi = " + reader[1].ToString() + " // Aciklama = " + siparisAciklama + "     |\n";
            }
            baglantiNesnesi.Close();
            SqlCommand komutNesnesi = new SqlCommand("insert into siparisOnay values(@masaNo,@siparis)", baglantiNesnesi);
            komutNesnesi.Parameters.AddWithValue("@masaNo", Convert.ToInt32(label11.Text));
            komutNesnesi.Parameters.AddWithValue("@siparis", siparis);
            baglantiNesnesi.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesi.Close();
            MessageBox.Show("Siparis onaylandi!");
            label1.Visible = false;
            comboBox1.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            textBox2.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label11.Visible = true;
            button5.Visible = false;
            button6.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlCommand komutNesnesi = new SqlCommand("delete from siparisOnay where masaNo='" + label11.Text + "'", baglantiNesnesi);
            baglantiNesnesi.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesi.Close();
            MessageBox.Show("Onay geri alindi!");
            comboBox2.Visible = false;
            label11.Visible = true;
            label1.Visible = true;
            comboBox1.Visible = true;
            comboBox3.SelectedIndex = 0;
            comboBox3.Visible = true;
            comboBox4.Visible = true;
            dataGridView1.Visible = true;
            dataGridView2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            textBox2.Visible = true;
            textBox1.Visible = true;
            button1.Visible = true;
            button6.Visible = false;
            SqlCommand komutNesnesi2 = new SqlCommand("select siparisAdi,siparisAdedi,siparisAciklama,siparisID from siparis where siparisMasasi='" + Convert.ToInt32(label11.Text) + "'", baglantiNesnesi);
            DataTable dt2 = new DataTable();
            SqlDataAdapter adaptor2 = new SqlDataAdapter(komutNesnesi2);
            baglantiNesnesi.Open();
            adaptor2.Fill(dt2);
            baglantiNesnesi.Close();
            if (dt2.Rows.Count > 0)
            {
                dataGridView2.DataSource = dt2;
                button5.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
            }
        }
    }
}
