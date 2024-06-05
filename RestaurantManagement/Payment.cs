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
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int siparisId = int.Parse(cmboxSiparisId.Text);

            int toplamTutar = 0;

            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT SUM(f.Fiyat) as Tutar FROM ordertab as o LEFT JOIN foodtab as f ON o.YemekID = f.YemekId WHERE o.SiparisID = @SiparisID GROUP BY o.SiparisID", con);
                cmd.Parameters.AddWithValue("@SiparisID", siparisId);

                SqlDataReader reader = cmd.ExecuteReader();

                // Check if there are any results returned by the query
                if (reader.Read())
                {
                    // Ensure the result is not null before adding to toplamTutar
                    if (!reader.IsDBNull(reader.GetOrdinal("Tutar")))
                    {
                        toplamTutar += reader.GetInt32(reader.GetOrdinal("Tutar"));
                    }
                }

                reader.Close();

                con.Close();
            }

            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand("spAddPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parametreleri ekleyin

                cmd.Parameters.AddWithValue("@SiparisId", siparisId);
                cmd.Parameters.AddWithValue("@OdemeSekli", txtOdemeSekli.Text);
                cmd.Parameters.AddWithValue("@Tutar", toplamTutar);
                cmd.Parameters.AddWithValue("@MusteriId", cmboxMusteriId.SelectedIndex + 1);


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

        private void btnAdd_Click(object sender, EventArgs e)
        {

            loadPayments();

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
                txtOdemeId.Text = "";
                txtOdemeSekli.Text = "";
                    cmboxMusteriId.Text = "";
                cmboxSiparisId.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            int siparisId = int.Parse(cmboxSiparisId.Text);

            int toplamTutar = 0;

            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT SUM(f.Fiyat) as Tutar FROM ordertab as o LEFT JOIN foodtab as f ON o.YemekID = f.YemekId WHERE o.SiparisID = @SiparisID GROUP BY o.SiparisID", con);
                cmd.Parameters.AddWithValue("@SiparisID", siparisId);

                SqlDataReader reader = cmd.ExecuteReader();

                // Check if there are any results returned by the query
                if (reader.Read())
                {
                    // Ensure the result is not null before adding to toplamTutar
                    if (!reader.IsDBNull(reader.GetOrdinal("Tutar")))
                    {
                        toplamTutar += reader.GetInt32(reader.GetOrdinal("Tutar"));
                    }
                }

                reader.Close();

                con.Close();
            }

            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
                {
                    con.Open();

                   
                    SqlCommand cmd = new SqlCommand("spUpdatePayment", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", int.Parse(txtOdemeId.Text));
                    cmd.Parameters.AddWithValue("@SiparisId", siparisId);
                    cmd.Parameters.AddWithValue("@OdemeSekli", txtOdemeSekli.Text);
                    cmd.Parameters.AddWithValue("@Tutar", toplamTutar);
                    cmd.Parameters.AddWithValue("@MusteriId", cmboxMusteriId.SelectedIndex + 1);


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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
                {
                    con.Open();

                   
                    SqlCommand cmd = new SqlCommand("spDeletePayment", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                   
                    cmd.Parameters.AddWithValue("@Id", int.Parse(txtOdemeId.Text));

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
           
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
                {
                    con.Open();

                    
                    SqlCommand cmd = new SqlCommand("spGetAllPayments", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                  
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    
                    da.Fill(dt);

                    
                    dataGridView1.DataSource = dt;

                    con.Close();
                }
            }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void getAllOrders()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("spGetAllOrders", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Assuming the food name is in the first column
                    string siparisID = reader["Siparis ID"].ToString();
                    cmboxSiparisId.Items.Add(siparisID);
                }

                reader.Close();

                con.Close();
            }
        }

        private void getAllCustomers()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("spGetAllCustomers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Assuming the food name is in the first column
                    string customerName = reader["MusteriAdi"].ToString();
                    cmboxMusteriId.Items.Add(customerName);
                }

                reader.Close();

                con.Close();
            }
        }

        private void loadPayments()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3M860B9\SQLEXPRESS;Initial Catalog=restaurantdb;Integrated Security=True;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetAllPayments", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            getAllCustomers();
            getAllOrders();
            loadPayments();
        }
    }
    }
