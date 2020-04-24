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
    public partial class planing_Form : Form
    {
        int topc = 1;
        public planing_Form()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Label l = new Label();
            TextBox T = new TextBox();
            this.Controls.Add(l);
            this.Controls.Add(T);
            l.Text = "Titre";
            l.BringToFront();
            T.BringToFront();
            T.Top = topc + 25;
            l.Top = topc;
            T.Left = 100;
            l.Left = T.Left + 100;
            T.Text = "tache " + this.topc.ToString();
            topc = topc + 1; 
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
