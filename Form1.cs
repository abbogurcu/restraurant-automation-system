using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace restaurantOtomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Veri Tabanı Dosya Yolu Baglantisi
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Baran\Documents\restaurantOtomasyon\restaurant.mdf;Integrated Security=True;Connect Timeout=30");

        //Formlar arası veri aktarımında kullanilacak degiskenler
        public static string tcno, adi, soyadi, yetki;

        //Yalnizca bu formda kullanilacak degiskenler
        int hak = 3; bool durum = false;



        private void button1_Click(object sender, EventArgs e)
        {
            yonetici.yoneticiCheck = false;
            if (hak != 0)
            {
                baglanti.Open();
                SqlCommand selectsorgu = new SqlCommand("select *from kullanicilar", baglanti);
                SqlDataReader kayitokuma = selectsorgu.ExecuteReader();

                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if(kayitokuma["kullaniciadi"].ToString()==textBox1.Text && kayitokuma["parola"].ToString()==textBox2.Text 
                            && kayitokuma["yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide(); // basarili giris durumunda ilk formu gizle
                            yonetici.yoneticiCheck = true;
                            Form2 frm2 = new Form2();
                            frm2.Show();
                            break; //while tekrar tekrar girmesin 


                        }
                    }

                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text
                            && kayitokuma["yetki"].ToString() == "Şef")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide(); // basarili giris durumunda ilk formu gizle
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            break; //while tekrar tekrar girmesin 


                        }
                    }

                    if (radioButton3.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text
                            && kayitokuma["yetki"].ToString() == "Garson")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide(); // basarili giris durumunda ilk formu gizle
                            Form4 frm4 = new Form4();
                            frm4.Show();
                            break; //while tekrar tekrar girmesin 


                        }
                    }


                    if (radioButton4.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text
                            && kayitokuma["yetki"].ToString() == "Kasa")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide(); // basarili giris durumunda ilk formu gizle
                            Form6 frm6 = new Form6();
                            frm6.Show();
                            break; //while tekrar tekrar girmesin 


                        }
                    }
                }
            }
            if(durum == false)
            {
                hak--;
                baglanti.Close();
            }
            label5.Text = Convert.ToString(hak);
            if (hak == 0)
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş Hakkınız Kalmadı !!!", "Restaurant Otomasyon Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi";
            this.AcceptButton = button1; //Enter'e basildiginda buton1 calissin    
            this.CancelButton = button2; //ESC'ye basildiginda buton2 calissin
            label5.Text = Convert.ToString(hak);
            radioButton1.Checked = true; //radiobutton1 secili gelsin
            this.StartPosition = FormStartPosition.CenterScreen; //Ekranin merkezinde gelsin
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow; //kücültme ve tamEkran tuslari görünmez
        }
    }
}
