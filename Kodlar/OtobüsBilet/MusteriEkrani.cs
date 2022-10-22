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
    public partial class MusteriEkrani : Form
    {
        public MusteriEkrani()
        {
            InitializeComponent();
        }
        DataBase db = new DataBase();
        OleDbCommand komut;
        OleDbDataAdapter da;
        DataSet ds;

        void doldur()
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            db.baglanti.Open();
            da = new OleDbDataAdapter("select * from MusteriBilgileri", db.baglanti);
            ds = new DataSet();
            da.Fill(ds, "MusteriBilgileri");
            dataGridView1.DataSource = ds.Tables["MusteriBilgileri"];
            dataGridView1.Columns[0].Visible = false;
            db.baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            db.baglanti.Open();
            komut = new OleDbCommand("update MusteriBilgileri set Adi=@adi, Soyadi=@soyadi, Telefon_No=@tel, TC_Kimlik_No=@tc, Cinsiyet=@cinsiyet where ID=id",db.baglanti);
            komut.Parameters.AddWithValue("@adi", txtAdi.Text);
            komut.Parameters.AddWithValue("@soyadi", txtSoyadi.Text);
            komut.Parameters.AddWithValue("@tel", txtTel.Text);
            komut.Parameters.AddWithValue("@tc", txtTC.Text);
            komut.Parameters.AddWithValue("@cinsiyet", cmbCinsiyet.Text);
            komut.Parameters.AddWithValue("@id",lblID.Text);
            komut.ExecuteNonQuery();
            db.baglanti.Close();
            MessageBox.Show("Müşteri Bilgileri Güncelledi.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            doldur();
        }

        private void MusteriEkrani_Load(object sender, EventArgs e)
        {
            doldur();
            txtAdi.Text = "";
            txtSoyadi.Text = "";
            txtTel.Text = "";
            txtTC.Text = "";
            cmbCinsiyet.Text = "";

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                komut = new OleDbCommand("delete from MusteriBilgileri where ID=@id", db.baglanti);
                komut.Parameters.AddWithValue("@id", lblID.Text);
                komut.ExecuteNonQuery();
                db.baglanti.Close();
                MessageBox.Show("Müşteri Silindi.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                doldur();
            }
            catch (Exception s)
            {
                MessageBox.Show("Silme işlemi tamamlanamadı.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult cevap = MessageBox.Show("Hata'yı görmek için 'Evet'e tıkla.", "Malumatcı", MessageBoxButtons.YesNo);
                if (cevap == DialogResult.Yes) { MessageBox.Show("Hatanız = " + "\n" + s); }
                else { }
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdi.Text == "" || txtSoyadi.Text == "" || txtTC.Text == "" || txtTel.Text == "" || cmbCinsiyet.Text == "")
                {
                    MessageBox.Show("Hata!!" + "\n" + "Lütfen tüm alanları doldurunuz.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (db.baglanti.State == ConnectionState.Open)
                    {
                        db.baglanti.Close();
                    }
                    db.baglanti.Open();
                    komut = new OleDbCommand("insert into MusteriBilgileri(Adi, Soyadi, Telefon_No, TC_Kimlik_No, Cinsiyet) values(@adi, @soyadi, @tel, @tc, @cinsiyet)", db.baglanti);
                    komut.Parameters.AddWithValue("@adi", txtAdi.Text);
                    komut.Parameters.AddWithValue("@soyadi", txtSoyadi.Text);
                    komut.Parameters.AddWithValue("@tel", txtTel.Text);
                    komut.Parameters.AddWithValue("@tc", txtTC.Text);
                    komut.Parameters.AddWithValue("@cinsiyet", cmbCinsiyet.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Müşteri Başarıyla Kaydedildi.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    doldur();
                    db.baglanti.Close();
                    txtAdi.Text = "";
                    txtSoyadi.Text = "";
                    txtTel.Text = "";
                    txtTC.Text = "";
                    cmbCinsiyet.Text = "";
                }

            }
            catch (Exception s) { MessageBox.Show(s.ToString()); }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtAdi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtSoyadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtTel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtTC.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                cmbCinsiyet.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            catch { }
        }
        ErrorProvider er = new ErrorProvider();

        private void txtTC_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
