using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaleYonetim
{
    class tblKullanici //tblKullanici (model)
    {
        public static tblKullanici GirisYapan { get; set; }
        public int KullaniciID { get; set; }
        public string Kadi { get; set; }
        public string Sifre { get; set; }
        public string AdSoyad { get; set; }
        public int YetkiGrup { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
    }
}
