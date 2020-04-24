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
    public partial class information_famille : Form
    {
        Information a = new Information();
        Information_membre x = new Information_membre();
        

        public information_famille()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_form x = new Main_form();
            x.Show();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {

            x.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();

            a.Show();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            x.Show();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            x.Show();

        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            x.Show();

        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            x.Show();

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Information.Pers.nbre_enfant = int.Parse(bunifuDropdown2.selectedValue);
            bunifuFlatButton1.Enabled = false;
            ActiveForm.Close();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (bunifuDropdown1.selectedValue=="Marié")
            {
                bunifuFlatButton3.Visible = true;
            }
            else
            {
                bunifuFlatButton3.Visible = false;
            }

            if (bunifuDropdown2.selectedValue == "1")
            {
                bunifuFlatButton4.Visible = true;
                bunifuFlatButton5.Visible = false;
                bunifuFlatButton6.Visible = false;
                bunifuFlatButton7.Visible = false;

            }
            else if (bunifuDropdown2.selectedValue == "2")
            {
                bunifuFlatButton4.Visible = true;
                bunifuFlatButton5.Visible = true;
                bunifuFlatButton6.Visible = false;
                bunifuFlatButton7.Visible = false;
            }
            else if (bunifuDropdown2.selectedValue == "3")
            {
                bunifuFlatButton4.Visible = true;
                bunifuFlatButton5.Visible = true;
                bunifuFlatButton6.Visible = true;
                bunifuFlatButton7.Visible = false;

            }
            else if (bunifuDropdown2.selectedValue == "4")
            {
                bunifuFlatButton4.Visible = true;
                bunifuFlatButton5.Visible = true;
                bunifuFlatButton6.Visible = true;
                bunifuFlatButton7.Visible = true;
            }
            else
            {
                bunifuFlatButton4.Visible = false;
                bunifuFlatButton5.Visible = false;
                bunifuFlatButton6.Visible = false;
                bunifuFlatButton7.Visible = false;
            }
            
        }

        
    }
}
