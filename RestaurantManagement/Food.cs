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
    public partial class Food : Form
    {
        public Food()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();

                SqlCommand getMaxIdCmd = new SqlCommand("SELECT ISNULL(MAX(yemekId), 0) + 1 FROM foodtab", con);
                int newYemekId = (int)getMaxIdCmd.ExecuteScalar();

                SqlCommand cmd = new SqlCommand("spInsertFood", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@yemekId", newYemekId);
                cmd.Parameters.AddWithValue("@yemekAdı", textBox2.Text);
                cmd.Parameters.AddWithValue("@fiyat", decimal.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@miktar", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@durum", textBox5.Text);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt basarıyla alındı", "Basarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetAllFood", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spUpdateFood", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@yemekId", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@yemekAdı", textBox2.Text);
                cmd.Parameters.AddWithValue("@fiyat", decimal.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@miktar", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@durum", textBox5.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt başarıyla güncellendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spDeleteFood", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@yemekId", int.Parse(textBox1.Text));

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt başarıyla silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnDisplay_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from foodtab", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
        }

        private void Food_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from foodtab", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
