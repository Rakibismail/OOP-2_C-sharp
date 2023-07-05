using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Poth_Dekho
{
    public partial class tourguide_view_trip : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbtps"].ConnectionString;
        string username = "";
        int IdNo = 0;
        public tourguide_view_trip()
        {
            InitializeComponent();
            this.username = username;
            BindGridView();
        }

        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);

            string query = "select * from package_register_TBL";

            SqlDataAdapter sda = new SqlDataAdapter(query, con);


            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            //DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            //dgv = (DataGridViewImageColumn)dataGridView1.Columns[4];
            //dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.RowTemplate.Height = 80;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

     

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            tourguide_dashboard f = new tourguide_dashboard(IdNo);
            f.Show();
        }
    }
}
