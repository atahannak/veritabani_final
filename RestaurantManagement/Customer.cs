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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RestaurantManagement
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
         
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
                {
                    con.Open();

                    // Get the next unique MusteriId
                    SqlCommand getMaxIdCmd = new SqlCommand("SELECT ISNULL(MAX(MusteriId), 0) + 1 FROM customertab", con);
                    int newMusteriId = (int)getMaxIdCmd.ExecuteScalar();

                    // Call the stored procedure to insert a new customer
                    SqlCommand cmd = new SqlCommand("spInsertCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MusteriId", newMusteriId);
                    cmd.Parameters.AddWithValue("@MusteriAdi", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Telefon", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox4.Text);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla alındı", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
            }


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spGetAllCustomers", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }


        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spUpdateCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MusteriId", int.Parse(textBox1.Text));
                    cmd.Parameters.AddWithValue("@MusteriAdi", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Telefon", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox4.Text);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla güncellendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spDeleteCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MusteriId", int.Parse(textBox1.Text));

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
            }


        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from customertab", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                }
            }
        }

        private void Customer_Load(object sender, EventArgs e)
        {
       
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from customertab", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                }
            }
        }
    }
}
