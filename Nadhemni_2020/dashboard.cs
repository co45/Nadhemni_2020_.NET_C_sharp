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
    public partial class dashboard : Form
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public dashboard()
        {
            InitializeComponent();
            timer1.Start();
        }
        

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bunifuCards2.Hide();
            bunifuCards1.Show();
        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            
               label1.Location = new Point(label1.Location.X + 9, label1.Location.Y);

                if (label1.Location.X > this.Width)
                {
                    label1.Location = new Point(0 - label1.Width, label1.Location.Y);
                }
            
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            label9.Text = DateTime.Now.ToString("MMM dd yyyy,hh:mm");

            bunifuCards1.Hide();
            bunifuCards2.Hide();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            
        }

        private void bunifuDropdown2_onItemSelected(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bunifuCards1.Hide();
            bunifuCards2.Show();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            try
            {
                tache t = new tache();
                t.description = bunifuMaterialTextbox4.Text;
                t.titre = bunifuMaterialTextbox3.Text;
                t.t_debut = dateTimePicker1.Value.Date;
                t.t_fin = dateTimePicker2.Value.Date;
                t.duree = int.Parse(bunifuDropdown2.selectedValue);
                t.type = bunifuDropdown1.selectedValue.ToString();
                db.tache.InsertOnSubmit(t);
                db.SubmitChanges();
                bunifuCards2.Refresh();
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
