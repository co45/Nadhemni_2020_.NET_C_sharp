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
            this.Close();
            Home f = new Home();
            f.Show();
        }
    }
}
