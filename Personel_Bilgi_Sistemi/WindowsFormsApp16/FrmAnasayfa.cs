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
    public partial class FrmAnasayfa : Form
    {
        public FrmAnasayfa()
        {
            InitializeComponent();
        }
        

        //Form yüklendiğinde tabloyu listeleme
        private void Form1_Load(object sender, EventArgs e)
        {     
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel); 
        }

        //SqlConnection sınıfından baglantı isminde nesne oluşturma. Veri tabanı ile bağlantı sağlamak için yapıldı. 

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KQHA9KT;Initial Catalog=PersonelVeriTabani;Integrated Security=True"); 


        //Temizleme Fonksiyonu
        void temizle()
        {
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            CmbSehir.Text = "";
            MskMaas.Text = "";
            TxtMeslek.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked=false;
            TxtAd.Focus();
        }

        //Tabloyu Listeleme
        private void BtnListele_Click(object sender, EventArgs e)
        {          
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel); 
        }

        //Personel Kayıt işlemi
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open(); //SqlConnection sınıfından oluşturulan baglantı nesnesi ile baglantıyı açma.

            SqlCommand komut = new SqlCommand("insert into Tbl_Personel (PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek,PerDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti); //SqlCommand sınıfından komut nesnesi üretme. sql komutu yazmak için.
            komut.Parameters.AddWithValue("@p1", TxtAd.Text); //@p1 parametresine TxtAd.Text değerini atama.
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", CmbSehir.Text);
            komut.Parameters.AddWithValue("@p4", MskMaas.Text);
            komut.Parameters.AddWithValue("@p5", TxtMeslek.Text);
            komut.Parameters.AddWithValue("@p6", label6.Text);
            komut.ExecuteNonQuery(); //sorguyu olduğu gibi çalıştırmak için kullanılır. tablo sonucunda etkilenme olduğunda kullanılır. ekleme,silme,
            MessageBox.Show("Personel eklendi"); //bilgilendirme mesajı

            baglanti.Close(); //bağlantıyı kapatma.
        }

        //radiobutton işaretine göre label6 durumu
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                label6.Text = "True";
            }
        }

        //radiobutton işaretine göre label6 durumu
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked==true)
            {
                label6.Text = "False";
            }          
        }

        //Temizleme Fonksiyonu Çağırma
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        //datagrid'den verileri araçlara taşıma
        //datagrid olaylardan celldoubleclick kullanımı
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbSehir.Text=dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskMaas.Text= dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label6.Text= dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtMeslek.Text= dataGridView1.Rows[secilen].Cells[6].Value.ToString();

        }

        //label6 değiştiğinde radiobutton durumunu değiştirme
        //label6 olaylardan textchange kullanımı
        private void label6_TextChanged(object sender, EventArgs e)
        {
            if(label6.Text=="True")
            {
                radioButton1.Checked = true;
            }
            if(label6.Text=="False")
            {
                radioButton2.Checked = true;
            }

        }

        //Personel Kayıt Silme
        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komutsil = new SqlCommand("Delete From Tbl_Personel Where Perid=@k1",baglanti);
            komutsil.Parameters.AddWithValue("@k1",Txtid.Text);
            komutsil.ExecuteNonQuery();
            MessageBox.Show("kayıt silindi");

            baglanti.Close();
        }

        //Personel Kayıt Güncelleme 
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("Update Tbl_Personel Set PerAd=@a1,PerSoyad=@a2,PerSehir=@a3,PerMaas=@a4,PerDurum=@a5,PerMeslek=@a6 where Perid=@a7",baglanti);
            komutguncelle.Parameters.AddWithValue("@a1",TxtAd.Text);
            komutguncelle.Parameters.AddWithValue("@a2", TxtSoyad.Text);
            komutguncelle.Parameters.AddWithValue("@a3", CmbSehir.Text);
            komutguncelle.Parameters.AddWithValue("@a4", MskMaas.Text);
            komutguncelle.Parameters.AddWithValue("@a5", label6.Text);
            komutguncelle.Parameters.AddWithValue("@a6", TxtMeslek.Text);
            komutguncelle.Parameters.AddWithValue("@a7", Txtid.Text);
            komutguncelle.ExecuteNonQuery();
            MessageBox.Show("Personel Bilgileri Güncellendi ");
            baglanti.Close();
        }

        //İstatistik Formu Açma
        private void Btnİstatistik_Click(object sender, EventArgs e)
        {
            Frmİstatistik fr2=new Frmİstatistik();
            fr2.Show();

        }

        //Grafikler Formu Açma
        private void BtnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler fr3=new FrmGrafikler();
            fr3.Show();
        }

        //Raporlar Formu Açma
        private void button1_Click(object sender, EventArgs e)
        {
            FrmRaporlar frp5=new FrmRaporlar();
            frp5.Show();
        }
    }
}
