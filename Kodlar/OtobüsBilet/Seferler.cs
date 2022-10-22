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
    public partial class Seferler : Form
    {
        public Seferler()
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
                da = new OleDbDataAdapter("select * from Seferler", db.baglanti);
                ds = new DataSet();
                da.Fill(ds, "Seferler");
                dataGridView1.DataSource = ds.Tables["Seferler"];
                dataGridView1.Columns[0].Visible = false;
                db.baglanti.Close();
            }
            catch { }
        }

        void sayi()
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                komut = new OleDbCommand("select count(*) from Yolcular where Plaka=@plaka", db.baglanti);
                komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
                lblYolcu.Text = komut.ExecuteScalar().ToString();
            }
            catch { }
        }

        private void Seferler_Load(object sender, EventArgs e)
        {
            doldur();
            cmbFirma.SelectedIndex = 0;
            cmbArac.SelectedIndex = 0;
            cmbNereden.SelectedIndex = 0;
            cmbNereye.SelectedIndex = 0;
            txtPlaka.Text = "00 ABC 0123";
            txtPlaka.ForeColor = Color.Gray;

            
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cmbFirma.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                cmbArac.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtPlaka.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                cmbNereye.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                cmbNereden.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                dtTarih.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                lblYolcu.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                //////
                txtPlaka.ForeColor = Color.Black;
                sayi();
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                komut = new OleDbCommand("update Seferler set Yolcu=@yolcu where ID=@id", db.baglanti);
                komut.Parameters.AddWithValue("@yolcu", lblYolcu.Text);
                komut.Parameters.AddWithValue("@id",lblID.Text);
                komut.ExecuteNonQuery();
                db.baglanti.Close();
                if (btnRefresh.Enabled == false)
                {
                    btnRefresh.Enabled = true;
                }
                btnUpd.Enabled = true;
                btnIptal.Enabled = true;
            }
            catch { }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if(cmbFirma.Text=="" || cmbArac.Text == "" || cmbNereden.Text=="" || cmbNereye.Text=="" || txtPlaka.Text=="")
                {
                    MessageBox.Show("Hata | Hiçbir alan boş geçilemez.","Malumatcı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                komut = new OleDbCommand("insert into Seferler(Firma, Arac, Plaka, Nereden, Nereye, Tarih, Yolcu) values(@firma, @arac, @plaka, @nereden, @nereye, @tarih, @yolcu)", db.baglanti);
                komut.Parameters.AddWithValue("@firma", cmbFirma.Text);
                komut.Parameters.AddWithValue("@arac", cmbArac.Text);
                komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
                komut.Parameters.AddWithValue("@nereden", cmbNereden.Text);
                komut.Parameters.AddWithValue("@nereye", cmbNereye.Text);
                komut.Parameters.AddWithValue("@tarih", Convert.ToString(dtTarih.Value));
                komut.Parameters.AddWithValue("@yolcu", lblYolcu.Text);
                komut.ExecuteNonQuery();
                db.baglanti.Close();
                MessageBox.Show("Sefer Eklendi.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                doldur();
                //Seferi ekledikten sonra alanları temizleme işlemi.
                cmbFirma.SelectedIndex = 0;
                cmbArac.SelectedIndex = 0;
                txtPlaka.Clear();
                cmbNereden.Text = "";
                cmbNereye.Text = "";
                dtTarih.Value = DateTime.Now;

            }
            catch { }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            db.baglanti.Open();
            komut = new OleDbCommand("delete from Seferler where ID=@id", db.baglanti);
            komut.Parameters.AddWithValue("@id", lblID.Text);
            komut.ExecuteNonQuery();
            db.baglanti.Close();
            MessageBox.Show("Sefer İptal Edildi.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            doldur();
        }

        private void cmbFirma_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbNereye_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            db.baglanti.Open();
            komut = new OleDbCommand("update Seferler set Firma=@firma, Arac=@arac, Plaka=@plaka, Nereden=@nereden, Nereye=@nereye, Tarih=@tarih, Yolcu=@yolcu where ID=@id", db.baglanti);
            komut.Parameters.AddWithValue("@firma", cmbFirma.Text);
            komut.Parameters.AddWithValue("@arac", cmbArac.Text);
            komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut.Parameters.AddWithValue("@nereden", cmbNereden.Text);
            komut.Parameters.AddWithValue("@nereye", cmbNereye.Text);
            komut.Parameters.AddWithValue("@tarih", Convert.ToString(dtTarih.Value));
            komut.Parameters.AddWithValue("@yolcu", lblYolcu.Text);
            komut.Parameters.AddWithValue("@id", lblID.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Sefer Bilgileri Güncellendi.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            db.baglanti.Close();
            doldur();
        }

        private void txtPlaka_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPlaka_Click(object sender, EventArgs e)
        {
            if (txtPlaka.Text == "00 ABC 0123")
            {
                txtPlaka.Clear();
                txtPlaka.ForeColor = Color.Black;
            }
        }

        private void cmbFirma_Click(object sender, EventArgs e)
        {
            try
            {

                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                DataTable dt = new DataTable();
                da = new OleDbDataAdapter("select * from Araclar ORDER BY ID ASC", db.baglanti);
                da.Fill(dt);
                // Markaları ComboBox'a aktarma işlemi.
                cmbArac.ValueMember = "ID";
                cmbArac.DisplayMember = "Marka_Model";
                cmbArac.DataSource = dt;
                db.baglanti.Close();

            }
            catch { }
        }

        private void cmbArac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbArac.Text == "Araç Ekle (+)")
            {
                AracEkle arac = new AracEkle();
                arac.Show();
            }
        }

        private void cmbFirma_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {

                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                DataTable dt = new DataTable();
                da = new OleDbDataAdapter("select * from Araclar ORDER BY ID ASC", db.baglanti);
                da.Fill(dt);
                // Markaları ComboBox'a aktarma işlemi.
                cmbArac.ValueMember = "ID";
                cmbArac.DisplayMember = "Marka_Model";
                cmbArac.DataSource = dt;
                db.baglanti.Close();

            }
            catch { }
        }

        private void pInfo_Click(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            tp.ToolTipTitle = "Bilgi";
            tp.ToolTipIcon = ToolTipIcon.Info; 
            tp.IsBalloon = true; // Balon'u aktif ediyoruz.

            tp.SetToolTip(pInfo, "Yolcu sayı bilgisini güncellemek istediğiniz sefer'e tıklayınız.");
        }

        private void pInfo_MouseMove(object sender, MouseEventArgs e)
        {
            ToolTip tp = new ToolTip();
            tp.ToolTipTitle = "| Bilgi |";
            tp.ToolTipIcon = ToolTipIcon.Info;
            tp.IsBalloon = true; // Balon'u aktif ediyoruz.

            tp.SetToolTip(pInfo, "Yolcu sayı bilgisini güncellemek istediğiniz sefer'e tıklayınız.");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            doldur();
            btnRefresh.Enabled = false;
        }
    }
}

