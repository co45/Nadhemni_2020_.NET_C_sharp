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
using System.Windows.Forms;

namespace Nadhemni_2020
{
    public partial class dashboard : Form
    {
        int idp;
        int num;
        Main_form main = new Main_form();
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
            main.Show();
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

            if (Main_form.id == 0)
                idp = int.Parse(Information.idd);
            else
                idp = Main_form.id;


            personne t1 = Information.db.personne.Single<personne>(x => x.id_personne == idp);
            label11.Text = t1.nom;
            label9.Text = DateTime.Now.ToString("MMM dd yyyy,hh:mm");

            var r = from s in dtb.plan
                    where s.date_heure_fin > DateTime.Now
                    && s.personne.id_personne == idp
                    select s.personne;

            var all = from s in dtb.plan
                    where s.personne.id_personne == idp
                    select s.id_taches;

            label10.Text = r.Count().ToString();
            bunifuGauge1.ProgressColor1 = Color.Indigo;
            if (r.Count() != 0)
            {
                int gauge = ((all.Count() - r.Count()) / all.Count()) * 100; 
                bunifuGauge1.Value = gauge;
            }
            
            else
                bunifuGauge1.Value = 30;

            var results = (from g in dtb.personne
                           join tr in dtb.plan on g.id_personne equals tr.id_prop
                           join t in dtb.tache on tr.id_taches equals t.id_tache
                           orderby tr.date_heure_debut descending
                           select new
                           {
                               Numero= t.id_tache,
                               Titre = t.titre,
                               Description = t.description,
                               Debut = tr.date_heure_debut,
                               Fin = tr.date_heure_fin
                           }
                       ).ToList();     

            bunifuCustomDataGrid1.DataSource = results;
            bunifuCustomDataGrid1.Columns[0].HeaderText = "Numero";
            bunifuCustomDataGrid1.Columns[1].HeaderText = "Tache";
            bunifuCustomDataGrid1.Columns[2].HeaderText = "Description";
            bunifuCustomDataGrid1.Columns[3].HeaderText = "Debut";
            bunifuCustomDataGrid1.Columns[4].HeaderText = "Fin";
            bunifuCustomDataGrid1.Refresh();

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
                plan p = new plan();
               
                

                t.description = bunifuMaterialTextbox4.Text;
                t.titre = bunifuMaterialTextbox3.Text;
                t.duree = int.Parse(bunifuDropdown2.selectedValue);
                t.type = bunifuDropdown1.selectedValue.ToString();
                
                p.personne = Information.db.personne.Single<personne>(x => x.id_personne == idp);
                t.emplacement = mapform.Code;
                p.tache = t;
                p.date_heure_debut = dateTimePicker2.Value;
                p.date_heure_fin = dateTimePicker1.Value; 


                Information.db.plan.InsertOnSubmit(p);
                Information.db.tache.InsertOnSubmit(t);
                Information.db.SubmitChanges();


                MessageBox.Show("Tache ajouté avec succes !");
                bunifuMaterialTextbox4.Text = "";
                bunifuMaterialTextbox3.Text = "";


                bunifuCustomDataGrid1.Refresh();

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
            bunifuCustomDataGrid1.Refresh();

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
              
                if (bunifuCustomDataGrid1.SelectedCells.Count > 0)
                {
                    int selectedrowindex = bunifuCustomDataGrid1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = bunifuCustomDataGrid1.Rows[selectedrowindex];
                    num = Convert.ToInt32(selectedRow.Cells["Numero"].Value);
                    
                    
                }
                tache t = Information.db.tache.Single<tache>(x => x.id_tache == num);

                var lr = from x in Information.db.plan
                          where x.id_taches == num
                          select x;

                foreach (var k in lr)
                {
                    Information.db.plan.DeleteOnSubmit(k);
                }
                Information.db.tache.DeleteOnSubmit(t);
                Information.db.SubmitChanges();

                

                MessageBox.Show("Suppression effectuée avec succées");
            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, " (Pas de  tache selectionné)");
            }
              
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedCells.Count > 0)
            {
                int selectedrowindex = bunifuCustomDataGrid1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = bunifuCustomDataGrid1.Rows[selectedrowindex];
                
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bunifuCards4.Show();
            bunifuCards3.Hide();
            bunifuCards2.Hide();
            bunifuCards1.Hide();

            if (bunifuCustomDataGrid1.SelectedCells.Count > 0)
            {
                int selectedrowindex = bunifuCustomDataGrid1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = bunifuCustomDataGrid1.Rows[selectedrowindex];
                bunifuMaterialTextbox2.Text = Convert.ToString(selectedRow.Cells["Description"].Value);
                bunifuMaterialTextbox1.Text = Convert.ToString(selectedRow.Cells["Tache"].Value);
                

            }

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            bunifuCards4.Hide();
            bunifuCards3.Show();

            
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedCells.Count > 0)
            {
                int selectedrowindex = bunifuCustomDataGrid1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = bunifuCustomDataGrid1.Rows[selectedrowindex];
                label23.Text = Convert.ToString(selectedRow.Cells["Numero"].Value);

            }

            tache tt = Information.db.tache.Single<tache>(x => x.id_tache == int.Parse(label23.Text));
           
                tt.description = bunifuMaterialTextbox2.Text;
                tt.titre = bunifuMaterialTextbox1.Text;
                
                tt.duree = int.Parse(bunifuDropdown4.selectedValue);
                tt.type = bunifuDropdown3.selectedValue.ToString();
                Information.db.SubmitChanges();

                MessageBox.Show("Tache modifié avec succes !");

            bunifuCustomDataGrid1.Refresh();

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
            main.Show();

        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            mapform m = new mapform();
            m.Show();
        }

        private void label10_Click_1(object sender, EventArgs e)
        {

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
