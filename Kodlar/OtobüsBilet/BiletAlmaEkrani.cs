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
using System.Collections;

namespace OtobüsBilet
{
    public partial class BiletAlmaEkrani : Form
    {
        DataBase db = new DataBase();
        OleDbDataAdapter da;
        OleDbCommand komut;
        DataSet ds;

        public BiletAlmaEkrani()
        {
            InitializeComponent();


        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                db.baglanti.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from MusteriBilgileri ORDER BY ID ASC", db.baglanti);
                da.Fill(dt);
                // İsimleri ComboBox'a aktarma işlemi
                cmbİsim.ValueMember = "ID";
                cmbİsim.DisplayMember = "Adi";
                cmbİsim.DataSource = dt;
                // Soyisimleri ComboBox'a aktarma işlemi
                cmbSoyisim.ValueMember = "ID";
                cmbSoyisim.DisplayMember = "Soyadi";
                cmbSoyisim.DataSource = dt;
            }
            catch { }
            finally
            {
                db.baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                if (lblY.Text == "" || cmbİsim.Text == "" || cmbSoyisim.Text == "" || txtTel.Text == "" || cmbNereden.Text == "" || cmbNereye.Text == "" || lblFirma.Text == "" || cmbPlaka.Text == "" || cmbKoltuk.Text == "" || lblTutar.Text == "")
                {
                    MessageBox.Show("Hata | Eksik bilgi girdiniz!", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    db.baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("insert into Yolcular(Adi, Soyadi, Telefon, Nereden, Nereye, Firma, Plaka, Koltuk_No, Tutar, Tarih, Yön) values(@adi, @soyadi, @telefon, @nereden, @nereye, @firma, @plaka, @koltuk, @tutar, @tarih ,@yon)", db.baglanti);
                    komut.Parameters.AddWithValue("@ad", cmbİsim.Text);
                    komut.Parameters.AddWithValue("@soyadi", cmbSoyisim.Text);
                    komut.Parameters.AddWithValue("@telefon", txtTel.Text);
                    komut.Parameters.AddWithValue("@nereden", cmbNereden.Text);
                    komut.Parameters.AddWithValue("@nereye", cmbNereye.Text);
                    komut.Parameters.AddWithValue("@firma", lblFirma.Text);
                    komut.Parameters.AddWithValue("@plaka", cmbPlaka.Text);
                    komut.Parameters.AddWithValue("@koltuk", cmbKoltuk.Text);
                    komut.Parameters.AddWithValue("@tutar", lblTutar.Text);
                    komut.Parameters.AddWithValue("@tarih", Convert.ToString(dtTarih.Value));
                    komut.Parameters.AddWithValue("@yon", lblY.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Yolcu Başarıyla Kaydedildi.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //////
                    BiletCiktisi bilet = new OtobüsBilet.BiletCiktisi();
                    bilet.Show();
                    bilet.label4.Text = cmbİsim.Text;
                    bilet.label5.Text = cmbSoyisim.Text;
                    bilet.label11.Text = dtTarih.Text;
                    bilet.label12.Text = lblTutar.Text;
                    bilet.label13.Text = lblFirma.Text;
                    bilet.label14.Text = cmbNereye.Text;
                    bilet.label19.Text = cmbPlaka.Text;
                    bilet.label20.Text = cmbKoltuk.Text;
                    bilet.label22.Text = lblY.Text;
                    //////

                    //////
                }

            }
            catch (Exception hata) { MessageBox.Show("hata" + hata); if (cmbİsim.Text == string.Empty || cmbSoyisim.Text == string.Empty || txtTel.Text == string.Empty || lblFirma.Text == string.Empty || cmbNereden.Text == string.Empty || cmbNereye.Text == string.Empty) { MessageBox.Show("DİKKAT!" + "\n" + "Alanlar boş bırakılamaz!", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Warning); } }
            finally
            {
                db.baglanti.Close();
            }
        }

        private void btnKamilKoc_Click(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                db.baglanti.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Seferler where Firma='KAMİL KOÇ'", db.baglanti);
                da.Fill(dt);
                // Kalkış yerini ComboBox'a aktarma işlemi.
                cmbNereden.ValueMember = "Firma";
                cmbNereden.DisplayMember = "Nereden";
                cmbNereden.DataSource = dt;
                // İstikamet'i ComboBox'a aktarma işlemi.
                cmbNereye.ValueMember = "Firma";
                cmbNereye.DisplayMember = "Nereye";
                cmbNereye.DataSource = dt;
                // Plaka'yı ComboBox'a aktarma işlemi.
                cmbPlaka.ValueMember = "Firma";
                cmbPlaka.DisplayMember = "Plaka";
                cmbPlaka.DataSource = dt;
                // Arac'ı ComboBox'a aktarma işlemi.
                cmbArac.ValueMember = "Firma";
                cmbArac.DisplayMember = "Arac";
                cmbArac.DataSource = dt;
                db.baglanti.Close();

                db.baglanti.Open();
                cmbKoltuk.Items.Clear();
                DataTable dg = new DataTable();
                OleDbDataAdapter daa = new OleDbDataAdapter("select * from Araclar where Marka_Model='"+cmbArac.Text+"'",db.baglanti);
                daa.Fill(dg);
                cmbKoltuks.ValueMember = "Marka_Model";
                cmbKoltuks.DisplayMember = "Kapasite";
                cmbKoltuks.DataSource = dg;
                db.baglanti.Close();

                if (cmbKoltuk.Items.Count != 0)
                {
                    cmbKoltuk.Items.Clear();
                }

                int sayi = Convert.ToInt32(cmbKoltuks.Text);

                if (cmbKoltuk.Items.Count == 0)
                {
                    for (int i = 1; i <= sayi; i++)
                    {
                        cmbKoltuk.Items.Add(i);
                    }
                }

                double kdv = 0;

                lblFirma.Text = "";
                lblFirma.Text = btnKamilKoc.Text;

                if (lblFirma.Text == "KAMİL KOÇ")
                {
                    kdv = 350 * 0.08;
                    lblTutar.Text = "";
                    lblTutar.Text += 350 + kdv;
                    btnKamilKoc.BackColor = Color.DarkOrange;
                }
                if (rCift.Checked)
                {
                    double tutar = Convert.ToDouble(lblTutar.Text);
                    double toplam = tutar + tutar;
                    lblTutar.Text = "";
                    lblTutar.Text += toplam;
                }
            }
            catch { }

        }



