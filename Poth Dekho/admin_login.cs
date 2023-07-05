using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace Poth_Dekho
{
    public partial class admin_login : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbrrs"].ConnectionString;

        Thread th;
        string username = "";

        public admin_login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                username = textBox1.Text;
                SqlConnection con = new SqlConnection(cs);
                string query = "select * from AdminReg where Username = @username and password = @pass";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //ResetControl();
                    this.Close();
                    th = new Thread(openadmindashboard);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                else
                {
                    MessageBox.Show("Username or Password is Invalid", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //ResetControl();
                }

                con.Close();

            }
            else
            {
                MessageBox.Show("Fill the textFiled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void openadmindashboard()
        {

            
            Application.Run(new admin_dashboard(username));
            

        }

        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            home f = new home();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Reg f = new Admin_Reg();
            f.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            bool status = checkBox1.Checked;
            switch (status)
            {
                case true:
                    textBox2.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox2.UseSystemPasswordChar = true;
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

            textBox2.Clear();
          
        }
    }
}
