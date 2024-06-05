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

namespace RestaurantManagement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
                {
                    con.Open();

                    string username = txtUsername.Text;
                    string password = txtPassword.Text;

                    using (SqlCommand cmd = new SqlCommand("ValidateUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int isValid = (int)cmd.ExecuteScalar();

                        if (isValid == 1)
                        {
                            Main mn = new Main();
                            mn.Show();
                        }
                        else
                        {
                            MessageBox.Show("Yanlış Kullanıcı Adı veya Şifre");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}