        private void btnMetro_Click(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {

                db.baglanti.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Seferler where Firma='METRO TURİZM'", db.baglanti);
                da.Fill(dt);
                // İsimleri ComboBox'a aktarma işlemi
                cmbNereden.ValueMember = "Firma";
                cmbNereden.DisplayMember = "Nereden";
                cmbNereden.DataSource = dt;
                // Soyisimleri ComboBox'a aktarma işlemi
                cmbNereye.ValueMember = "Firma";
                cmbNereye.DisplayMember = "Nereye";
                cmbNereye.DataSource = dt;
                // Plaka'yı ComboBox'a aktarma
                cmbPlaka.ValueMember = "Firma";
                cmbPlaka.DisplayMember = "Plaka";
                cmbPlaka.DataSource = dt;
                // Arac'ı ComboBox'a aktarma işlemi.
                cmbArac.ValueMember = "Firma";
                cmbArac.DisplayMember = "Arac";
                cmbArac.DataSource = dt;
                db.baglanti.Close();

                db.baglanti.Open();
                cmbKoltuk.Items.Clear();
                DataTable dg = new DataTable();
                OleDbDataAdapter daa = new OleDbDataAdapter("select * from Araclar where Marka_Model='" + cmbArac.Text + "'", db.baglanti);
                daa.Fill(dg);
                cmbKoltuks.ValueMember = "Marka_Model";
                cmbKoltuks.DisplayMember = "Kapasite";
                cmbKoltuks.DataSource = dg;
                db.baglanti.Close();

                if (cmbKoltuk.Items.Count != 0)
                {
                    cmbKoltuk.Items.Clear();
                }

                int sayi = Convert.ToInt32(cmbKoltuks.Text);

                if (cmbKoltuk.Items.Count == 0)
                {
                    
                    for (int i = 1; i <= sayi; i++)
                    {
                        cmbKoltuk.Items.Add(i);
                    }
                }

                double kdv = 0;

                lblFirma.Text = "";
                lblFirma.Text = btnMetro.Text;
                if (lblFirma.Text == "METRO TURİZM")
                {
                    kdv = 300 * 0.08;
                    lblTutar.Text = "";
                    lblTutar.Text += 300 + kdv;
                    btnMetro.BackColor = Color.DarkOrange;
                }
                if (rCift.Checked)
                {
                    double tutar = Convert.ToDouble(lblTutar.Text);
                    double toplam = tutar + tutar;
                    lblTutar.Text = "";
                    lblTutar.Text += toplam;
                }
            }
            catch { }

        }

