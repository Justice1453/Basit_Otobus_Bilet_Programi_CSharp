using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtobüsBilet
{
    public partial class YoneticiGiris : Form
    {
        public YoneticiGiris()
        {
            InitializeComponent();
        }
        GirisEkrani g = new OtobüsBilet.GirisEkrani();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKadi.Text == "Admin" && txtPass.Text == "admin")
                {
                    MessageBox.Show("Giriş Başarılı.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PersonelKayit personel = new OtobüsBilet.PersonelKayit();
                    personel.Show();
                    this.Close();
                }
                else if (txtKadi.Text == string.Empty || txtPass.Text == string.Empty)
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre alanı boş bırakılamaz.","Malumatcı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre tekrar deneyiniz", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                int sayi = Convert.ToInt16(lblSayi.Text);
                sayi+= +1;
                lblSayi.Text = sayi.ToString();
                if (sayi == 3)
                {
                    MessageBox.Show("Deneme haklarınız bitti."+"\n"+"Tekrar denemek için programı kapatıp açınız.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtKadi.Enabled = false;
                    txtPass.Enabled = false;
                    button1.Enabled = false;
                    Application.Exit();
                }
            }
            catch { }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void YoneticiGiris_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Giriş için 3 defa yanlış giriş yapma hakkınız var.","Malumatcı",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            if (txtKadi.Enabled==false || txtPass.Enabled==false)
            {
                txtKadi.Enabled = true;
                txtPass.Enabled = true;
            }
        }
    }
}
