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
using System.Timers;
using System.Drawing;


namespace Nadhemni_2020
{
    public partial class dashboard : Form
    {
        System.Timers.Timer timer;
        public static int idp;
        public static int num;
        public static String reqs;
        SpeechRecognitionEngine s = new SpeechRecognitionEngine(new CultureInfo("fr-FR"));
        SpeechRecognitionEngine rec = new SpeechRecognitionEngine(new CultureInfo("fr-FR"));

        Grammar words = new DictationGrammar();
        DataClassesDataContext dtb = new DataClassesDataContext();
        Main_form main = new Main_form();



        //recuperer id personne
        public dashboard()
        {
            InitializeComponent();
            if (Main_form.id == 0)
                idp = int.Parse(Information.idd);
            else
                idp = Main_form.id;

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

        //executer une methode a une date donnée
        public void Execute(Action action, DateTime ExecutionTime)
        {
            try
            {
                Task WaitTask = Task.Delay((int)ExecutionTime.Subtract(DateTime.Now).Seconds);
                WaitTask.ContinueWith(_ => action);
                WaitTask.Start();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        // verification chaque minutes la presence d'une nouvelle tache est l'executer si = True  
        private void timer1_Tick(object sender, EventArgs e)
        {
            label9.Text = DateTime.Now.ToString("MMM dd yyyy,hh:mm");
            DateTime ct = DateTime.Now;

            
            var reqe = (from g in dtb.personne
                        join tr in dtb.plan on g.id_personne equals tr.id_prop
                        join t in dtb.tache on tr.id_taches equals t.id_tache
                        where tr.id_prop == idp && tr.accomplie == 0 && tr.date_heure_debut.Value.Day == ct.Day && tr.date_heure_debut.Value.TimeOfDay == ct.TimeOfDay
                        orderby tr.date_heure_debut.Value.Date ascending
                        select new
                        {
                            titre = t.titre,
                            date = tr.date_heure_debut
                        }).ToList();

            DateTime reqt; 

            if (reqe.Count() != 0)
            {
                reqt = reqe.First().date.Value;
                reqs = reqe.First().titre.ToString();
                notif_form nf = new notif_form(reqs);

                Execute(opennf, reqt);
                reqe.RemoveAt(0);
            }
            

        }

        //creation et affichage d'une notification
       private void opennf ()
        {
            notif_form nf = new notif_form(reqs);
            nf.Show();
        }


        private void dashboard_Load(object sender, EventArgs e)
        {
            
            bunifuCards5.Hide();
            bunifuCards4.Hide();
            bunifuCards3.Hide();
            bunifuCards2.Hide();
            bunifuCards1.Show();

            var req = from g in dtb.personne
                      join tr in dtb.plan on g.id_personne equals tr.id_prop
                      join t in dtb.tache on tr.id_taches equals t.id_tache
                      where tr.id_prop == idp && tr.accomplie == 0
                      orderby tr.date_heure_debut ascending
                      select t.titre;

            personne t1 = Information.db.personne.Single(x => x.id_personne == idp);
            label11.Text = t1.nom;
            label9.Text = DateTime.Now.ToString("MMM dd yyyy,hh:mm");
            label10.Text = req.Count().ToString();
            
            datagridtache(0);

        }

        //afficher les taches selon les parametres : (acc==1) => taches accomplies / (acc==0) => taches non accomplies / (acc==2) => toutes les taches 
        public void datagridtache(int acc )
        {
            
            if (acc == 2)
            {
                var result = (from g in dtb.personne
                               join tr in dtb.plan on g.id_personne equals tr.id_prop
                               join t in dtb.tache on tr.id_taches equals t.id_tache
                               where g.id_personne == idp
                               orderby tr.date_heure_debut ascending
                               select new
                               {
                                   Numero = t.id_tache,
                                   Debutj = tr.date_heure_debut,
                                   Fin = tr.date_heure_fin,
                                   Titre = t.titre,
                                   Description = t.description

                               }
                       ).ToList();
                bunifuCustomDataGrid1.DataSource = result;
            }
            else
            {
                var results = (from g in dtb.personne
                               join tr in dtb.plan on g.id_personne equals tr.id_prop
                               join t in dtb.tache on tr.id_taches equals t.id_tache
                               where g.id_personne == idp && tr.accomplie == acc
                               orderby tr.date_heure_debut ascending
                               select new
                               {
                                   Numero = t.id_tache,
                                   Debutj = tr.date_heure_debut,
                                   Fin = tr.date_heure_fin,
                                   Titre = t.titre,
                                   Description = t.description

                               }
                       ).ToList();
                bunifuCustomDataGrid1.DataSource = results;
            }
            

            

            bunifuCustomDataGrid1.Columns[0].HeaderText = "Numero";
            bunifuCustomDataGrid1.Columns[3].HeaderText = "Tache";
            bunifuCustomDataGrid1.Columns[4].HeaderText = "Description";
            bunifuCustomDataGrid1.Columns[1].HeaderText = "Debut";
            bunifuCustomDataGrid1.Columns[2].HeaderText = "Fin";



        }
        
        public void tache_date (DateTime d)
        {
            var result = (from g in dtb.personne
                          join tr in dtb.plan on g.id_personne equals tr.id_prop
                          join t in dtb.tache on tr.id_taches equals t.id_tache
                          where g.id_personne == idp && tr.accomplie == 0 && tr.date_heure_debut.Value.DayOfYear == d.DayOfYear
                          orderby tr.date_heure_debut ascending
                          select new
                          {
                              Debutj = tr.date_heure_debut,
                              Fin = tr.date_heure_fin,
                              Titre = t.titre,
                              Description = t.description

                          }
                       ).ToList();

            bunifuCustomDataGrid2.DataSource = result;
            bunifuCustomDataGrid3.DataSource = result;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bunifuCards1.Hide();
            bunifuCards2.Show();
            bunifuCards2.BringToFront();
            bunifuCards3.Hide();
        }

        // Ajouter une tache
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
                p.date_heure_fin = dateTimePicker2.Value + TimeSpan.FromHours(Convert.ToInt32(bunifuDropdown2.selectedValue));
                p.accomplie = 0;


                Information.db.plan.InsertOnSubmit(p);
                Information.db.tache.InsertOnSubmit(t);
                Information.db.SubmitChanges();


                MessageBox.Show("Tache ajouté avec succes !");
                bunifuMaterialTextbox4.Text = "";
                bunifuMaterialTextbox3.Text = "";


                datagridtache(2);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

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

        // Supprimer une tache
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


                datagridtache(2);
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

        //modifier une tache 
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedCells.Count > 0)
            {
                int selectedrowindex = bunifuCustomDataGrid1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = bunifuCustomDataGrid1.Rows[selectedrowindex];
                label23.Text = Convert.ToString(selectedRow.Cells["Numero"].Value);
            }

            tache tt = Information.db.tache.Single<tache>(x => x.id_tache == int.Parse(label23.Text));
            plan pp = Information.db.plan.Single<plan>(x => x.id_taches == int.Parse(label23.Text));

            tt.description = bunifuMaterialTextbox2.Text;
            tt.titre = bunifuMaterialTextbox1.Text;
            tt.duree = int.Parse(bunifuDropdown4.selectedValue);
            tt.type = bunifuDropdown3.selectedValue.ToString();
            
            pp.date_heure_debut = dateTimePicker4.Value ;
            pp.date_heure_fin = dateTimePicker4.Value + TimeSpan.FromHours(Convert.ToInt32(bunifuDropdown4.selectedValue));


            Information.db.SubmitChanges();

            MessageBox.Show("Tache modifié avec succes !");

            datagridtache(2);
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

        //Statistiques
        private void stat ()
        {
            bunifuCards5.Show();
            bunifuCards5.BringToFront();
            bunifuCards1.Hide();
            bunifuCards2.Hide();
            bunifuCards3.Hide();
            bunifuCards4.Hide();
            
            //jour
            var r = from g in dtb.personne
                    join tr in dtb.plan on g.id_personne equals tr.id_prop
                    join t in dtb.tache on tr.id_taches equals t.id_tache
                    where tr.id_prop == idp && tr.date_heure_debut.Value.Date == DateTime.Today && tr.accomplie == 1
                    select t.titre;

            var all = from g in dtb.personne
                      join tr in dtb.plan on g.id_personne equals tr.id_prop
                      join t in dtb.tache on tr.id_taches equals t.id_tache
                      where tr.id_prop == idp && tr.date_heure_debut.Value.Date == DateTime.Today
                      select t.titre;

            float ri = r.Count();
            float alli = all.Count();

            label27.Text = Convert.ToInt32(ri).ToString();
            label29.Text = alli.ToString();


            if (alli != 0)
            {
                if (ri == 0)
                {
                    bunifuGauge1.Value = 0;
                }
                else
                {
                    bunifuGauge1.Value = 100;
                    float gaugej = ((ri / alli) * 100);
                    int gj = Convert.ToInt32(gaugej);
                    bunifuGauge1.Value = gj;

                }

            }
            else
            {
                bunifuGauge1.Value = 0;

            }



            //mois
            var rm = from g in dtb.personne
                     join tr in dtb.plan on g.id_personne equals tr.id_prop
                     join t in dtb.tache on tr.id_taches equals t.id_tache
                     where tr.id_prop == idp && tr.date_heure_debut.Value.Date.Month == DateTime.Today.Month && tr.accomplie == 1
                     select t.titre;
            var allm = from g in dtb.personne
                       join tr in dtb.plan on g.id_personne equals tr.id_prop
                       join t in dtb.tache on tr.id_taches equals t.id_tache
                       where tr.id_prop == idp && tr.date_heure_debut.Value.Date.Month == DateTime.Today.Month
                       select t.titre;


            label37.Text = rm.Count().ToString();
            label36.Text = allm.Count().ToString();
            float alm = allm.Count();

            if (alm != 0)
            {
                if (rm.Count() == 0)
                {
                    bunifuGauge2.Value = 0;
                }
                else
                {
                    
                    float gaugem = ((rm.Count() / alm) * 100);
                    bunifuGauge2.Value = Convert.ToInt32(gaugem);
                }

            }
            else
            {
                bunifuGauge2.Value = 0;

            }

            


            //année
            var ra = from g in dtb.personne
                     join tr in dtb.plan on g.id_personne equals tr.id_prop
                     join t in dtb.tache on tr.id_taches equals t.id_tache
                     where tr.id_prop == idp && tr.date_heure_debut.Value.Date.Year == DateTime.Today.Year && tr.accomplie == 1
                     select t.titre;

            var alla = from g in dtb.personne
                       join tr in dtb.plan on g.id_personne equals tr.id_prop
                       join t in dtb.tache on tr.id_taches equals t.id_tache
                       where tr.id_prop == idp && tr.date_heure_debut.Value.Date.Year == DateTime.Today.Year
                       select t.titre;

            label34.Text = ra.Count().ToString();
            label35.Text = alla.Count().ToString();
            float ala = alla.Count();

            if (ala != 0)
            {
                if (ra.Count() == 0)
                {
                    bunifuGauge3.Value = 0;
                }
                else
                {
                    
                    float gaugea = ((ra.Count() / ala) * 100);
                    bunifuGauge3.Value = Convert.ToInt32(gaugea);
                }

            }
            else
            {
                bunifuGauge2.Value = 0;

            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            stat();  
        }

        // Resultat de l'enregistrement vocal pour remplir le formulaire d'une tache (toSting)
        public String stt(Grammar gra, SpeechRecognitionEngine en)
        {
            
            en.LoadGrammar(gra);
            en.SetInputToDefaultAudioDevice();
            RecognitionResult res = en.Recognize();
            String a = "";
            a = res.Text;
            en.UnloadGrammar(gra);
            

            return a;
        }

   
        // switch pour remplir chaque lignes d'un formulaire d'une tache 
        private void Rec_SpeechRecognized1(object sender, SpeechRecognizedEventArgs e)
        {
           
            pictureBox5.Show();
            pictureBox5.Location = new System.Drawing.Point(509,18);

                switch (e.Result.Text)
                {
                    case "Titre":
                        pictureBox5.Location = new  System.Drawing.Point(336, 44);
                        bunifuMaterialTextbox3.Text = stt(words, s).ToString();
                        break;

                    case "Description":
                    pictureBox5.Location = new  System.Drawing.Point(336, 88);
                    bunifuMaterialTextbox4.Text = stt(words, s).ToString();
                        break;

                    case "Durée":
                    pictureBox5.Location = new  System.Drawing.Point(378, 134);
                    /*vc = e.Result.Text;
                    switch (vc)
                        {
                            case "un":
                                MessageBox.Show("un");
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

                    }*/
                        break;

                    case "Type" :
                    pictureBox5.Location = new System.Drawing.Point(336, 172);
                   /* vc = stt(e.Result.Grammar, s).ToString();
                    switch (vc)
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
                    }*/
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

            datagridtache(2);
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

            datagridtache(0);
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            datagridtache(1);
            
            
        }

        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {
            datagridtache(2);
        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            datagridtache(0);
        }

        public int quest (DateTime date )
        {
            var all = from s in dtb.plan
                      where s.personne.id_personne == idp && s.date_heure_fin == date
                      select s.id_taches;

            int fin = all.Count();

            return  fin;
        }

        // Building, loading du grammaire du speech recognizer  
        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "Normale", "Urgente", "Habituelle", "Inhabituelle", "Titre", "Description", "Durée", "Type", "Debut", "Fin", "Emplacement", "un", "deux", "trois", "quatre" });
            GrammarBuilder b = new GrammarBuilder();
            b.Append(commands);
            Grammar gr = new Grammar(b);
            rec.LoadGrammarAsync(gr);
            rec.SetInputToDefaultAudioDevice();
            rec.RecognizeAsync(RecognizeMode.Multiple);
            rec.SpeechRecognized += Rec_SpeechRecognized1;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            bunifuCards2.Visible = true;
            bunifuCards2.BringToFront();
        }

        // creer et afficher map form
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            mapform mf = new mapform();
            mf.Show();
        }

