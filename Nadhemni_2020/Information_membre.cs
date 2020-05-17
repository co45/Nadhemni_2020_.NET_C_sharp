using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nadhemni_2020
{
    public partial class Information_membre : Form
    {
        DataClassesDataContext db = new DataClassesDataContext();
        personne m = new personne();

        public Information_membre()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            m.nom = bunifuMaterialTextbox3.Text;
            m.prenom = bunifuMaterialTextbox5.Text;
            m.date_de_naissance = metroDateTime1.Value.Date;
            m.genre = (radioButton1.Checked == true) ? "Homme" : "Femme";
            m.fonction = bunifuMaterialTextbox2.Text;
            m.etablissement = bunifuMaterialTextbox1.Text;
            m.etat_sante = bunifuDropdown2.selectedValue;
            db.personne.InsertOnSubmit(m);
            db.SubmitChanges();

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            mapform p = new mapform();
            p.Show();
        }
    }
}
