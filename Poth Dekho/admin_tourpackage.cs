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
using System.Threading;
using System.IO;


namespace Poth_Dekho
{
    public partial class admin_tourpackage : Form
    {

        string cs = ConfigurationManager.ConnectionStrings["dbtps"].ConnectionString;

        Thread th;
        string username = "";


        public admin_tourpackage(string username)
        {
            InitializeComponent();
            this.username = username;
            BindGridView();
            ResetControl();
            //BindGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "ALL IMAGE FILE (*.*) | *.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into Tourpackage_TBL values (@Destination,@Duration,@Price,@PackageId,@Image)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Destination", textBox4.Text);
            cmd.Parameters.AddWithValue("@Duration", textBox3.Text);
            cmd.Parameters.AddWithValue("@Price", textBox2.Text);
            cmd.Parameters.AddWithValue("@PackageId", textBox1.Text);
            cmd.Parameters.AddWithValue("@Image", SavePhoto());



            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Saved Successfully");
                BindGridView();
                ResetControl();
            }
            else
            {
                MessageBox.Show("Data Not Inserted");
            }


            con.Close();
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update Tourpackage_TBL set Destination =@Destination, Duration=@Duration, Price=@Price , PackageId=@PackageId , Image = @Image where PackageId =@PackageId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Destination", textBox4.Text);
            cmd.Parameters.AddWithValue("@Duration", textBox3.Text);
            cmd.Parameters.AddWithValue("@Price", textBox2.Text);
            cmd.Parameters.AddWithValue("@PackageId", textBox1.Text);
            cmd.Parameters.AddWithValue("@Image", SavePhoto());

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Updated Successfully");
                BindGridView();
                ResetControl();
            }
            else
            {
                MessageBox.Show("Data Not Updated");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from Tourpackage_TBL where PackageId =@PackageId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@PackageId", textBox1.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            Console.WriteLine(a);
            if (a > 0)
            {
                MessageBox.Show("Data Deleted Successfully");
                BindGridView();
                ResetControl();
            }
            else
            {
                MessageBox.Show("Data Not Deleted");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            pictureBox1.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[4].Value);
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }




        void ResetControl()
        {
            textBox4.Clear();
            textBox3.Clear();
            textBox2.Clear();
            textBox1.Clear();
            pictureBox1.Image = Properties.Resources.no_image_available;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_dashboard f = new admin_dashboard(username); 
            f.Show();
        }
    }
}
