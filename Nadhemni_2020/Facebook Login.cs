using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;

namespace Nadhemni_2020
{
    public partial class Facebook_Login : Form
    {
        private const string AppId = "223067295640955";
        private Uri _loginUrl;
        private const string _ExtendedPermissions = "user_about_me, publish_stream, offline_access";
        
            FacebookClient fbClient = new FacebookClient();

        public Facebook_Login()
        {
            InitializeComponent();
        }

        private void Facebook_Login_Load(object sender, EventArgs e)
        {
            Login();
        }

        public void Login()
        {
            dynamic parameters = new ExpandoObject();
            parameters.client_id = AppId;
            parameters.redirect_uri = "https://www.facebook.com/connect/login_success.html";

            parameters.response_type = "token";

            parameters.display = "popup";

            if (!string.IsNullOrWhiteSpace(_ExtendedPermissions))
                parameters.scope = _ExtendedPermissions;

            var fb = new FacebookClient();
            _loginUrl = fb.GetLoginUrl(parameters);
            webBrowserLogin.Navigate(_loginUrl.AbsoluteUri);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
