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
    public partial class AracEkle : Form
    {
        public AracEkle()
        {
            InitializeComponent();
        }
        DataBase db = new DataBase();
        OleDbCommand komut;

        private void AracEkle_Load(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtMarka.Text=="" || txtKapasite.Text=="")
                {
                    MessageBox.Show("Hata | İki alanda boş geçilemez!","Malumatcı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                DialogResult s = MessageBox.Show("Aracı sisteme eklemek istediğinize emin misiniz ?", "Malumatcı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                db.baglanti.Open();
                if (s == DialogResult.Yes)
                {
                    komut = new OleDbCommand("insert into Araclar(Marka_Model, Kapasite) values(@marka,@kapasite)", db.baglanti);
                    komut.Parameters.AddWithValue("@marka", txtMarka.Text);
                    komut.Parameters.AddWithValue("@kapasite", txtKapasite.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Araç başarıyla sisteme eklendi", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                db.baglanti.Close();
            }
            catch { }

        }
    }
}
