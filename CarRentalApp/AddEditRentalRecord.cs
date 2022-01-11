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
    public partial class AddEditRentalRecord : Form
    {
        private bool isEditMode;
        private ManageRentalRecords _manageRentalRecords;
        //carRentalEntities is going to give me access to every single entity
        //that is inside of my model or every table.
        //I would of basically established an instance of connection
        //to my database through the declaration of this property
        private readonly CarRentalEntities _db;
        public AddEditRentalRecord(ManageRentalRecords manageRentalRecords = null)
        {
            InitializeComponent();
            lblTitleRecord.Text = "Add new Rental Record";
            this.Text = "Add new Rental Record";
            isEditMode = false;
            _manageRentalRecords = manageRentalRecords;
            _db = new CarRentalEntities();
            
        }
        public AddEditRentalRecord(CarRentalRecord recordToEdit, ManageRentalRecords manageRentalRecords = null)
        {
            InitializeComponent();
            lblTitleRecord.Text = "Edit Rental Record";
            this.Text = "Edit Rental Record";
            _manageRentalRecords = manageRentalRecords;
            if (recordToEdit == null)
            {
                MessageBox.Show("Please ensure that you select a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalEntities();
                PopulateFields(recordToEdit);
            }
        }

        private void PopulateFields(CarRentalRecord recordToEdit)
        {
            tbCustomerName.Text = recordToEdit.CustomerName;
            dtRented.Value = (DateTime)recordToEdit.DateRented;
            dtReturned.Value = (DateTime)recordToEdit.DateReturned;
            tbCost.Text = recordToEdit.Cost.ToString();
            lblRecordId.Text = recordToEdit.Id.ToString();
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
                    //Declare an object of th record to be added
                    var rentalRecord = new CarRentalRecord();
                    //if in editMode, the get the ID and retrieve the record from the database 
                    //and place the secult in the record object
                    if (isEditMode)
                    {
                        var id = int.Parse(lblRecordId.Text);
                        rentalRecord = _db.CarRentalRecords.FirstOrDefault(q => q.Id == id);                      
                    }
                    //Populate record object with value from the form
                    rentalRecord.CustomerName = customerName;
                    rentalRecord.DateRented = dateOut;
                    rentalRecord.DateReturned = dateIn;
                    rentalRecord.Cost = (decimal)cost;
                    rentalRecord.TypeOfCarId = (int)cbTypeOfCar.SelectedValue;
                    //if not in edit mode, then add the record object to the db
                    if (!isEditMode)
                    {
                        _db.CarRentalRecords.Add(rentalRecord);
                    }
                    //sae changes made to the entity
                    _db.SaveChanges();
                    _manageRentalRecords.PopulateGrid();
                    MessageBox.Show($"Customer name: {tbCustomerName.Text} \n\r " +
                        $"Date rented: {dateOut} \n\r" +
                        $"Date renturned: {dateIn} \n\r" +
                        $"Cost: {cost} \n\r" +
                        $"Car Type: {carType} \n\r" +
                        $"THANK YOU FOR YOUR BUSINESS");                   
                    Close();
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //when I want to call in the database I use
            // that object was previously declared and initialized
            //Select * from Cars (I'm querying the database for the list
            //of cars
            //var cars = carRentalEntities.Cars.ToList();
            var cars = _db.Cars
                .Select(q => new
                {
                    Id = q.Id,
                    Name = q.Make + " " + q.Model
                })
                .ToList();
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
