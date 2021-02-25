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
using System.Text.RegularExpressions; // güvenli parola oluşturulması icin hazır kodlara sahip kutuphane
using System.IO;

namespace restaurantOtomasyon
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Baran\Documents\restaurantOtomasyon\restaurant.mdf;Integrated Security=True;Connect Timeout=30");

        private void kullanicilariGoster()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter kullanicilariListele = new SqlDataAdapter("select tcno AS [TC KİMLİK NO], ad AS [ADI], soyad AS [SOYADI], yetki AS [YETKİ], kullaniciadi AS [KULLANICI ADI], parola AS [PAROLA] from kullanicilar Order By ad ASC", baglanti);
                DataSet dshafiza = new DataSet();
                kullanicilariListele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception hataMsj)
            {
                MessageBox.Show(hataMsj.Message, "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();

            }
        }
        private void personelleriGoster()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter personelleriListele = new SqlDataAdapter("select tcno AS [TC KİMLİK NO], ad AS [ADI], soyad AS [SOYADI], cinsiyet AS [CİNSİYETİ], mezuniyet AS [MEZUNİYETİ], dogumtarihi AS [DOĞUM TARİHİ], gorevi AS [GÖREVİ], maasi AS [MAAŞI] from personeller Order By ad ASC", baglanti);
                DataSet dshafiza = new DataSet();
                personelleriListele.Fill(dshafiza);
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception hataMsj)
            {
                MessageBox.Show(hataMsj.Message, "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();

            }
        } 


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if(yonetici.yoneticiCheck==true)
            {
                button12.Visible = true;
            }
            else
            {
                button12.Visible = false;
            }

            //form2 ayarlari
            pictureBox1.Height = 190;
            pictureBox1.Width = 190;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // resmi pictureboxa sigdir
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciResimler\\" + Form1.tcno + ".jpg"); // bin>debug'daki resimlerden tc'si uyusani getir !!!
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciResimler\\resimyok.png"); //kullanicinin resmi yoksa resimyok avatarini getir !!!

            }
            //kullanici islemleri sekmesi
            this.Text = "YÖNETİCİ İŞLEMLERİ";
            label11.ForeColor = Color.DarkRed;
            label11.Text = Form1.adi + " " + Form1.soyadi;
            textBox1.MaxLength = 11; // tc 11 haneden fazla girilemesin
            textBox4.MaxLength = 15; // isim 15 haneden fazla girilemesin
            radioButton1.Checked = true;
            textBox2.CharacterCasing = CharacterCasing.Upper; // textbox2 ve textbox3 yazilanlari büyük harfe cevir
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 15;
            textBox6.MaxLength = 15;

            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            kullanicilariGoster(); //kullanici islemleri sekmesi bitti

            //personel islemleri sekmsi
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Height = 110;
            pictureBox2.Width = 110;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D; //picturebox2 nin cevresi 3bouytlu gozuksun
            maskedTextBox1.Mask = "00000000000"; // kullanici tcno ya 11 karakter girmek zorunda gorunecek
            maskedTextBox2.Mask = "LL????????????????????"; //ad icin iki harf mecbur girilsin 22 karaktere kadar istenirse giriilsin
            maskedTextBox3.Mask = "LL????????????????????";
            maskedTextBox4.Mask = "0000"; //maas en az ve en cok 4 haneli olacak
            maskedTextBox4.Text = "0";
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper(); // ad ve soyad karakterlerini buyuk yaz

            comboBox1.Items.Add("İlköğretim");
            comboBox1.Items.Add("Ortaöğretim");
            comboBox1.Items.Add("Lise");
            comboBox1.Items.Add("Üniversite");

            comboBox2.Items.Add("Yönetici");
            comboBox2.Items.Add("Şef");
            comboBox2.Items.Add("Garson");
            comboBox2.Items.Add("Aşçı");
            comboBox2.Items.Add("Barista");
            comboBox2.Items.Add("Kurye");

            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gun = int.Parse(zaman.ToString("dd"));

            dateTimePicker1.MinDate = new DateTime(1960, 1, 1); //en dusuk tarih 1960 alinsin
            dateTimePicker1.MaxDate = new DateTime(yil - 18, ay, gun); // 18 yasindan kucukler calisamayacagi icin simdikiZaman-18 yıl
            dateTimePicker1.Format = DateTimePickerFormat.Short;

            radioButton4.Checked = true;
            personelleriGoster();
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
            if (textBox1.Text.Length < 11)

                errorProvider1.SetError(textBox1, "TC Kimlik No 11 Karakter Olmalı !!!");
            else
                errorProvider1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;  //tcno kisminda klavyeden basılan tus ASCII karsiligi 48 57 arasi olacak ve backspace tusuna da basabilecek yani sadece rakam basilacak
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true; //textbox2 ve 3 yani ad ve soyad icin kullanici sadece harf girebilsin 
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true; //kullaniciadi nda harf sayi ve backspaceye izin veriyoz
        }

        int parola_skoru = 0;

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox5.Text;
            //Regex kütüphanesi İngilizce karakterleri baz aldığından, Türkçe karakterlerde sorun yaşamamak için sifre string ifadesindeki Türkçe karakterleri İngilizce karakterlere dönüştürmemiz gerekiyor.
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');
            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Paroladaki Türkçe karakterler İngilizce karakterlere dönüştürülmüştür!");
            }
            //1 küçük harf 10 puan, 2 ve üzeri 20 puan
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;

            //1 büyük harf 10 puan, 2 ve üzeri 20 puan
            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;

            //1 rakam 10 puan, 2 ve üzeri 20 puan
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            //1 sembol 10 puan, 2 ve üzeri 20 puan
            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;
            if (sifre.Length == 9)
                parola_skoru += 10;
            else if (sifre.Length == 10)
                parola_skoru += 20;
            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
                label21.Text = "Büyük harf, küçük harf, rakam ve sembol mutlaka kullanmalısın!";
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
                label21.Text = "";

            if (parola_skoru < 70)
                parola_seviyesi = "Kabul edilemez!";
            else if (parola_skoru == 70 || parola_skoru == 80)
                parola_seviyesi = "Güçlü";
            else if (parola_skoru == 90 || parola_skoru == 100)
                parola_seviyesi = "Çok Güçlü";
            label9.Text = "%" + Convert.ToString(parola_skoru);
            label10.Text = parola_seviyesi;
            progressBar1.Value = parola_skoru;
        }

        private void topPage1Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void topPage2Temizle()
        {
            pictureBox2.Image = null;
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox3.Clear();
            maskedTextBox4.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1; //comboboxlarda bir sey gorunmemesi icin
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string yetki = "";
            bool kayitkontrol = false;

            baglanti.Open();
            SqlCommand selectsorgu = new SqlCommand("select *from kullanicilar where tcno = '" + textBox1.Text + "' ", baglanti);
            SqlDataReader kayitokuma = selectsorgu.ExecuteReader();

            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglanti.Close();

            if (kayitkontrol == false)
            {
                //tcno kontrol
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
                //adı kontorl
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;
                //soyadı kontrol
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;
                //kullaniciadi kontrol
                if (textBox4.Text.Length < 2 || textBox4.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;
                //parola kontrol
                if (textBox5.Text == "" || parola_skoru < 70)
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;
                //parolatekrar kontrol
                if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;
                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
                {
                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";
                    else if (radioButton2.Checked == true)
                        yetki = "Şef";
                    else 
                        yetki = "Garson";
                    try
                    {
                        baglanti.Open();
                        SqlCommand eklekomutu = new SqlCommand("insert into kullanicilar values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + yetki + "','" + textBox4.Text + "','" + textBox5.Text + "')", baglanti);
                        eklekomutu.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kayıt Başarıyla Oluşturuldu!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        kullanicilariGoster();
                        topPage1Temizle();
                    }
                    catch (Exception hataMsj)
                    {
                        MessageBox.Show(hataMsj.Message);
                        baglanti.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Tekrar Gözden Geçiriniz!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarası Daha Önceden Kayıtlıdır!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool kayitAramaDurumu = false; //yazdigimiz tcno ya uygun kayit aramasi
            if (textBox1.Text.Length == 11)
            {
                baglanti.Open();
                SqlCommand selectsorgu = new SqlCommand("select *from kullanicilar where tcno='" + textBox1.Text + "'", baglanti);
                SqlDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitAramaDurumu = true;
                    textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(2).ToString();
                    if (kayitokuma.GetValue(3).ToString() == "Yönetici")
                        radioButton1.Checked = true;
                    else if (kayitokuma.GetValue(3).ToString() == "Şef")
                        radioButton2.Checked = true;
                    else
                        radioButton3.Checked = true;
                    textBox4.Text = kayitokuma.GetValue(4).ToString();
                    textBox5.Text = kayitokuma.GetValue(5).ToString();
                    textBox6.Text = kayitokuma.GetValue(5).ToString();
                    break;
                }
                if (kayitAramaDurumu == false)
                {
                    MessageBox.Show("Aranan Kayıt Bulunamadı !!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Lütfen 11 Haneli Bir TC Kimlik Numarası Giriniz !!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1Temizle();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                bool kayitAramaDurumu = false;
                baglanti.Open();
                SqlCommand selectsorgu = new SqlCommand("select *from kullanicilar where tcno='" + textBox1.Text + "'", baglanti);
                SqlDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayitAramaDurumu = true;
                    SqlCommand deletesorgu = new SqlCommand("delete from kullanicilar where tcno='" + textBox1.Text + "'", baglanti);
                    kayitokuma.Close();
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı Kaydı Başarıyla Silindi!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglanti.Close();
                    kullanicilariGoster();
                    topPage1Temizle();
                    break;
                }
                if (kayitAramaDurumu == false)
                    MessageBox.Show("Silinecek Kayıt Bulunamadı!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
                topPage1Temizle();
            }
            else
                MessageBox.Show("Lütfen 11 Karakterden Oluşan Bir TC Kimlik Numarası Giriniz!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string yetki = "";
            //tcno kontrol
            if (textBox1.Text.Length < 11 || textBox1.Text == "")
                label1.ForeColor = Color.Red;
            else
                label1.ForeColor = Color.Black;
            //adı kontorl
            if (textBox2.Text.Length < 2 || textBox2.Text == "")
                label2.ForeColor = Color.Red;
            else
                label2.ForeColor = Color.Black;
            //soyadı kontrol
            if (textBox3.Text.Length < 2 || textBox3.Text == "")
                label3.ForeColor = Color.Red;
            else
                label3.ForeColor = Color.Black;
            //kullaniciadi kontrol
            if (textBox4.Text.Length < 2 || textBox4.Text == "")
                label5.ForeColor = Color.Red;
            else
                label5.ForeColor = Color.Black;
            //parola kontrol
            if (textBox5.Text == "" || parola_skoru < 70)
                label6.ForeColor = Color.Red;
            else
                label6.ForeColor = Color.Black;
            //parolatekrar kontrol
            if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                label7.ForeColor = Color.Red;
            else
                label7.ForeColor = Color.Black;
            if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
            {
                if (radioButton1.Checked == true)
                    yetki = "Yönetici";
                else if (radioButton2.Checked == true)
                    yetki = "Şef";
                else
                    yetki = "Garson";

                try
                {
                    baglanti.Open();
                    SqlCommand guncellekomutu = new SqlCommand("update kullanicilar set ad='" + textBox2.Text + "', soyad='" + textBox3.Text + "', yetki='" + yetki + "', kullaniciadi='" + textBox4.Text + "', parola='" + textBox5.Text + "' where tcno='" + textBox1.Text + "' ", baglanti);
                    guncellekomutu.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kullanıcı Bilgileri Güncellendi!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    kullanicilariGoster();


                }
                catch (Exception hataMsj)
                {
                    MessageBox.Show(hataMsj.Message, "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();

                }
            }

            else
            {
                MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Tekrar Gözden Geçiriniz!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            topPage1Temizle();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog(); //picturebox2 ye resim secmek icin
            resimsec.Title = "Personel Resmi Seçiniz"; //gelen pencerenin basligi
            resimsec.Filter = "JPG Dosyalar (*.jpg) | *.jpg"; //kullanici sadece jpg dosyalari görecek
            if (resimsec.ShowDialog() == DialogResult.OK) //kullaniciye resimsec gösterildeyse
            {
                this.pictureBox2.Image = new Bitmap(resimsec.OpenFile()); // secilen resim picturebox2ye yüklensin
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool kayitAramaDurumu = false;
            if (maskedTextBox1.Text.Length == 11)
            {
                baglanti.Open();
                SqlCommand selectSorgu = new SqlCommand("select *from personeller where tcno='" + maskedTextBox1.Text + "'", baglanti);
                SqlDataReader kayitOkuma = selectSorgu.ExecuteReader();
                while (kayitOkuma.Read())
                {
                    kayitAramaDurumu = true;
                    try
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelResimler\\" + kayitOkuma.GetValue(0).ToString() + ".jpg");
                    }
                    catch
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelResimler\\resimyok.png");
                    }
                    maskedTextBox2.Text = kayitOkuma.GetValue(1).ToString();
                    maskedTextBox3.Text = kayitOkuma.GetValue(2).ToString();
                    if (kayitOkuma.GetValue(3).ToString() == "Erkek")
                        radioButton4.Checked = true;
                    else
                        radioButton5.Checked = true;
                    comboBox1.Text = kayitOkuma.GetValue(4).ToString();
                    dateTimePicker1.Text = kayitOkuma.GetValue(5).ToString();
                    comboBox2.Text = kayitOkuma.GetValue(6).ToString();
                    maskedTextBox4.Text = kayitOkuma.GetValue(7).ToString();
                    break;
                }
                if (kayitAramaDurumu == false)
                    MessageBox.Show("Aranan Kayıt Bulunamadı!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("11 Haneli Bir TC Kimlik Numarası Giriniz!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            bool kayitKontrol = false;

            baglanti.Open();
            SqlCommand selectSorgu = new SqlCommand("select *from personeller where tcno='" + maskedTextBox1.Text + "'", baglanti);
            SqlDataReader kayitOkuma = selectSorgu.ExecuteReader();
            while (kayitOkuma.Read())
            {
                kayitKontrol = true;
                break;
            }
            baglanti.Close();
            if (kayitKontrol == false)
            {
                if (pictureBox2.Image == null)
                    button6.ForeColor = Color.Red;
                else
                    button6.ForeColor = Color.Black;

                if (maskedTextBox1.MaskCompleted == false)
                    label13.ForeColor = Color.Red;
                else
                    label13.ForeColor = Color.Black;

                if (maskedTextBox2.MaskCompleted == false)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.Black;

                if (maskedTextBox3.MaskCompleted == false)
                    label15.ForeColor = Color.Red;
                else
                    label15.ForeColor = Color.Black;

                if (comboBox1.Text == "")
                    label17.ForeColor = Color.Red;
                else
                    label17.ForeColor = Color.Black;

                if (comboBox2.Text == "")
                    label19.ForeColor = Color.Red;
                else
                    label19.ForeColor = Color.Black;

                if (maskedTextBox4.MaskCompleted == false)
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;

                if (int.Parse(maskedTextBox4.Text) < 1000)
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;

                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text != ""  && maskedTextBox4.MaskCompleted != false)
                {
                    if (radioButton4.Checked == true)
                        cinsiyet = "Erkek";
                    else if (radioButton5.Checked == true)
                        cinsiyet = "Kadın";

                    try
                    {
                        baglanti.Open();
                        SqlCommand ekleKomutu = new SqlCommand("insert into personeller values('" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + cinsiyet + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "','" + comboBox2.Text + "','" + maskedTextBox4.Text + "')", baglanti);
                        ekleKomutu.ExecuteNonQuery();
                        baglanti.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\personelResimler")) //bin>debug icinde personelresimler klasörü yoksa
                            Directory.CreateDirectory(Application.StartupPath + "\\personelResimler"); //klasör yoksa biz olusturduk   
                        pictureBox2.Image.Save(Application.StartupPath + "\\personelResimler\\" + maskedTextBox1.Text + ".jpg"); //resmi tcno ile klasöre kayıt ettik
                        MessageBox.Show("Yeni Personel Kaydı Oluşturuldu!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        personelleriGoster();
                        topPage2Temizle();
                        maskedTextBox4.Text = "0"; //maasda deger olmadiginda hata almamak icin
                    }
                    catch (Exception hataMsj)
                    {
                        MessageBox.Show(hataMsj.Message, "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglanti.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Yazı Rengi Kırmızı Olan Alanları Yeniden Gözden Geçirin!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarası Daha Önceden Kayıtlıdır!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";

            if (pictureBox2.Image == null)
                button6.ForeColor = Color.Red;
            else
                button6.ForeColor = Color.Black;

            if (maskedTextBox1.MaskCompleted == false)
                label13.ForeColor = Color.Red;
            else
                label13.ForeColor = Color.Black;

            if (maskedTextBox2.MaskCompleted == false)
                label14.ForeColor = Color.Red;
            else
                label14.ForeColor = Color.Black;

            if (maskedTextBox3.MaskCompleted == false)
                label15.ForeColor = Color.Red;
            else
                label15.ForeColor = Color.Black;

            if (comboBox1.Text == "")
                label17.ForeColor = Color.Red;
            else
                label17.ForeColor = Color.Black;

            if (comboBox2.Text == "")
                label19.ForeColor = Color.Red;
            else
                label19.ForeColor = Color.Black;

            if (maskedTextBox4.MaskCompleted == false)
                label20.ForeColor = Color.Red;
            else
                label20.ForeColor = Color.Black;

            if (int.Parse(maskedTextBox4.Text) < 1000)
                label20.ForeColor = Color.Red;
            else
                label20.ForeColor = Color.Black;

            if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text !=  "" && maskedTextBox4.MaskCompleted != false)
            {
                if (radioButton4.Checked == true)
                    cinsiyet = "Erkek";
                else if (radioButton5.Checked == true)
                    cinsiyet = "Kadın";

                try
                {
                    baglanti.Open();
                    SqlCommand guncelleKomutu = new SqlCommand("update personeller set ad= '" + maskedTextBox2.Text + "', soyad= '" + maskedTextBox3.Text + "', cinsiyet='" + cinsiyet + "', mezuniyet='" + comboBox1.Text + "', dogumtarihi='" + dateTimePicker1.Text + "', gorevi='" + comboBox2.Text + "' , maasi='" + maskedTextBox4.Text + "' where tcno='" + maskedTextBox1.Text + "'", baglanti);
                    guncelleKomutu.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Güncelleştirme Başarıyla Tamamlandı!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    personelleriGoster();
                    topPage2Temizle();
                    maskedTextBox4.Text = "0"; //maasda deger olmadiginda hata almamak icin
                }
                catch (Exception hataMsj)
                {
                    MessageBox.Show(hataMsj.Message, "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.MaskCompleted == true)
            {
                bool kayitAramaDurumu = false;
                baglanti.Open();
                SqlCommand aramaSorgusu = new SqlCommand("select *from personeller where tcno='" + maskedTextBox1.Text + "'", baglanti);
                SqlDataReader kayitOkuma = aramaSorgusu.ExecuteReader();
                while (kayitOkuma.Read())
                {
                    kayitAramaDurumu = true;
                    SqlCommand deleteSorgu = new SqlCommand("delete from personeller where tcno='" + maskedTextBox1.Text + "'", baglanti);
                    kayitOkuma.Close();
                    MessageBox.Show("Kayıt Başarıyla Siindi!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    deleteSorgu.ExecuteNonQuery();
                    break;
                }
                if (kayitAramaDurumu == false)
                {
                    MessageBox.Show("Silinecek Kayıt Bulunamadı!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglanti.Close();
                personelleriGoster();
                topPage2Temizle();
                maskedTextBox4.Text = "0";
            }
            else
            {
                MessageBox.Show("Lütfen 11 Haneden Oluşan Bir TC Kimlik Numarası Giriniz!!!", "Restaurant Otomasyon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage2Temizle();
                maskedTextBox4.Text = "0";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            topPage2Temizle();
            maskedTextBox4.Text = "0";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 form5 = new Form5();
            form5.Show();
        }
    }
}
