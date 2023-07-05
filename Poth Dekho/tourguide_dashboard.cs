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
    public partial class tourguide_dashboard : Form
    {

        int IdNo = 0;
        public tourguide_dashboard(int IdNo)
        {
            InitializeComponent();
            this.IdNo = IdNo;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            tourguide_traveller_information f = new tourguide_traveller_information();
            f.Show();
        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            tourguide_view_trip f = new tourguide_view_trip();
            f.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            tour_guide_profile_details f = new tour_guide_profile_details(IdNo);
            f.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            tour_guide_login f = new tour_guide_login();
            f.Show();
        }
    }
}
