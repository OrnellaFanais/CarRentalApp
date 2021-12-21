using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //I am Able to generate variable and then store the value
            //from the control inside the variable
            string customerName = tbCustomerName.Text;
            //Convert the datas into strings
            string dateOut = dtRented.Value.ToString();
            string dateIn = dtReturned.Value.ToString();

            var carType = cbTypeOfCar.SelectedItem.ToString();
            
            MessageBox.Show($"Customer name: {tbCustomerName.Text} \n\r " +
                $"Date rented: {dateOut} \n\r" +
                $"Date renturned: {dateIn} \n\r" +
                $"Car Type: {carType} \n\r" +
                $"THANK YOU FOR YOUR BUSINESS");
        }
    }
}
