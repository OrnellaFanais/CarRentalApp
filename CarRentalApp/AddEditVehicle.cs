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
    public partial class AddEditVehicle : Form
    {
        private bool isEditMode;
        private readonly CarRentalEntities _db;
        public AddEditVehicle()
        {
            InitializeComponent();
            lblTitle.Text = "Add new Vehicle";
            isEditMode = false;
            _db = new CarRentalEntities();
        }

        public AddEditVehicle(Car carToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle";
            this.Text = "Edit Vehicle";
            if (carToEdit == null)
            {
                MessageBox.Show("Please ensure that you select a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalEntities();
                PopulateFields(carToEdit);
            }            
        }

        private void PopulateFields(Car car)
        {
            lblId.Text = car.Id.ToString();
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVin.Text = car.Vin;
            tbLicenseNum.Text = car.LicensePlateNumber;
            tbYear.Text = car.Year.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (isEditMode == true)
            if (isEditMode)
            {
                // Edit code here
                var id = int.Parse(lblId.Text);
                var car = _db.Cars.FirstOrDefault(q => q.Id == id);
                car.Model = tbModel.Text;
                car.Make = tbMake.Text;
                car.Vin = tbVin.Text;
                car.Year = int.Parse(tbYear.Text);
                car.LicensePlateNumber = tbLicenseNum.Text;

                _db.SaveChanges();
                this.Close();

            }
            else
            {
                // Add code here
                try
                {
                    var newCar = new Car
                    {
                        LicensePlateNumber = tbLicenseNum.Text,
                        Make = tbMake.Text,
                        Model = tbModel.Text,
                        Vin = tbVin.Text,
                        Year = int.Parse(tbYear.Text)
                    };

                    _db.Cars.Add(newCar);
                    _db.SaveChanges();
                    this.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Error: {ex.Message} \n\t All fields need to be completed!");
                }
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // this = AddEditVehicle
            //this.Close();
            Close();
        }
    }
}
