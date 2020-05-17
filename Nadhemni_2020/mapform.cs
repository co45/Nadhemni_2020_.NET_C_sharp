using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;




namespace Nadhemni_2020
{
    public partial class mapform : Form
    {
        GMarkerGoogle marker;
        GMapOverlay mo;

        private static string code;
        double lat = 36.801752;
        double lang = 10.1470843;

        public static string Code { get => code; set => code = value; }

        public mapform()
        {
            InitializeComponent();
        }

        private void mapform_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.Position = new PointLatLng(lat, lang);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 30;
            gMapControl1.Zoom = 10;
            gMapControl1.AutoScroll = true;

            mo = new GMapOverlay("pin");
            marker = new GMarkerGoogle(new PointLatLng(lat, lang), GMarkerGoogleType.red_small);
            mo.Markers.Add(marker);

            gMapControl1.Overlays.Add(mo);


        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            
        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
             double a=gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
             double b= gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
            
           
            code = a.ToString() + "-" + b.ToString(); 
            label3.Text = code;
            marker.Position = new PointLatLng(a,b );

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