        private void btnUlusoy_Click(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                db.baglanti.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Seferler where Firma='ULUSOY TURİZM'", db.baglanti);
                da.Fill(dt);
                // Kalkış yerlerini ComboBox'a aktarma işlemi
                cmbNereden.ValueMember = "Firma";
                cmbNereden.DisplayMember = "Nereden";
                cmbNereden.DataSource = dt;
                // İStikamet yerlerini ComboBox'a aktarma işlemi
                cmbNereye.ValueMember = "Firma";
                cmbNereye.DisplayMember = "Nereye";
                cmbNereye.DataSource = dt;
                // Plaka'yı ComboBox'a aktarma
                cmbPlaka.ValueMember = "Firma";
                cmbPlaka.DisplayMember = "Plaka";
                cmbPlaka.DataSource = dt;
                // Arac'ı ComboBox'a aktarma işlemi.
                cmbArac.ValueMember = "Firma";
                cmbArac.DisplayMember = "Arac";
                cmbArac.DataSource = dt;
                db.baglanti.Close();

                db.baglanti.Open();
                cmbKoltuk.Items.Clear();
                DataTable dg = new DataTable();
                OleDbDataAdapter daa = new OleDbDataAdapter("select * from Araclar where Marka_Model='" + cmbArac.Text + "'", db.baglanti);
                daa.Fill(dg);
                cmbKoltuks.ValueMember = "Marka_Model";
                cmbKoltuks.DisplayMember = "Kapasite";
                cmbKoltuks.DataSource = dg;
                db.baglanti.Close();

                if (cmbKoltuk.Items.Count != 0)
                {
                    cmbKoltuk.Items.Clear();
                }

                int sayi = Convert.ToInt32(cmbKoltuks.Text);

                if (cmbKoltuk.Items.Count == 0)
                {
                    
                    for (int i = 1; i <= sayi; i++)
                    {
                        cmbKoltuk.Items.Add(i);
                    }
                }

                double kdv = 0;
                db.baglanti.Close();
                lblFirma.Text = "";
                lblFirma.Text = btnUlusoy.Text;

                if (lblFirma.Text == "ULUSOY TURİZM")
                {
                    kdv = 295 * 0.08;
                    lblTutar.Text = "";
                    lblTutar.Text += 295 + kdv;
                    btnUlusoy.BackColor = Color.DarkOrange;
                }
                if (rCift.Checked)
                {
                    double tutar = Convert.ToDouble(lblTutar.Text);
                    double toplam = tutar + tutar;
                    lblTutar.Text = "";
                    lblTutar.Text += toplam;
                }
            }
            catch { }
        }

