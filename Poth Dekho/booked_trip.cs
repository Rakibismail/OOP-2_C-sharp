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
    public partial class booked_trip : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbtps"].ConnectionString;
        int travellerId;
        public booked_trip(int travellerId)
        {
            InitializeComponent();
            this.travellerId = travellerId;
            BindGridView();
            
            
        }


        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            
            string query = "select * from package_register_TBL where travellerId='"+travellerId+"'";
            
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tripNo.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
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
                MessageBox.Show("Data Not Deleted");
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            traveller_dashboard f = new traveller_dashboard(travellerId); // travellerId need to insert
            f.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
