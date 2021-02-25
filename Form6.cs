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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        SqlConnection baglantiNesnesi = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Baran\Documents\restaurantOtomasyon\restaurant.mdf;Integrated Security=True;Connect Timeout=30");
        SqlConnection baglantiNesnesi2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Baran\Documents\restaurantOtomasyon\restaurant.mdf;Integrated Security=True;Connect Timeout=30");

        double urunToplamFiyat = 0;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //REFRESH SİPARİŞ YEDEK//

            SqlCommand komutNesnesi22 = new SqlCommand("select * from siparisYedek", baglantiNesnesi2);
            baglantiNesnesi2.Open();
            SqlDataReader reader55 = komutNesnesi22.ExecuteReader();
            if (reader55.HasRows)
            {
                while (reader55.Read())
                {
                    DateTime hesapKapatmaZamani = Convert.ToDateTime(reader55[5]);
                    DateTime anlikDate = DateTime.Now;
                    if (hesapKapatmaZamani.Year == anlikDate.Year && hesapKapatmaZamani.Month == anlikDate.Month && hesapKapatmaZamani.Day == anlikDate.Day && hesapKapatmaZamani.Hour == anlikDate.Hour && anlikDate.Minute >= hesapKapatmaZamani.Minute + 2)
                    {
                        SqlCommand komutNesnesi7 = new SqlCommand("delete from siparisYedek where siparisMasasi='" + Convert.ToInt32(reader55[4]) + "'", baglantiNesnesi);
                        baglantiNesnesi.Open();
                        komutNesnesi7.ExecuteNonQuery();
                        baglantiNesnesi.Close();
                    }
                }
            }
            baglantiNesnesi2.Close();
            //
            urunToplamFiyat = 0;

            double urunFiyat;
            SqlCommand komutNesnesi = new SqlCommand("select * from siparis where siparisMasasi=@urunKategori", baglantiNesnesi);
            komutNesnesi.Parameters.AddWithValue("@urunKategori", Convert.ToInt32(comboBox1.SelectedItem));
            DataTable dt = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi);
            baglantiNesnesi.Open();
            adaptor.Fill(dt);
            baglantiNesnesi.Close();
            dataGridView1.DataSource = dt;
            baglantiNesnesi.Open();
            SqlDataReader reader = komutNesnesi.ExecuteReader();

            while (reader.Read()) {
                SqlCommand komutNesnesi2 = new SqlCommand("select urunFiyat from urunler where urunIsmi='"+reader[1].ToString()+"'", baglantiNesnesi2);
                baglantiNesnesi2.Open();


                SqlDataReader reader2 = komutNesnesi2.ExecuteReader();

                while (reader2.Read()) {
                    urunFiyat = Convert.ToDouble(reader2[0]);
                    urunToplamFiyat += urunFiyat * Convert.ToInt32(reader[2]);

                    
                }
                baglantiNesnesi2.Close();
            }
            label3.Text = urunToplamFiyat.ToString() +" TL";
            baglantiNesnesi.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double dusulecekFiyat = 0;
            SqlCommand komutNesnesi2 = new SqlCommand("select urunFiyat from urunler where urunIsmi='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'", baglantiNesnesi2);
            baglantiNesnesi2.Open();


            SqlDataReader reader2 = komutNesnesi2.ExecuteReader();

            while (reader2.Read())
            {
                dusulecekFiyat = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value) * Convert.ToDouble(reader2[0]);
                urunToplamFiyat -= dusulecekFiyat;
            }
            if (urunToplamFiyat < 0)
            {
                label3.Text = "HATA : " + 0 + " TL" + " ( Masadan " + urunToplamFiyat * -1 + " TL fazla ucret alindi.)";
            }
            else
            {
                label3.Text = urunToplamFiyat.ToString() + " TL" + " (" + dataGridView1.CurrentRow.Cells[0].Value + " nolu siparis odendi.)";
            }
            baglantiNesnesi2.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            urunToplamFiyat -= Convert.ToDouble(textBox1.Text);
            if (urunToplamFiyat < 0)
            {
                label3.Text = "HATA : "+0 + " TL" + " ( Masadan " + urunToplamFiyat*-1 + " TL fazla ucret alindi.)";
            }
            else {
                label3.Text = urunToplamFiyat.ToString() + " TL" + " ( Masadan " + Convert.ToDouble(textBox1.Text) + " TL dusuldu.)";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komutNesnesi2 = new SqlCommand("select * from siparis where siparisMasasi='"+ Convert.ToInt32(comboBox1.SelectedItem) + "'", baglantiNesnesi2);
            baglantiNesnesi2.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi2);
            adaptor.Fill(dt);
            baglantiNesnesi2.Close();

            if (dt.Rows.Count > 0) {

                baglantiNesnesi2.Open();
                SqlDataReader reader = komutNesnesi2.ExecuteReader();
                while (reader.Read()) {
                    int siparisID = Convert.ToInt32(reader[0]);
                    string siparisAdi = reader[1].ToString();
                    int siparisAdedi = Convert.ToInt32(reader[2]);
                    string siparisAciklama = reader[3].ToString();
                    int siparisMasasi = Convert.ToInt32(reader[4]);
                    DateTime date = DateTime.Now;
                    SqlCommand komutNesnesi3 = new SqlCommand("insert into siparisYedek(siparisID,siparisAdi,siparisAdedi,siparisAciklama,siparisMasasi,hesapKapatmaZamani) values (@siparisID,@siparisAdi,@siparisAdedi,@siparisAciklama,@siparisMasasi,@hesapKapatmaZamani)", baglantiNesnesi);
                    komutNesnesi3.Parameters.AddWithValue("@siparisID", siparisID);
                    komutNesnesi3.Parameters.AddWithValue("@siparisAdi", siparisAdi);
                    komutNesnesi3.Parameters.AddWithValue("@siparisAdedi", siparisAdedi);
                    komutNesnesi3.Parameters.AddWithValue("@siparisAciklama", siparisAciklama);
                    komutNesnesi3.Parameters.AddWithValue("@siparisMasasi", siparisMasasi);
                    komutNesnesi3.Parameters.AddWithValue("@hesapKapatmaZamani", date);
                    baglantiNesnesi.Open();
                    komutNesnesi3.ExecuteNonQuery();
                    baglantiNesnesi.Close();
                }

                SqlCommand komutNesnesi5 = new SqlCommand("delete from siparis where siparisMasasi='" + Convert.ToInt32(comboBox1.SelectedItem) + "'", baglantiNesnesi);
                baglantiNesnesi.Open();
                komutNesnesi5.ExecuteNonQuery();
                baglantiNesnesi.Close();
                baglantiNesnesi2.Close();
                SqlCommand komutNesnesi55 = new SqlCommand("delete from siparisOnay where MasaNo='" + Convert.ToInt32(comboBox1.SelectedItem) + "'", baglantiNesnesi);
                baglantiNesnesi.Open();
                komutNesnesi55.ExecuteNonQuery();
                baglantiNesnesi.Close();
                label3.Text = "0";
                MessageBox.Show("Hesap kapatildi.");
            }
            else
            {
                MessageBox.Show("Bu masaya ait siparis zaten bulunmamakta.");
            }
            SqlCommand komutNesnesi = new SqlCommand("select * from siparis where siparisMasasi=@urunKategori", baglantiNesnesi);
            komutNesnesi.Parameters.AddWithValue("@urunKategori", Convert.ToInt32(comboBox1.SelectedItem));
            DataTable dte = new DataTable();
            SqlDataAdapter adaptore = new SqlDataAdapter(komutNesnesi);
            baglantiNesnesi.Open();
            adaptore.Fill(dte);
            baglantiNesnesi.Close();
            dataGridView1.DataSource = dte;

            dataGridView1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Boolean a = false;
            Boolean b = false;
            SqlCommand komutNesnesi2 = new SqlCommand("select * from siparisYedek where siparisMasasi='" + Convert.ToInt32(comboBox1.SelectedItem) + "'", baglantiNesnesi2);
            baglantiNesnesi2.Open();
            SqlDataReader reader = komutNesnesi2.ExecuteReader();
            if (reader.HasRows) {
                while (reader.Read())
                {
                    DateTime hesapKapatmaZamani=Convert.ToDateTime(reader[5]);
                    DateTime anlikDate = DateTime.Now;
                    if (hesapKapatmaZamani.Year==anlikDate.Year&&hesapKapatmaZamani.Month==anlikDate.Month&&hesapKapatmaZamani.Day==anlikDate.Day&&hesapKapatmaZamani.Hour==anlikDate.Hour&&anlikDate.Minute >= hesapKapatmaZamani.Minute+2)
                    {
                        b = true;
                        SqlCommand komutNesnesi6 = new SqlCommand("delete from siparisYedek where siparisMasasi='" + Convert.ToInt32(comboBox1.SelectedItem) + "'", baglantiNesnesi);
                        baglantiNesnesi.Open();
                        komutNesnesi6.ExecuteNonQuery();
                        baglantiNesnesi.Close();
                        SqlCommand komutNesnesi5 = new SqlCommand("delete from siparisOnay where MasaNo='" + Convert.ToInt32(comboBox1.SelectedItem) + "'", baglantiNesnesi);
                        baglantiNesnesi.Open();
                        komutNesnesi5.ExecuteNonQuery();
                        baglantiNesnesi.Close();
                    }
                    else
                    {
                        int siparisID = Convert.ToInt32(reader[0]);
                        string siparisAdi = reader[1].ToString();
                        int siparisAdedi = Convert.ToInt32(reader[2]);
                        string siparisAciklama = reader[3].ToString();
                        int siparisMasasi = Convert.ToInt32(reader[4]);
                        DateTime date = DateTime.Now;
                        SqlCommand komutNesnesi3 = new SqlCommand("insert into siparis(siparisAdi,siparisAdedi,siparisAciklama,siparisMasasi) values (@siparisAdi,@siparisAdedi,@siparisAciklama,@siparisMasasi)", baglantiNesnesi);
                        komutNesnesi3.Parameters.AddWithValue("@siparisID", siparisID);
                        komutNesnesi3.Parameters.AddWithValue("@siparisAdi", siparisAdi);
                        komutNesnesi3.Parameters.AddWithValue("@siparisAdedi", siparisAdedi);
                        komutNesnesi3.Parameters.AddWithValue("@siparisAciklama", siparisAciklama);
                        komutNesnesi3.Parameters.AddWithValue("@siparisMasasi", siparisMasasi);
                        baglantiNesnesi.Open();
                        komutNesnesi3.ExecuteNonQuery();
                        baglantiNesnesi.Close();

                        a = true;
                        SqlCommand komutNesnesi7 = new SqlCommand("delete from siparisYedek where siparisMasasi='" + Convert.ToInt32(comboBox1.SelectedItem) + "'", baglantiNesnesi);
                        baglantiNesnesi.Open();
                        komutNesnesi7.ExecuteNonQuery();
                        baglantiNesnesi.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Zaten hesap kapatilmamis veya masa bos!");
            }
            if (a == true)
            {
                MessageBox.Show("Eski hesap geri getirildi.");
                SqlCommand komutNesnesi54 = new SqlCommand("insert into siparisOnay(MasaNo,siparis) values ('" + Convert.ToInt32(comboBox1.SelectedItem) + "','')", baglantiNesnesi);
                baglantiNesnesi.Open();
                komutNesnesi54.ExecuteNonQuery();
                baglantiNesnesi.Close();
            }
            if (b == true)
            {
                MessageBox.Show("1 dakikadan fazla zaman gecti. Artık hesabi geri getiremezsiniz.");
            }
            baglantiNesnesi2.Close();
            SqlCommand komutNesnesi = new SqlCommand("select * from siparis where siparisMasasi=@urunKategori", baglantiNesnesi);
            komutNesnesi.Parameters.AddWithValue("@urunKategori", Convert.ToInt32(comboBox1.SelectedItem));
            DataTable dt = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi);
            baglantiNesnesi.Open();
            adaptor.Fill(dt);
            baglantiNesnesi.Close();
            dataGridView1.DataSource = dt;

            dataGridView1.Refresh();
        }
    }
}
