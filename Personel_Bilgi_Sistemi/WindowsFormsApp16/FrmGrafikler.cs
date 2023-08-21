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
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }

        //veri tabanı bağlantısı

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KQHA9KT;Initial Catalog=PersonelVeriTabani;Integrated Security=True");
        
        //Form yüklendiğinde grafik verilerini gösterme 
        private void Form3_Load(object sender, EventArgs e)
        {
            //şehirlerde kişi sayılarını gösteren grafik
            baglanti.Open();
            SqlCommand komutg1 = new SqlCommand("Select PerSehir,Count(*) From Tbl_Personel Group By PerSehir",baglanti);
            SqlDataReader dr1 = komutg1.ExecuteReader();
            while(dr1.Read())
            {
                chart1.Series["Şehirler-Kişi"].Points.AddXY(dr1[0], dr1[1]);
            }
            baglanti.Close();

            //mesleklere ait ortalama maaşı gösteren grafik
            baglanti.Open();
            SqlCommand komutg2 = new SqlCommand("Select PerMeslek,Avg(PerMaas) From Tbl_Personel group by PerMeslek",baglanti);
            SqlDataReader dr2 = komutg2.ExecuteReader();
            while(dr2.Read())
            {
                chart2.Series["Meslek-Maaş"].Points.AddXY(dr2[0], dr2[1]);
            }
            baglanti.Close();
        }
    }
}
