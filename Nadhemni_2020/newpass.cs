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
    public partial class newpass : Form
    {
        DataClassesDataContext db = new DataClassesDataContext();
        

        public newpass()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Control x; 
            try
            {
                user us = new user();

                us.login = bunifuMaterialTextbox1.Text;
                us.mdp = bunifuMaterialTextbox2.Text;
                db.user.InsertOnSubmit(us);
                db.SubmitChanges();
                
                dashboard d = new dashboard();
                d.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
