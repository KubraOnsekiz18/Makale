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

    public partial class YazarDüzenle : Form
    {
        public int gelenid { get; set; }
        public YazarDüzenle()
        {
            InitializeComponent();
        }

        private void YazarDüzenle_Load(object sender, EventArgs e)
        {
            Data d = new Data();
            d.komut.CommandText = "Select * from tblKullanici where KullaniciID=" + gelenid;
            DataRow dr = d.SatirGetir();
            txt_AdSoyad.Text = dr["AdSoyad"].ToString();
            txt_KullaniciAd.Text = dr["Kadi"].ToString();
            txtSifre.Text = dr["Sifre"].ToString();
            txt_SifreTekrar.Text = dr["Sifre"].ToString();
            masked_Tel.Text = dr["Telefon"].ToString();
            txt_Mail.Text = dr["EPosta"].ToString();
            checkBox1.Checked = dr["YetkiGrup"].ToString() == "1" ? true : false;
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

        private void btn_SifreUret_Click(object sender, EventArgs e)
        {

            string s = tblKullanici.ŞifreUret();
            txtSifre.Text = s;
            txt_SifreTekrar.Text = s;
        }
        string hata = "";
        private void button1_Click(object sender, EventArgs e)
        {

            tblKullanici.Validasyonlar(this);

            if (!txt_Mail.Text.Contains("@")) hata += "\nEmail geçerli değil.";
            if (txtSifre.Text != txt_SifreTekrar.Text) hata += "Şifreler eşleşmiyor.";

            if (!string.IsNullOrEmpty(hata)) MessageBox.Show(hata);
            else
            {
                Data de = new Data();
                de.komut.CommandText = @"UPDATE tblKullanici SET AdSoyad=@ad,Kadi=@kad,Sifre=@sifre,Telefon=@tel,EPosta=@mail,YetkiGrup=@yetki Where KullaniciID=@id";
                de.komut.Parameters.AddWithValue("id", gelenid);
                de.komut.Parameters.AddWithValue("ad", txt_AdSoyad.Text);
                de.komut.Parameters.AddWithValue("kad", txt_KullaniciAd.Text);
                de.komut.Parameters.AddWithValue("sifre", txtSifre.Text);
                de.komut.Parameters.AddWithValue("tel", masked_Tel.Text);
                de.komut.Parameters.AddWithValue("mail", txt_Mail.Text);

                de.komut.Parameters.AddWithValue("yetki", checkBox1.Checked ? 1 : 2);
                de.KomutCalistir();
                MessageBox.Show("Düzenlendi.");
            }

        }
    }

}
