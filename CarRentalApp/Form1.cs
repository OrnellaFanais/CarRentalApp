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
        //carRentalEntities is going to give me access to every single entity
        //that is inside of my model or every table.
        //I would of basically established an instance of connection
        //to my database through the declaration of this property
        private readonly CarRentalEntities carRentalEntities;
        public Form1()
        {
            InitializeComponent();
            //I initialized it in the constructor
            carRentalEntities = new CarRentalEntities();
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
            try
            {
                //I am Able to generate variable and then store the value
                //from the control inside the variable
                string customerName = tbCustomerName.Text;

                var dateOut = dtRented.Value;
                var dateIn = dtReturned.Value;
                double cost = Convert.ToDouble(tbCost.Text);
                var carType = cbTypeOfCar.Text;

                var isValid = true;
                var errorMessage = "";

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(carType))
                {
                    isValid = false;
                    errorMessage += "Error: Please enter missing data.\n\r";
                }

                if (dateOut > dateIn)
                {
                    isValid = false;
                    errorMessage += "Error: Illegal Date Selection.\n\r";
                }

                //if (isValid == true)
                if (isValid)
                {
                    
                    //This class directly matches the car into a card table that we created
                    var rentalRecord = new CarRentalRecord();

                    //the value of the customer name value that I just collected from my form
                    //the textbox store in customer name in the winforms
                    rentalRecord.CustomerName = customerName;
                    rentalRecord.DateRented = dateOut;
                    rentalRecord.DateReturned = dateIn;
                    rentalRecord.Cost = (decimal)cost;
                    rentalRecord.TypeOfCarId = (int)cbTypeOfCar.SelectedValue;

                    carRentalEntities.CarRentalRecords.Add(rentalRecord);
                    carRentalEntities.SaveChanges();

                    MessageBox.Show($"Customer name: {tbCustomerName.Text} \n\r " +
                    $"Date rented: {dateOut} \n\r" +
                    $"Date renturned: {dateIn} \n\r" +
                    $"Cost: {cost} \n\r" +
                    $"Car Type: {carType} \n\r" +
                    $"THANK YOU FOR YOUR BUSINESS");
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //when I want to call in the database I use
            // that object was previously declared and initialized
            //Select * from Cars (I'm querying the database for the list
            //of cars
            var cars = carRentalEntities.Cars.ToList();

            //call our combo box (displayMember id the takes that you see
            //I want my combo box whatever data source
            cbTypeOfCar.DisplayMember = "Name";
            
            // set the value member to be Id (stored the Id)
            cbTypeOfCar.ValueMember = "Id";
            
            //set the data source to be the car. I'm going to set
            //that this list od cars with Id and name sould be 
            //the souce for the list of items coming into this combo box
            cbTypeOfCar.DataSource = cars;
        }
    }
}