        private void btnNilüfer_Click(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                db.baglanti.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Seferler where Firma='NİLÜFER TURİZM'", db.baglanti);
                da.Fill(dt);
                // İsimleri ComboBox'a aktarma işlemi
                cmbNereden.ValueMember = "Firma";
                cmbNereden.DisplayMember = "Nereden";
                cmbNereden.DataSource = dt;
                // Soyisimleri ComboBox'a aktarma işlemi
                cmbNereye.ValueMember = "Firma";
                cmbNereye.DisplayMember = "Nereye";
                cmbNereye.DataSource = dt;
                // Plaka'yı ComboBox'a aktarma
                cmbPlaka.ValueMember = "Firma";
                cmbPlaka.DisplayMember = "Plaka";
                cmbPlaka.DataSource = dt;
                // Arac'ı ComboBox'a aktarma işlemi.
                cmbArac.ValueMember = "Firma";
                cmbArac.DisplayMember = "Arac";
                cmbArac.DataSource = dt;
                db.baglanti.Close();

                db.baglanti.Open();
                DataTable dg = new DataTable();
                OleDbDataAdapter daa = new OleDbDataAdapter("select * from Araclar where Marka_Model='" + cmbArac.Text + "'", db.baglanti);
                daa.Fill(dg);
                cmbKoltuks.ValueMember = "Marka_Model";
                cmbKoltuks.DisplayMember = "Kapasite";
                cmbKoltuks.DataSource = dg;
                db.baglanti.Close();

                if (cmbKoltuk.Items.Count != 0)
                {
                    cmbKoltuk.Items.Clear();
                }

                int sayi = Convert.ToInt32(cmbKoltuks.Text);

                if (cmbKoltuk.Items.Count == 0)
                {
                    for (int i = 1; i <= sayi; i++)
                    {
                        cmbKoltuk.Items.Add(i);
                    }
                }

                double kdv = 0;
                lblFirma.Text = "";
                lblFirma.Text = btnNilüfer.Text;

                if (lblFirma.Text == "NİLÜFER TURİZM")
                {
                    kdv = 150 * 0.08;
                    lblTutar.Text = "";
                    lblTutar.Text += 150 + kdv;
                    btnNilüfer.BackColor = Color.DarkOrange;
                }
                if (rCift.Checked)
                {
                    double tutar = Convert.ToDouble(lblTutar.Text);
                    double toplam = tutar + tutar;
                    lblTutar.Text = "";
                    lblTutar.Text += toplam;
                }
            }
            catch { }
        }

        private void btnVaran_Click(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                db.baglanti.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Seferler where Firma='VARAN TURİZM'", db.baglanti);
                da.Fill(dt);
                // İsimleri ComboBox'a aktarma işlemi
                cmbNereden.ValueMember = "Firma";
                cmbNereden.DisplayMember = "Nereden";
                cmbNereden.DataSource = dt;
                // Soyisimleri ComboBox'a aktarma işlemi
                cmbNereye.ValueMember = "Firma";
                cmbNereye.DisplayMember = "Nereye";
                cmbNereye.DataSource = dt;
                // Plaka'yı ComboBox'a aktarma
                cmbPlaka.ValueMember = "Firma";
                cmbPlaka.DisplayMember = "Plaka";
                cmbPlaka.DataSource = dt;
                // Arac'ı ComboBox'a aktarma işlemi.
                cmbArac.ValueMember = "Firma";
                cmbArac.DisplayMember = "Arac";
                cmbArac.DataSource = dt;
                db.baglanti.Close();

                db.baglanti.Open();
                DataTable dg = new DataTable();
                OleDbDataAdapter daa = new OleDbDataAdapter("select * from Araclar where Marka_Model='" + cmbArac.Text + "'", db.baglanti);
                daa.Fill(dg);
                cmbKoltuks.ValueMember = "Marka_Model";
                cmbKoltuks.DisplayMember = "Kapasite";
                cmbKoltuks.DataSource = dg;
                db.baglanti.Close();

                if (cmbKoltuk.Items.Count != 0)
                {
                    cmbKoltuk.Items.Clear();
                }

                int sayi = Convert.ToInt32(cmbKoltuks.Text);

                if (cmbKoltuk.Items.Count == 0)
                {
                    for (int i = 1; i <= sayi; i++)
                    {
                        cmbKoltuk.Items.Add(i);
                    }
                }

                double kdv = 0;
                lblFirma.Text = "";
                lblFirma.Text = btnVaran.Text;

                if (lblFirma.Text == "VARAN TURİZM")
                {
                    kdv = 100 * 0.08;
                    lblTutar.Text = "";
                    lblTutar.Text += 100 + kdv;
                    btnVaran.BackColor = Color.DarkOrange;
                }
                if (rCift.Checked)
                {
                    double tutar = Convert.ToDouble(lblTutar.Text);
                    double toplam = tutar + tutar;
                    lblTutar.Text = "";
                    lblTutar.Text += toplam;
                }
            }
            catch { }
        }

        private void btnYigido_Click(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                db.baglanti.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Seferler where Firma='YİĞİDO TURİZM'", db.baglanti);
                da.Fill(dt);
                // İsimleri ComboBox'a aktarma işlemi
                cmbNereden.ValueMember = "Firma";
                cmbNereden.DisplayMember = "Nereden";
                cmbNereden.DataSource = dt;
                // Soyisimleri ComboBox'a aktarma işlemi
                cmbNereye.ValueMember = "Firma";
                cmbNereye.DisplayMember = "Nereye";
                cmbNereye.DataSource = dt;
                // Plaka'yı ComboBox'a aktarma
                cmbPlaka.ValueMember = "Firma";
                cmbPlaka.DisplayMember = "Plaka";
                cmbPlaka.DataSource = dt;
                // Arac'ı ComboBox'a aktarma işlemi.
                cmbArac.ValueMember = "Firma";
                cmbArac.DisplayMember = "Arac";
                cmbArac.DataSource = dt;
                db.baglanti.Close();

                db.baglanti.Open();
                DataTable dg = new DataTable();
                OleDbDataAdapter daa = new OleDbDataAdapter("select * from Araclar where Marka_Model='" + cmbArac.Text + "'", db.baglanti);
                daa.Fill(dg);
                cmbKoltuks.ValueMember = "Marka_Model";
                cmbKoltuks.DisplayMember = "Kapasite";
                cmbKoltuks.DataSource = dg;
                db.baglanti.Close();

                if (cmbKoltuk.Items.Count != 0)
                {
                    cmbKoltuk.Items.Clear();
                }

                int sayi = Convert.ToInt32(cmbKoltuks.Text);
                
                if (cmbKoltuk.Items.Count==0)
                {
                    for (int i = 1; i <= sayi; i++)
                    {
                        cmbKoltuk.Items.Add(i);
                    }
                }
                

                double kdv = 0;
                lblFirma.Text = "";
                lblFirma.Text = btnYigido.Text;

                if (lblFirma.Text == "YİĞİDO TURİZM")
                {
                    kdv = 258 * 0.08;
                    lblTutar.Text = "";
                    lblTutar.Text += 258 + kdv;
                    btnYigido.BackColor = Color.DarkOrange;
                }
                if (rCift.Checked)
                {
                    double tutar = Convert.ToDouble(lblTutar.Text);
                    double toplam = tutar + tutar;
                    lblTutar.Text = "";
                    lblTutar.Text += toplam;
                }
            }
            catch { }
        }

        private void lblFirma_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //////
            cmbİsim.Text = string.Empty; cmbİsim.Text = string.Empty; txtTel.Text = string.Empty; lblFirma.Text = string.Empty; cmbNereden.Text = string.Empty; cmbNereye.Text = string.Empty;
            lblFirma.Text = "";
            lblTutar.Text = "";
            btnKamilKoc.BackColor = Color.White;
            btnMetro.BackColor = Color.White;
            btnNilüfer.BackColor = Color.White;
            btnUlusoy.BackColor = Color.White;
            btnVaran.BackColor = Color.White;
            btnYigido.BackColor = Color.White;
            rTek.Checked = false;
            rCift.Checked = false;
            rTek.Enabled = true;
            rCift.Enabled = true;
            lblY.Text = "";
        }

        private void rCift_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                double tutar = Convert.ToDouble(lblTutar.Text);
                double toplam = tutar + tutar;
                lblTutar.Text = "";
                lblTutar.Text += toplam;
                rTek.Enabled = false;
                lblY.Text = "";
                lblY.Text = rCift.Text;
            }
            catch { }

        }

        private void rTek_CheckedChanged(object sender, EventArgs e)
        {
            lblY.Text = "";
            lblY.Text = rTek.Text;
            rCift.Enabled = false;
        }

        private void cmbArac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlaka.Text == "Tourismo")
            {
                Random koltuk = new Random();
                int sayi = koltuk.Next(1, 37);
                cmbKoltuk.Text = sayi.ToString();
            }
            if (cmbPlaka.Text == "Travego")
            {
                Random koltuk = new Random();
                int sayi = koltuk.Next(1, 46);
                cmbKoltuk.Text = sayi.ToString();
            }
            if (cmbPlaka.Text == "Setra")
            {
                Random koltuk = new Random();
                int sayi = koltuk.Next(1, 46);
                cmbKoltuk.Text = sayi.ToString();
            }
            if (cmbPlaka.Text == "Man")
            {
                Random koltuk = new Random();
                int sayi = koltuk.Next(1, 38);
                cmbKoltuk.Text = sayi.ToString();
            }
            if (cmbPlaka.Text == "Neoplan")
            {
                Random koltuk = new Random();
                int sayi = koltuk.Next(1, 53);
                cmbKoltuk.Text = sayi.ToString();
            }
        }

        private void cmbSoyisim_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbİsim_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnBilet_Click(object sender, EventArgs e)
        {
            BiletCiktisi bilet = new OtobüsBilet.BiletCiktisi();
            bilet.Show();
        }
    }
}
