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
    public partial class PersonelKayit : Form
    {
        public PersonelKayit()
        {
            InitializeComponent();
        }
        DataBase db = new DataBase();
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter da;
        DataSet ds;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAd.Text == "" || txtSoyad.Text == "" || txtTC.Text == "" || txtTel.Text == "" || comboBox1.Text == "" || txtPass.Text == "")
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
                    komut = new OleDbCommand("insert into Personel(Adi, Soyadi, TC_Kimlik_No, Telefon, Cinsiyet, Sifre) values(@adi, @soyadi, @tc, @tel, @cinsiyet, @sifre) ", db.baglanti);
                    komut.Parameters.AddWithValue("@adi", txtAd.Text);
                    komut.Parameters.AddWithValue("@soyadi", txtSoyad.Text);
                    komut.Parameters.AddWithValue("@tc", txtTC.Text);
                    komut.Parameters.AddWithValue("@tel", txtTel.Text);
                    komut.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
                    komut.Parameters.AddWithValue("@sifre", txtPass.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Personel Başarıyla Kaydedildi.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    doldur();

                    db.baglanti.Close();
                }                
            }
            catch { }

        }
        void doldur()
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                da = new OleDbDataAdapter("select * from Personel", db.baglanti);
                ds = new DataSet();
                da.Fill(ds, "Personel");
                dataGridView1.DataSource = ds.Tables["Personel"];
                dataGridView1.Columns[0].Visible = false;
                db.baglanti.Close();

            }
            catch { }

        }

        private void PersonelKayit_Load(object sender, EventArgs e)
        {
            doldur();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                komut = new OleDbCommand("delete from Personel where ID=@id", db.baglanti);
                komut.Parameters.AddWithValue("@id", label7.Text);
                komut.ExecuteNonQuery();
                db.baglanti.Close();
                MessageBox.Show("Personel Silindi.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                label7.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtTC.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtTel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtPass.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            }
            catch { }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            db.baglanti.Open();
            komut = new OleDbCommand("update Personel set Adi=@adi, Soyadi=@soyad, TC_Kimlik_No=@tc, Telefon=@tel, Cinsiyet=@cinsiyet, Sifre=@sifre where ID=@id", db.baglanti);
            komut.Parameters.AddWithValue("@adi", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@tc", txtTC.Text);
            komut.Parameters.AddWithValue("@tel", txtTel.Text);
            komut.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
            komut.Parameters.AddWithValue("@sifre", txtPass.Text);
            komut.Parameters.AddWithValue("@id", label7.Text);
            komut.ExecuteNonQuery();
            db.baglanti.Close();
            MessageBox.Show("Personel bilgileri başarıyla güncellendi.", "Maluamtcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            doldur();
        }
    }
}