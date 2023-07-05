using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poth_Dekho
{
    public partial class admin_dashboard : Form
    {
        string username="";
        public admin_dashboard(string user)
        {
            InitializeComponent();
            this.username = user;
        }

        private void btnPackages_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_tourpackage f = new admin_tourpackage(username);
            f.Show();
        }

        private void btnTraveller_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_traveller_information f = new admin_traveller_information(username);
            f.Show();
        }

        private void btnBookedPackages_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_booked_packages f = new admin_booked_packages(username);
            f.Show();
        }

        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_view_profile f = new admin_view_profile(username);
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            username = "";
            admin_login f = new admin_login();
            f.Show();
        }

        private void btnTourGuide_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            admin_guide_information f = new admin_guide_information(username);
            f.Show();
        }
    }
}
