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
    public partial class traveller_create_account : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbrtrs"].ConnectionString;
        public traveller_create_account()
        {
            InitializeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            //ofd.Filter = "PNG FILE (*.PNG) | *.PNG";
            ofd.Filter = "ALL IMAGE FILE (*.*) | *.*";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (!Authenticate())
            //{
            //    MessageBox.Show("Do not keep any textbox blank!");
            //    return;
            //}

            SqlConnection con = new SqlConnection(cs);
            string query = "insert into TravellerRegi values (@UserName,@Email,@Phone,@Password,@ConfirmPassword,@Image)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserName", textBox5.Text);
            cmd.Parameters.AddWithValue("@Email", textBox4.Text);
            cmd.Parameters.AddWithValue("@Phone", textBox3.Text);
            cmd.Parameters.AddWithValue("@Password", textBox2.Text);
            cmd.Parameters.AddWithValue("@ConfirmPassword", textBox1.Text);
            cmd.Parameters.AddWithValue("@Image", SavePhoto());



            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Saved Successfully");
                //BindGridView();
                //ResetControl();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            traveller_log_in f = new traveller_log_in();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            traveller_log_in f = new traveller_log_in();
            f.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
            switch (status)
            {
                case true:
                    textBox1.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox1.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetControl();
        }
        void ResetControl()
        {
            textBox1.Clear();
            textBox3.Clear();
            textBox2.Clear();
            textBox4.Clear();
            textBox5.Clear();
            pictureBox1.Image = Properties.Resources.no_image_available;
        }
    }
}
