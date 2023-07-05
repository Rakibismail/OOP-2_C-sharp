using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace Poth_Dekho
{
    public partial class Choose_package : Form
    {
        int travellerId;

        string cs = ConfigurationManager.ConnectionStrings["dbtps"].ConnectionString;
        Thread th; 

        public Choose_package(int travellerId)
        {
            InitializeComponent();
            BindGridView();

            this.travellerId = travellerId;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Update_pakage_Load(object sender, EventArgs e)
        {

        }

        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Tourpackage_TBL";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[4];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.RowTemplate.Height = 80;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            destination.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            duration.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            price.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            packageId.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            pictureBox1.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[4].Value);
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into package_register_TBL values (@travellerId,@price,@packageId,@destination,@duration)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@destination", destination.Text);
            cmd.Parameters.AddWithValue("@duration", duration.Text);
            cmd.Parameters.AddWithValue("@packageId", packageId.Text);
            cmd.Parameters.AddWithValue("@travellerId", travellerId);
            cmd.Parameters.AddWithValue("@price", price.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Package enrolled Successfully");
                this.Hide();
                booked_trip f = new booked_trip(travellerId);
                f.Show();

            }
            else
            {
                MessageBox.Show("Data Not Inserted");
            }


            con.Close();
        }
    }
}
