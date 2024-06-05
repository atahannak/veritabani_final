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

    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePicker1.CustomFormat = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();

                int newSiparisID = 0;

                if(txtSiparisID.Text != null && txtSiparisID.Text != "")
                {
                    newSiparisID = int.Parse(txtSiparisID.Text);
                }
                else
                {
                    SqlCommand getMaxIdCmd = new SqlCommand("SELECT ISNULL(MAX(SiparisID), 0) + 1 FROM ordertab", con);
                    newSiparisID = (int)getMaxIdCmd.ExecuteScalar();
                }


                SqlCommand cmd = new SqlCommand("spInsertOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SiparisID", newSiparisID);
                cmd.Parameters.AddWithValue("@YemekID", cmBoxYemekList.SelectedIndex + 1);
                cmd.Parameters.AddWithValue("@SiparisZamanı", DateTime.Now);
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

                SqlCommand cmd = new SqlCommand("spGetAllOrders", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            {
                txtSiparisID.Text = "";
                cmBoxYemekList.Text = "";

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("spDeleteOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SiparisID", int.Parse(txtSiparisID.Text));

                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Kayıt başarıyla silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            getAllOrders();


        }

        private void getAllOrders()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("spGetAllOrders", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Console.WriteLine(da.ToString());
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();
            }
        }

        private void getAllFoods()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("spGetAllFood", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Assuming the food name is in the first column
                    string foodName = reader["YemekAdı"].ToString();
                    cmBoxYemekList.Items.Add(foodName);
                }

                reader.Close();

                con.Close();
            }
        } 

        private void Order_Load(object sender, EventArgs e)
        {
            cmBoxYemekList.Text = "Bir yemek seçiniz";
            getAllFoods();
            getAllOrders();

        }

    }
}
