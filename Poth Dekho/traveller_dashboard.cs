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
    public partial class traveller_dashboard : Form
    {
        int travellerId=0;
        public traveller_dashboard(int travellerId)
        {
            InitializeComponent();
            this.travellerId = travellerId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Choose_package f = new Choose_package( travellerId);
            f.Show();
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
 
            this.Hide();
            booked_trip f = new booked_trip(travellerId);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            traveller_view_profile f = new traveller_view_profile(travellerId);
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            traveller_log_in f = new traveller_log_in();
            f.Show();
        }
    }
}
