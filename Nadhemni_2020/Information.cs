using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Nadhemni_2020
{
    
    public partial class Information : Form
    {

        DataClassesDataContext db = new DataClassesDataContext();
         public identity pers = new identity();
         public user us = new user();

        
        

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
            us.login = bunifuMaterialTextbox1.Text;
            us.mdp = bunifuMaterialTextbox2.Text;
            pers.nom = bunifuMaterialTextbox3.Text;
            pers.prenom = bunifuMaterialTextbox5.Text;
            pers.date_naissance = metroDateTime1.Value.Date;
            pers.genre = (radioButton1.Checked == true) ? "Homme" : "Femme";
            pers.etat_civil = bunifuDropdown3.selectedValue.ToString();
            pers.fonction = bunifuMaterialTextbox4.Text;
            pers.mail = bunifuMaterialTextbox8.Text;
            pers.etat_sante = bunifuDropdown1.selectedValue.ToString();
            pers.nbre_enfant = int.Parse(bunifuDropdown2.selectedValue);
            
            db.user.InsertOnSubmit(us);
            db.identity.InsertOnSubmit(pers);
            db.SubmitChanges();

            this.Hide();
            dashboard a = new dashboard();
            a.Show();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {

            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (o.ShowDialog()==DialogResult.OK)
            {
                String img = o.FileName.ToString();
                pictureBox3.ImageLocation = img;
                



            }



        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            planing_Form p = new planing_Form();
            p.Show();
        }

        

        
    }
}
