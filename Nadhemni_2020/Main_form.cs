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
    public partial class Main_form : Form
    {
        DataClassesDataContext context = new DataClassesDataContext();
        public static string id;
        public Main_form()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Information a = new Information();
            a.Show();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Information i = new Information();
            ActiveForm.Hide();
            i.Show();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (IsvalidUser(bunifuMaterialTextbox5.Text, bunifuMaterialTextbox1.Text))
            {
                var r = from s in context.user
                        where s.login == bunifuMaterialTextbox5.Text
                        && s.pass == bunifuMaterialTextbox1.Text
                        select s.id_pers ;

                id = r.FirstOrDefault().ToString();

                this.Hide();
                dashboard f = new dashboard();
                f.Show();
            }
            else
            {
                MessageBox.Show("Pseudo ou mot de passe invalide !");
            }
        }

        private bool IsvalidUser(string userName, string password)
        {
            var q = from p in context.user
                    where p.login == userName
                    && p.pass == password
                    select p;

            if (q.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
