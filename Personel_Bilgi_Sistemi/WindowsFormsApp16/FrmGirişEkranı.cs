using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp16
{
    public partial class FrmGirişEkranı : Form
    {
        public FrmGirişEkranı()
        {
            InitializeComponent();
        }

        //veri tabanı bağlantısı
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KQHA9KT;Initial Catalog=PersonelVeriTabani;Integrated Security=True");


        //Giriş Doğrulama İşlemi
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_Yonetici where KullaniciAd=@p1 and Sifre=@p2",baglanti);
            komut.Parameters.AddWithValue("@p1",TxtKullaniciAd.Text);
            komut.Parameters.AddWithValue("@p2",TxtSifre.Text);
            SqlDataReader dr=komut.ExecuteReader();
            if (dr.Read())
            {
                FrmAnasayfa fr=new FrmAnasayfa();
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı adı ya da Şifre");
            }
            baglanti.Close();
        }   
    }
}
