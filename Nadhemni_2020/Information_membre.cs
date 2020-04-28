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
        identity m = new identity();

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
            m.date_naissance = metroDateTime1.Value.Date;
            m.genre = (radioButton1.Checked == true) ? "Homme" : "Femme";
            m.fonction = bunifuMaterialTextbox2.Text;
            m.etablissement = bunifuMaterialTextbox1.Text;
            m.etat_sante = bunifuDropdown2.selectedValue;
            db.identity.InsertOnSubmit(m);
            db.SubmitChanges();

        }
    }
}