        //planing de cette semaine
        public Array getplan()
        {
            string[,] tableplan = new string[15, 7];
            int nhour = 7;
            int nday = 1;

            try
            {
                var rar = (from g in dtb.personne
                           join tr in dtb.plan on g.id_personne equals tr.id_prop
                           join t in dtb.tache on tr.id_taches equals t.id_tache
                           where tr.id_prop == idp && tr.date_heure_debut.Value.Month == DateTime.Now.Month && tr.date_heure_debut.Value.DayOfYear / 7 == DateTime.Now.DayOfYear / 7 && tr.accomplie == 0
                           orderby tr.date_heure_debut
                           select new
                           {
                               titre = tr.tache,
                               date = tr.date_heure_debut.Value,
                               dure = t.duree

                           }).ToList();


                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 7; i++)
                    {
                        foreach (var a in rar)
                        {
                            if (Convert.ToInt32(a.date.Hour) == nhour && a.date.DayOfYear / 7 == DateTime.Now.DayOfYear / 7 && Convert.ToInt32(a.date.DayOfWeek) == nday)
                            {
                                tableplan[i,j] = a.titre.ToString();
                            }
                        }
                        nday++;
                    }
                    nhour++;

                }
                MessageBox.Show("Done !");
            }
            catch (Exception c)
            {
                MessageBox.Show(c.ToString());
            }

