using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace OtobüsBilet
{
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BiletAlmaEkrani bilet = new OtobüsBilet.BiletAlmaEkrani();
            bilet.ShowDialog();
        }
        DataBase db = new DataBase();
        OleDbCommand komut;
        OleDbDataReader da;
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                komut = new OleDbCommand();
                db.baglanti.Open();
                komut.Connection = db.baglanti;
                komut.CommandText = "select * from Personel where TC_Kimlik_No='" + txtKadi.Text + "'and Sifre='" + txtPass.Text + "'";
                da = komut.ExecuteReader();
                if (da.Read())
                {
                    MessageBox.Show("Giriş Başarılı.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panel1.Visible = false;
                    pictureBox1.Visible = false;
                    label1.Visible = false;
                    txtKadi.Visible = false;
                    txtPass.Visible = false;
                    button5.Visible = false;
                    btnEkle.Visible = false;
                    button1.Enabled = true;
                    btnSeferler.Enabled = true;
                    btnMusteriler.Enabled = true;
                    btnBiletler.Enabled = true;
                    btnGeri.Visible = true;
                    txtKadi.Text = "";
                    txtPass.Text = "";
                }
                else
                {
                    MessageBox.Show("Giriş Başarısız." + "\n" + "Lüften Tekrar Deneyiniz..", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult s = MessageBox.Show("Bu işleme devam edebilmek için yönetici girişi yapmalısınız." + "\n" + "Yönetici giriş ekranına gitmek istiyor musunuz ?", "Malumatcı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (s==DialogResult.Yes)
            {
                MessageBox.Show("Yönetici giriş ekranına aktarılıyorsunuz..", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YoneticiGiris yonetici = new YoneticiGiris();
                yonetici.Show();
            }
            
        }

        private void GirisEkrani_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            btnSeferler.Enabled = false;
            btnMusteriler.Enabled = false;
            btnBiletler.Enabled = false;
            
        }

        private void btnMusteriler_Click(object sender, EventArgs e)
        {
            MusteriEkrani musteri = new OtobüsBilet.MusteriEkrani();
            musteri.Show();
        }

        private void btnBiletler_Click(object sender, EventArgs e)
        {
            Biletler b = new OtobüsBilet.Biletler();
            b.Show();
        }

        private void btnSeferler_Click(object sender, EventArgs e)
        {
            Seferler sefer = new OtobüsBilet.Seferler();
            sefer.Show();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            pictureBox1.Visible = true;
            label1.Visible = true;
            txtKadi.Visible = true;
            txtPass.Visible = true;
            button5.Visible = true;
            btnEkle.Visible = true;
            button1.Enabled = false;
            btnSeferler.Enabled = false;
            btnMusteriler.Enabled = false;
            btnBiletler.Enabled = false;
            btnGeri.Visible = false;
        }
    }
}
