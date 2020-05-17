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
using System.Speech.Recognition;
using System.Globalization;
using System.IO;

namespace Nadhemni_2020
{
    public partial class dashboard : Form
    {
        DataClassesDataContext dtb = new DataClassesDataContext();
        SpeechRecognitionEngine s = new SpeechRecognitionEngine(new CultureInfo("fr-FR"));


        public dashboard()
        {
            InitializeComponent();
            timer1.Start();

           /* Grammar words = new DictationGrammar();

            s.LoadGrammarAsync(words);
            s.SetInputToDefaultAudioDevice();
            RecognitionResult res = s.Recognize();

            if (bunifuCheckbox1.Checked == true )
                bunifuMaterialTextbox3.Text = res.Text;*/


        
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
            int idp = int.Parse(Main_form.id);
            personne t1 = Information.db.personne.Single<personne>(x => x.id_personne == idp);
            label11.Text = t1.nom;
            label9.Text = DateTime.Now.ToString("MMM dd yyyy,hh:mm");

            var r = from s in dtb.plan
                    where s.date_heure_fin < DateTime.Now
                    && s.personne.id_personne == idp
                    select s.personne;
            label10.Text = r.Count().ToString();


            bunifuCards4.Hide();
            bunifuCards3.Hide();
            bunifuCards2.Hide();
            bunifuCards1.Show();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Information.db.tache;
            //speechtoText();
            

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
                tache t = new tache();
                

                t.description = bunifuMaterialTextbox4.Text;
                t.titre = bunifuMaterialTextbox3.Text;
                t.duree = int.Parse(bunifuDropdown2.selectedValue);
                t.type = bunifuDropdown1.selectedValue.ToString();


                Information.db.tache.InsertOnSubmit(t);
                Information.db.SubmitChanges();

                plan p = Information.db.plan.Single(x => x.id_taches == t.id_tache);
                Information.db.plan.InsertOnSubmit(p);
                Information.db.SubmitChanges();

                MessageBox.Show("Tache ajouté avec succes !");
                bunifuMaterialTextbox4.Text="";
                t.titre = bunifuMaterialTextbox3.Text="";
                

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

                var lr = from x in Information.db.plan
                          where x.id_taches == int.Parse(label14.Text)
                          select x;

                foreach (var k in lr)
                {
                    Information.db.plan.DeleteOnSubmit(k);
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
                
                tt.duree = int.Parse(bunifuDropdown4.selectedValue);
                tt.type = bunifuDropdown3.selectedValue.ToString();
                Information.db.SubmitChanges();

                MessageBox.Show("Tache modifié avec succes !");

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


        /*private void bunifuMaterialTextbox3_Click(object sender, EventArgs e)
        {
            SpeechRecognitionEngine s = new SpeechRecognitionEngine(new CultureInfo("fr-FR"));
            Grammar words = new DictationGrammar();

            s.LoadGrammar(words);
            s.SetInputToDefaultAudioDevice();
            RecognitionResult res = s.Recognize();

            if(bunifuCheckbox1.Checked==true)
                bunifuMaterialTextbox3.Text = res.Text;

        }*/

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        /*private void bunifuMaterialTextbox4_Click(object sender, EventArgs e)
        {
            Grammar words = new DictationGrammar();

            s.LoadGrammar(words);
            s.SetInputToDefaultAudioDevice();
            RecognitionResult res = s.Recognize();

            if(bunifuCheckbox1.Checked == true)
                bunifuMaterialTextbox4.Text = res.Text;

        }*/
    }
}
