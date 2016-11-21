using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakaleYonetim
{
    public partial class YazarEkle : Form
    {
        public YazarEkle()
        {
            InitializeComponent();
        }

        string hata = "";
        private void button1_Click(object sender, EventArgs e)
        {


            tblKullanici.Validasyonlar(this);

            Data d = new Data();
            d.komut.CommandText = "Select * from tblKullanici where Kadi=@pkadi" ;
            d.komut.Parameters.AddWithValue("pkadi",txt_KullaniciAd.Text);
            DataTable dt = d.TabloGetir();
            if (dt.Rows.Count > 0) { hata += "\nKullanici adı daha önce kullanılmış."; }
            if(!txt_Mail.Text.Contains("@")) hata += "\nEmail geçerli değil.";
            if (txtSifre.Text != txt_SifreTekrar.Text) hata += "Şifreler eşleşmiyor.";

            if (!string.IsNullOrEmpty(hata)) MessageBox.Show(hata);
            else
            {
                Data de = new Data();
                de.komut.CommandText = @"INSERT INTO tblKullanici(AdSoyad,Kadi,Sifre,Telefon,EPosta,YetkiGrup) values(@ad,@kad,@sifre,@tel,@mail,@yetki)";
                de.komut.Parameters.AddWithValue("ad",txt_AdSoyad.Text);
                de.komut.Parameters.AddWithValue("kad", txt_KullaniciAd.Text);
                de.komut.Parameters.AddWithValue("sifre", txtSifre.Text);
                de.komut.Parameters.AddWithValue("tel", masked_Tel.Text);
                de.komut.Parameters.AddWithValue("mail", txt_Mail.Text);
                de.komut.Parameters.AddWithValue("yetki", checkBox1.Checked ? 1:2);
                de.KomutCalistir();
                MessageBox.Show("Eklendi.");
            }

        }

        private void btn_GosterGizle_Click(object sender, EventArgs e)
        {
            if (txtSifre.PasswordChar == '*')
            {
                txtSifre.PasswordChar = '\0';
                txt_SifreTekrar.PasswordChar = '\0';
            }
            else
            {
                txtSifre.PasswordChar = '*';
                txt_SifreTekrar.PasswordChar = '*';

            }
        }

        private void YazarEkle_Load(object sender, EventArgs e)
        {
            masked_Tel.Mask = "(999) 000 00 00";
        }

        private void btn_SifreUret_Click(object sender, EventArgs e)
        {
            string s = tblKullanici.ŞifreUret();
            txtSifre.Text = s;
            txt_SifreTekrar.Text = s; 

        }
    }
}
