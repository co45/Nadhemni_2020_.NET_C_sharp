using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace Nadhemni_2020
{
    public partial class dashboard : Form
    {
        
        tache t = new tache();

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
            bunifuCards3.Hide();
        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            
               label1.Location = new Point(label1.Location.X + 9, label1.Location.Y);

                if (label1.Location.X > this.Width)
                {
                    label1.Location = new Point(0 - label1.Width, label1.Location.Y);
                    label9.Text = DateTime.Now.ToString("MMM dd yyyy,hh:mm");
            }
            
        }

        private void dashboard_Load(object sender, EventArgs e)
        {

            bunifuCards4.Hide();
            bunifuCards3.Hide();
            bunifuCards2.Hide();
            bunifuCards1.Show();
            bunifuFlatButton1.Select();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Information.db.tache;
            //label11.Text = Main_form.id.ToString();




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
            bunifuCards3.Hide();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            try
            {
                t.description = bunifuMaterialTextbox4.Text;
                t.titre = bunifuMaterialTextbox3.Text;
                t.t_debut = dateTimePicker1.Value.Date;
                t.t_fin = dateTimePicker2.Value.Date;
                t.duree = int.Parse(bunifuDropdown2.selectedValue);
                t.type = bunifuDropdown1.selectedValue.ToString();
                Information.db.tache.InsertOnSubmit(t);
                Information.db.SubmitChanges();

                MessageBox.Show("Tache ajouté avec succes !");
                this.Update();

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Information.db.tache;

                var selectQuery =
                      from a in Information.db.tache
                      select a;

                dataGridView1.DataSource = selectQuery;

            }
            catch (Exception x)
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

        private void button3_Click(object sender, EventArgs e)
        {
            bunifuCards3.Show();
            bunifuCards2.Hide();
            bunifuCards1.Hide();
            dataGridView1.Refresh();

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    label14.Text = Convert.ToString(selectedRow.Cells["id_tache"].Value);
                    label13.Text = Convert.ToString(selectedRow.Cells["titre"].Value);
                    
                }
                tache t = Information.db.tache.Single<tache>(x => x.id_tache == int.Parse(label14.Text));

                var lr = from x in Information.db.tache
                          where x.id_tache == int.Parse(label14.Text)
                          select x;

                foreach (var k in lr)
                {
                    Information.db.tache.DeleteOnSubmit(k);
                }
                Information.db.tache.DeleteOnSubmit(t);
                Information.db.SubmitChanges();
                var selectQuery =
                      from a in Information.db.tache
                      select a;

                dataGridView1.DataSource = selectQuery;

                MessageBox.Show("Suppression effectuée avec succées");
            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, " (Pas de  tache selectionné)");
            }
              
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string a = Convert.ToString(selectedRow.Cells["id_tache"].Value);
                string b = Convert.ToString(selectedRow.Cells["titre"].Value);
                this.label14.Text = a;
                this.label13.Text = b;
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bunifuCards4.Show();
            bunifuCards3.Hide();
            bunifuCards2.Hide();
            bunifuCards1.Hide();

            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                bunifuMaterialTextbox2.Text = Convert.ToString(selectedRow.Cells["description"].Value);
                bunifuMaterialTextbox1.Text = Convert.ToString(selectedRow.Cells["titre"].Value);
                

            }

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            bunifuCards4.Hide();
            bunifuCards3.Show();

            
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                label23.Text = Convert.ToString(selectedRow.Cells["id_tache"].Value);
               

            }

            tache tt = Information.db.tache.Single<tache>(x => x.id_tache == int.Parse(label23.Text));
           
                tt.description = bunifuMaterialTextbox2.Text;
                tt.titre = bunifuMaterialTextbox1.Text;
                tt.t_debut = dateTimePicker4.Value.Date;
                tt.t_fin = dateTimePicker3.Value.Date;
                tt.duree = int.Parse(bunifuDropdown4.selectedValue);
                tt.type = bunifuDropdown3.selectedValue.ToString();
                Information.db.tache.InsertOnSubmit(tt);
                Information.db.SubmitChanges();

                MessageBox.Show("Tache modifié avec succes !");
                this.Update();

             var sq =
                      from a in Information.db.tache
                      select a;

                dataGridView1.DataSource = sq;

            


            var selectQuery =
                      from a in Information.db.tache
                      select a;
            dataGridView1.DataSource = selectQuery;

            bunifuCards4.Hide();
            bunifuCards3.Show();
        }
    }
}
