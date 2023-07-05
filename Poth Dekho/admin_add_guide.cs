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

namespace Poth_Dekho
{
    public partial class admin_add_guide : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbtps"].ConnectionString;
        string username = "";

        public admin_add_guide(string username)
        {
            InitializeComponent();
            this.username = username;
        }

      

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_guide_information f = new admin_guide_information(username);
            f.Show();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose Image";

            ofd.Filter = "ALL IMAGE FILE (*.*) | *.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void btnAddNewGuide_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into TourGuideReg values (@UserName,@Email,@IdNo,@Phone,@Password,@Image)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserName", textBox5.Text);
            cmd.Parameters.AddWithValue("@Email", textBox4.Text);
            cmd.Parameters.AddWithValue("@IdNo", textBox3.Text);
            cmd.Parameters.AddWithValue("@Phone", textBox2.Text);
            cmd.Parameters.AddWithValue("@Password", textBox1.Text);
            cmd.Parameters.AddWithValue("@Image", SavePhoto());



            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Traveller added Successfully");
                //BindGridView();
                //ResetControl();
            }
            else
            {
                MessageBox.Show("Traveller Not Added");
            }


            con.Close();
        }
    }
}
