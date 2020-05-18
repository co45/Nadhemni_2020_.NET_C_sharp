using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Nadhemni_2020
{
    
    public partial class Information : Form
    {
        public static string idd;

        internal static DataClassesDataContext db = new DataClassesDataContext();
         

        
        

        public Information()
        {
            InitializeComponent();
        }
        

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
            Main_form a = new Main_form();
            a.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Close();
            Main_form a = new Main_form();
            a.Show();
        }

        public void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
        }

        public void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            
            
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (o.ShowDialog()==DialogResult.OK)
            {
                String img = o.FileName.ToString();
                pictureBox3.ImageLocation = img;
                pictureBox3.Image = Image.FromFile(img);


            }



        }

        private void Information_Load(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                personne pers = new personne();
                user u = new user();
                adresse ad = new adresse();
                //plan p = new plan();

                //insertion des informations
                pers.nom = bunifuMaterialTextbox3.Text;
                pers.prenom = bunifuMaterialTextbox5.Text;
                pers.date_de_naissance = metroDateTime1.Value.Date;
                pers.genre = (radioButton1.Checked == true) ? "Homme" : "Femme";
                pers.etat_civil = bunifuDropdown3.selectedValue.ToString();
                pers.fonction = bunifuMaterialTextbox4.Text;
                pers.mail = bunifuMaterialTextbox8.Text;
                pers.etat_sante = bunifuDropdown1.selectedValue.ToString();
                pers.nbre_enfant = int.Parse(bunifuDropdown2.selectedValue);
                /* insertion de l'image 
                MemoryStream ms = new MemoryStream();
                pictureBox3.Image.Save(ms,pictureBox3.Image.RawFormat);
                byte[] photo_aray = ms.ToArray();
                pers.photo = photo_aray;*/
                u.personne = pers;

                u.personne = pers;
                u.login = bunifuMaterialTextbox1.Text;
                u.pass = bunifuMaterialTextbox2.Text;
                idd = u.id_pers.ToString();
                db.user.InsertOnSubmit(u);
                ad.personne = pers;
                ad.numero = bunifuMaterialTextbox6.Text;
                ad.rue = bunifuMaterialTextbox7.Text;
                ad.localisation = mapform.Code;

                
                db.adresse.InsertOnSubmit(ad);

                db.SubmitChanges();


                this.Close();
                dashboard a = new dashboard();
                a.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            mapform map = new mapform();
            map.Show();
            map.BringToFront();
        }
    }
}
