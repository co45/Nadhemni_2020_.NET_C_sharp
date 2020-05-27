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
        int idp;
        int num;
        SpeechRecognitionEngine s = new SpeechRecognitionEngine(new CultureInfo("fr-FR"));
        SpeechRecognitionEngine rec = new SpeechRecognitionEngine(new CultureInfo("fr-FR"));

        Grammar words = new DictationGrammar();
        DataClassesDataContext dtb = new DataClassesDataContext();
        Main_form main = new Main_form();



        public dashboard()
        {
            InitializeComponent();
            timer1.Start();
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
            bunifuCards1.BringToFront();
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

            bunifuCards5.Hide();
            bunifuCards4.Hide();
            bunifuCards3.Hide();
            bunifuCards2.Hide();
            bunifuCards1.Show();


            if (Main_form.id == 0)
                idp = int.Parse(Information.idd);
            else
                idp = Main_form.id;

            var req = (from g in dtb.personne
                       join tr in dtb.plan on g.id_personne equals tr.id_prop
                       join t in dtb.tache on tr.id_taches equals t.id_tache
                       where tr.id_prop == idp && tr.accomplie == 0 
                       orderby tr.date_heure_debut descending
                       select t.titre
                       ).ToList();
            

            foreach (var titre in req)
            {
                String news = string.Concat(req);
                label1.Text = news;
            }

            personne t1 = Information.db.personne.Single<personne>(x => x.id_personne == idp);
            label11.Text = t1.nom;
            label9.Text = DateTime.Now.ToString("MMM dd yyyy,hh:mm");
            
            datagridtacheall();



        }

        public void datagridtacheall()
        {
            var results = (from g in dtb.personne
                           join tr in dtb.plan on g.id_personne equals tr.id_prop
                           join t in dtb.tache on tr.id_taches equals t.id_tache
                           orderby tr.date_heure_debut descending
                           select new
                           {
                               Numero = t.id_tache,
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
        }

        public void datagridtachewait()
        {
            
            var results = (from g in dtb.personne
                           join tr in dtb.plan on g.id_personne equals tr.id_prop
                           join t in dtb.tache on tr.id_taches equals t.id_tache
                           where tr.accomplie == 0
                           orderby tr.date_heure_debut descending
                           select new
                           {
                               
                               Titre = t.titre,
                               Description = t.description,
                               Debut = tr.date_heure_debut,
                               Fin = tr.date_heure_fin
                           }
                       ).ToList();

            bunifuCustomDataGrid1.DataSource = results;
            bunifuCustomDataGrid1.Columns[0].HeaderText = "Tache";
            bunifuCustomDataGrid1.Columns[1].HeaderText = "Description";
            bunifuCustomDataGrid1.Columns[2].HeaderText = "Debut";
            bunifuCustomDataGrid1.Columns[3].HeaderText = "Fin";
            bunifuCustomDataGrid1.Refresh();
        }

        public void datagridtachedone()
        {
            var results = (from g in dtb.personne
                           join tr in dtb.plan on g.id_personne equals tr.id_prop
                           join t in dtb.tache on tr.id_taches equals t.id_tache
                           where tr.accomplie == 1
                           orderby tr.date_heure_debut descending
                           select new
                           {
                               
                               Titre = t.titre,
                               Description = t.description,
                               Debut = tr.date_heure_debut,
                               Fin = tr.date_heure_fin
                           }
                       ).ToList();

            bunifuCustomDataGrid1.DataSource = results;
           
            bunifuCustomDataGrid1.Columns[0].HeaderText = "Tache";
            bunifuCustomDataGrid1.Columns[1].HeaderText = "Description";
            bunifuCustomDataGrid1.Columns[2].HeaderText = "Debut";
            bunifuCustomDataGrid1.Columns[3].HeaderText = "Fin";

            bunifuCustomDataGrid1.Refresh();
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
            bunifuCards2.BringToFront();
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
                p.accomplie = 0;


                Information.db.plan.InsertOnSubmit(p);
                Information.db.tache.InsertOnSubmit(t);
                Information.db.SubmitChanges();


                MessageBox.Show("Tache ajouté avec succes !");
                bunifuMaterialTextbox4.Text = "";
                bunifuMaterialTextbox3.Text = "";


                datagridtacheall();
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
            bunifuCards3.BringToFront();
            bunifuCards2.Hide();
            bunifuCards1.Hide();
            bunifuCards4.Hide();
            bunifuCards5.Hide();
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


                datagridtacheall();
                MessageBox.Show("Suppression effectuée avec succées");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " (Pas de tache selectionné)");
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
                bunifuMaterialTextbox1.Text = Convert.ToString(selectedRow.Cells["Titre"].Value);


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

            datagridtacheall();

            bunifuCards4.Hide();
            bunifuCards3.Show();
            bunifuCards3.BringToFront();
        }



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

        private void button5_Click(object sender, EventArgs e)
        {
            bunifuCards5.Show();
            bunifuCards5.BringToFront();
            bunifuCards1.Hide();
            bunifuCards2.Hide();
            bunifuCards3.Hide();
            bunifuCards4.Hide();

            //jour
            var r = from s in dtb.plan
                    join rf in dtb.tache on s.accomplie equals '1'
                    where  s.personne.id_personne == idp && s.date_heure_fin == DateTime.Today && s.accomplie == 1
                    select s.personne;
            var all = from s in dtb.plan
                      where s.personne.id_personne == idp && s.date_heure_fin.Value.Day == DateTime.Today.Day
                      select s.id_taches;


            int ri = r.Count();
            int alli = all.Count();

            label27.Text = (alli - ri).ToString();
            label29.Text = alli.ToString();

           
            if (alli != 0)
            {
                int gaugej = ((alli - ri) / alli * 100);
                bunifuGauge1.Value = gaugej;
            }
            else
                bunifuGauge1.Value = 0;
            
            
            
            //mois
            var rm = from s in dtb.plan
                    join rf in dtb.tache on s.accomplie equals 1
                    where s.personne.id_personne == idp && s.date_heure_fin.Value.Month == DateTime.Today.Month
                     select s.personne;
            var allm = from s in dtb.plan
                      where s.personne.id_personne == idp && s.date_heure_fin.Value.Month == DateTime.Today.Month
                      select s.id_taches;
            int alm = allm.Count();

            int gaugem = ((alm - rm.Count()) / alm * 100);
            bunifuGauge1.Value = gaugem;
            

            //année
            var ra = from s in dtb.plan
                     join rf in dtb.tache on s.accomplie equals 1
                     where s.personne.id_personne == idp && s.date_heure_fin.Value.Year == DateTime.Today.Year
                     select s.personne;
            var alla = from s in dtb.plan
                      where s.personne.id_personne == idp && s.date_heure_fin.Value.Year == DateTime.Today.Year
                      select s.id_taches;
            int ala = alla.Count();
            int gaugea = ((ala - ra.Count()) / ala * 100);
            bunifuGauge1.Value = gaugea;
            
            
        }

        public String stt(Grammar gr, SpeechRecognitionEngine en)
        {

            en.LoadGrammar(gr);
            en.SetInputToDefaultAudioDevice();
            RecognitionResult res = en.Recognize();
            String a = "";
            a = res.Text;
            en.UnloadGrammar(gr);

            return a;
        }

   

        private void Rec_SpeechRecognized1(object sender, SpeechRecognizedEventArgs e)
        {


                pictureBox5.Show();
                switch (e.Result.Text)
                {
                    case "Titre":
                        pictureBox5.Location = new Point(336, 44);
                        bunifuMaterialTextbox3.Text = stt(words, s).ToString();
                        break;

                    case "Description":
                    pictureBox5.Location = new Point(336, 88);
                    bunifuMaterialTextbox4.Text = stt(words, s).ToString();
                        break;

                    case "Durée":
                    pictureBox5.Location = new Point(378, 134);
                    switch (e.Result.Text)
                        {
                            case "un":
                                bunifuDropdown2.selectedIndex = 0 ;
                                break;
                            case "deux":
                                bunifuDropdown2.selectedIndex = 1;
                                break;
                            case "trois":
                                bunifuDropdown2.selectedIndex = 2;
                                break;
                            case "quatre":
                                bunifuDropdown2.selectedIndex = 3;
                                break;
                            case "cinq":
                                bunifuDropdown2.selectedIndex = 4;
                                break;

                    }
                        break;

                    case "Type":
                    pictureBox5.Location = new Point(336, 172);
                    switch (e.Result.Text)
                        {
                            case "Normale":
                                bunifuDropdown1.selectedIndex = 0;
                                break;
                            case "Habituelle":
                                bunifuDropdown1.selectedIndex = 2;
                                break;
                            case "Inhabituelle":
                                bunifuDropdown1.selectedIndex = 3;
                                break;
                            case "Urgente":
                                bunifuDropdown1.selectedIndex = 1;
                                break;
                        }
                        break;

                    case "Emplacement":
                        mapform mf = new mapform();
                        mf.Show();
                        break;
                }


        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedCells.Count > 0)
            {
                int selectedrowindex = bunifuCustomDataGrid1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = bunifuCustomDataGrid1.Rows[selectedrowindex];
                label23.Text = Convert.ToString(selectedRow.Cells["Numero"].Value);
            }

            plan pt = Information.db.plan.Single<plan>(x => x.id_taches == int.Parse(label23.Text));

            pt.accomplie = 1;
            Information.db.SubmitChanges();

            datagridtachedone();
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedCells.Count > 0)
            {
                int selectedrowindex = bunifuCustomDataGrid1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = bunifuCustomDataGrid1.Rows[selectedrowindex];
                label23.Text = Convert.ToString(selectedRow.Cells["Numero"].Value);
            }

            plan pt = Information.db.plan.Single<plan>(x => x.id_taches == int.Parse(label23.Text));

            pt.accomplie = 0;
            Information.db.SubmitChanges();

            datagridtachewait();
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            datagridtachedone();
            
            
        }

        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {
            datagridtacheall();
        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            datagridtachewait();
        }

        public int quest (DateTime date )
        {
            var all = from s in dtb.plan
                      where s.personne.id_personne == idp && s.date_heure_fin == date
                      select s.id_taches;

            int fin = all.Count();

            return  fin;
        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "Normale", "Urgente", "Habituelle", "Inhabituelle", "Titre", "Description", "Durée", "Type", "Debut", "Fin", "Emplacement","un","deux","trois","quatre" });
            GrammarBuilder b = new GrammarBuilder();
            b.Append(commands);
            Grammar gr = new Grammar(b);
            rec.LoadGrammarAsync(gr);
            rec.SetInputToDefaultAudioDevice();
            rec.RecognizeAsync(RecognizeMode.Multiple);
            rec.SpeechRecognized += Rec_SpeechRecognized1;
            
            
        }
    }    
}
