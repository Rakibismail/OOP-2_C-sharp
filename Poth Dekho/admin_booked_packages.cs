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
    public partial class admin_booked_packages : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbtps"].ConnectionString;
        string username = "";
        public admin_booked_packages(string username)
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

        private void cancelTrip_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from package_register_TBL where id =@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", tripNo.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            Console.WriteLine(a);
            if (a > 0)
            {
                MessageBox.Show("Data Deleted Successfully");
                BindGridView();
                tripNo.Text = "0";
            }
            else
            {
                MessageBox.Show("Trip Not Deleted. Don't forget to double click on cell");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tripNo.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_dashboard f = new admin_dashboard(username); 
            f.Show();
        }
    }
}
