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

        
        private static identity pers = new identity();
        private static user us = new user();

        public static identity Pers { get => pers; set => pers = value; }
        public static user Us { get => us; set => us = value; }
        

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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            us.login = bunifuMaterialTextbox1.Text;
            us.mdp = bunifuMaterialTextbox2.Text;
            Pers.nom = bunifuMaterialTextbox3.Text;
            Pers.prenom = bunifuMaterialTextbox5.Text;
            Pers.date_naissance = metroDateTime1.Value.Date;
            Pers.genre = (radioButton1.Checked == true) ? "Homme" : "Femme";
            pers.etat_civil = bunifuMaterialTextbox6.Text;
            Pers.fonction = bunifuMaterialTextbox4.Text;
            Pers.mail = bunifuMaterialTextbox8.Text;
            Pers.etat_sante = bunifuDropdown1.selectedValue.ToString();
            

            
            

            this.Hide();
            information_famille a = new information_famille();
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
