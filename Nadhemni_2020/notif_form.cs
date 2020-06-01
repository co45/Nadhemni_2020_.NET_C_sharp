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
    public partial class notif_form : Form
    {
        int x, y;

        public notif_form(string c)
        {
            InitializeComponent();
            label1.Text = c;
        }

        private void notif_form_Load(object sender, EventArgs e)
        {
            x = Screen.PrimaryScreen.WorkingArea.Width - this.Width  ;
            y = Screen.PrimaryScreen.WorkingArea.Height - this.Height  ;
            this.Location = new Point(x, y);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