            return tableplan;
        }

        private void bunifuFlatButton11_Click(object sender, EventArgs e)
        {

            //getplan();
            
            
        }

        private void doc_PrintPageplan(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Bitmap bbp = new Bitmap(bunifuCards6.Width, bunifuCards6.Height, bunifuCards6.CreateGraphics());
            bunifuCards6.DrawToBitmap(bbp, new Rectangle(0, 0, bunifuCards6.Width, bunifuCards6.Height));
            RectangleF bounds = e.PageSettings.PrintableArea;
            float factor = ((float)bbp.Height / (float)bbp.Width);
            e.Graphics.DrawImage(bbp, bounds.Left, bounds.Top, bounds.Width, factor * bounds.Width);
        }

        private void doc_PrintPagestat(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Bitmap bmp = new Bitmap(bunifuCards5.Width, bunifuCards5.Height, bunifuCards5.CreateGraphics());
            bunifuCards5.DrawToBitmap(bmp, new Rectangle(0, 0, bunifuCards5.Width, bunifuCards5.Height));
            RectangleF bounds = e.PageSettings.PrintableArea;
            float factor = ((float)bmp.Height / (float)bmp.Width);
            e.Graphics.DrawImage(bmp, bounds.Left, bounds.Top, bounds.Width, factor * bounds.Width);
        }

       

   
        private void Stat_btn_Click(object sender, EventArgs e)
        {
            bunifuCards5.Show();
            bunifuCards5.BringToFront();
            stat();
        }

        private void Taches_btn_Click(object sender, EventArgs e)
        {
            bunifuCards3.Show();
            bunifuCards3.BringToFront();
        }

        private void Acceuil_btn_Click(object sender, EventArgs e)
        {
            bunifuCards1.Show();
            bunifuCards1.BringToFront();
        }

        private void Quit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            ActiveForm.Show();
            
        }
        Bitmap bitmap;
        private void printPreviewDialog1_Load(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);

        }

        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {
            tache_date(metroDateTime1.Value);
            bunifuFlatButton14.Enabled = true;
            label1.Text = metroDateTime1.Value.ToString("MMM dd yyyy");
        }

        private void bunifuFlatButton13_Click(object sender, EventArgs e)
        {
            metroDateTime1.Value = DateTime.Today;
        }

        private void bunifuFlatButton14_Click(object sender, EventArgs e)
        {
            
            System.Drawing.Printing.PrintDocument dc = new System.Drawing.Printing.PrintDocument();
            dc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(doc_PrintPageplan);
            printPreviewDialog1.Document = dc;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void bunifuFlatButton12_Click(object sender, EventArgs e)
        {
            bunifuFlatButton12.Visible = false;
            panel3.Location = new System.Drawing.Point(288, 82);
            System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();
            doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(doc_PrintPagestat);
            printPreviewDialog1.Document = doc;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
            bunifuFlatButton12.Visible = true;
            panel3.Location = new System.Drawing.Point(288, 65);
            
        }
    }    
}
