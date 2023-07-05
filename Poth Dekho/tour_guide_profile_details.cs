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
using System.Drawing;

namespace Poth_Dekho
{
    public partial class tour_guide_profile_details : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbtps"].ConnectionString;
        int IdNo = 0;
        public tour_guide_profile_details(int IdNo)
        {
            InitializeComponent();
            this.IdNo = IdNo;


            reload();
        }

        void reload()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from TourGuideReg where IdNo=@IdNo";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@IdNo", IdNo);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == true)
            {
                //MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ResetControl();

                while (reader.Read())
                {
                    byte[] img = (byte[])reader["Image"];
                    textBox5.Text = (string)reader["Username"];
                    textBox4.Text = (string)reader["Email"];
                    textBox2.Text = (string)reader["Phone"];
                    textBox1.Text = (string)reader["Password"];

                    if (img == null)
                    {
                        MessageBox.Show("image not available");
                    }
                    else
                    {
                        pictureBox1.Image = byte_ToImage(img);

                    }
                }



            }
            else
            {
                MessageBox.Show("Username or Password is Invalid", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ResetControl();
            }
            reader.Close();
            con.Close();

        }

        public static Image byte_ToImage(byte[] img_byte)
        {
            Image img = null;
            if (img_byte != null)
            {
                MemoryStream mstm = new MemoryStream(img_byte);
                img = Image.FromStream(mstm);
            }

            return img;
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(cs);
            string query = "update TourGuideReg set Username=@username, Email=@email, Phone=@phone, Password=@password , Image = @Image where IdNo=@IdNo";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", textBox5.Text);
            cmd.Parameters.AddWithValue("@IdNo", IdNo);
            cmd.Parameters.AddWithValue("@email", textBox4.Text);
            cmd.Parameters.AddWithValue("@phone", textBox2.Text);
            cmd.Parameters.AddWithValue("@password", textBox1.Text);
            cmd.Parameters.AddWithValue("@Image", SavePhoto());

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Updated Successfully");

                reload();

            }
            else
            {
                MessageBox.Show("Data Not Updated");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            tourguide_dashboard f = new tourguide_dashboard(IdNo);
            f.Show();
        }
    }
}
