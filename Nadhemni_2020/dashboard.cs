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
            bunifuCards1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bunifuCards2.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bunifuCards3.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bunifuCards4.BringToFront();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
               label1.Location = new Point(label1.Location.X + 5, label1.Location.Y);

                if (label1.Location.X > this.Width)
                {
                    label1.Location = new Point(0 - label1.Width, label1.Location.Y);
                }
            
        }
    }
}
