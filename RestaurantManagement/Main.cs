using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagement
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Food fd = new Food();
            fd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Customer cr = new Customer();   
            cr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Show(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Payment payment = new Payment();
            payment.Show();
        }
    }
}
