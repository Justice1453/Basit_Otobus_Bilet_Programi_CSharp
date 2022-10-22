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
    public partial class Biletler : Form
    {
        public Biletler()
        {
            InitializeComponent();
        }
        DataBase db = new DataBase();
        OleDbCommand komut;
        OleDbDataAdapter da;
        DataSet ds;

        void doldur()
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                da = new OleDbDataAdapter("select * from Yolcular", db.baglanti);
                ds = new DataSet();
                da.Fill(ds, "Yolcular");
                dataGridView1.DataSource = ds.Tables["Yolcular"];
                dataGridView1.Columns[0].Visible = false;
                db.baglanti.Close();
            }
            catch { }
        }

        private void Biletler_Load(object sender, EventArgs e)
        {
            doldur();
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                ////////////////////// Verileri textBox ve comboBox'a aktarma işlemi.
                lblID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cmbİsim.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                cmbSoyisim.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtTel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                cmbNereden.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                cmbNereye.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                cmbFirma.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtPlaka.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                lblKoltuk.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                lblTutar.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                dtTarih.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
                lblY.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                //////////////////////
                if (lblY.Text == "Çift Yön")
                {
                    rCift.Enabled = false;
                    if (rTek.Checked == true)
                    { rTek.Checked = false; }
                    if (rTek.Enabled == false)
                    { rTek.Enabled = true; }
                }
                if (lblY.Text == "Tek Yön")
                {
                    rTek.Enabled = false;
                    if (rCift.Checked == true)
                    { rCift.Checked = false; }
                    if (rCift.Enabled == false)
                    { rCift.Enabled = true; }
                }
                btnGuncelle.Enabled = true;
                btnSil.Enabled = true;
                ////////////////////// Aktarılan verileri bilet ekranına aktarma işlemi.
                BiletCiktisi bilet = new BiletCiktisi();
                bilet.label4.Text = cmbİsim.Text;
                bilet.label5.Text = cmbSoyisim.Text;
                bilet.label11.Text = dtTarih.Text;
                bilet.label12.Text = lblTutar.Text;
                bilet.label13.Text = cmbFirma.Text;
                bilet.label14.Text = cmbNereye.Text;
                bilet.label19.Text = txtPlaka.Text;
                bilet.label20.Text = lblKoltuk.Text;
                bilet.label22.Text = lblY.Text;
                bilet.Show();
            }
            catch { }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult soru = MessageBox.Show("Dikkat! | Bilet bilgilerini güncellemek istediğinize emin misiniz ?", "Malumatcı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(soru==DialogResult.Yes)
                {
                    if (db.baglanti.State == ConnectionState.Open)
                    {
                        db.baglanti.Close();
                    }
                    db.baglanti.Open();
                    komut = new OleDbCommand("update Yolcular set Adi=@adi, Soyadi=@soyadi, Telefon=@telefon, Nereden=@nereden, Nereye=@nereye, Firma=@firma, Plaka=@plaka, Koltuk_No=@koltuk, Tutar=@tutar, Tarih=@tarih, Yön=@yon where ID=@id", db.baglanti);
                    komut.Parameters.AddWithValue("@ad", cmbİsim.Text);
                    komut.Parameters.AddWithValue("@soyadi", cmbSoyisim.Text);
                    komut.Parameters.AddWithValue("@telefon", txtTel.Text);
                    komut.Parameters.AddWithValue("@nereden", cmbNereden.Text);
                    komut.Parameters.AddWithValue("@nereye", cmbNereye.Text);
                    komut.Parameters.AddWithValue("@firma", cmbFirma.Text);
                    komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
                    komut.Parameters.AddWithValue("@koltuk", lblKoltuk.Text);
                    komut.Parameters.AddWithValue("@tutar", lblTutar.Text);
                    komut.Parameters.AddWithValue("@tarih", Convert.ToString(dtTarih.Value));
                    komut.Parameters.AddWithValue("@yon", lblY.Text);
                    komut.Parameters.AddWithValue("@id", lblID.Text);
                    komut.ExecuteNonQuery();
                    db.baglanti.Close();
                    MessageBox.Show("Bilet Bilgileri Güncelledi.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    Biletler bilet = new Biletler();
                    bilet.Show();
                }
                
            }
            catch (Exception s) { MessageBox.Show("" + s); }
        }

        private void rTek_CheckedChanged(object sender, EventArgs e)
        {
            lblY.Text = rTek.Text;
            double tutar = Convert.ToDouble(lblTutar.Text);
            double bol = tutar / 2;
            lblTutar.Text = "";
            lblTutar.Text += bol;
        }

        private void rCift_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                double tutar = Convert.ToDouble(lblTutar.Text);
                double toplam = tutar + tutar;
                lblTutar.Text = "";
                lblTutar.Text += toplam;
                lblY.Text = rCift.Text;
            }
            catch { }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult soru = MessageBox.Show("Dikkat! | Bileti iptal etmek istediğinize emin misiniz ?", "Malumatcı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (soru == DialogResult.Yes)
                {
                    if (db.baglanti.State == ConnectionState.Open)
                    {
                        db.baglanti.Close();
                    }
                    db.baglanti.Open();
                    komut = new OleDbCommand("delete from Yolcular where ID=@id", db.baglanti);
                    komut.Parameters.AddWithValue("@id", lblID.Text);
                    komut.ExecuteNonQuery();
                    db.baglanti.Close();
                    MessageBox.Show("Bilet İptal Edildi.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    doldur();
                }

            }
            catch { }

        }

        private void btnBilet_Click(object sender, EventArgs e)
        {
            BiletCiktisi b = new OtobüsBilet.BiletCiktisi();
            b.Show();
        }
    }
}
