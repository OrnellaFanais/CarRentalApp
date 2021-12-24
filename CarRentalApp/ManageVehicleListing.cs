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
    public partial class ManageVehicleListing : Form
    {
        private readonly CarRentalEntities _db;
        public ManageVehicleListing()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            //Select * From Cars
            //var cars = _db.Cars.ToList(); 

            //Select Id as CarId, name as CarName from Cars
            //var cars = _db.Cars
            //    .Select(q => new { CarId = q.Id, CarName = q.Make })
            //    .ToList();
            //gvVehicleList.DataSource = cars;
            //gvVehicleList.Columns[0].HeaderText = "ID";
            //gvVehicleList.Columns[1].HeaderText = "Make";

            var cars = _db.Cars
                .Select(q => new 
                {
                    Make = q.Make, 
                    Model = q.Model, 
                    Vin = q.Vin, 
                    Year = q.Year, 
                    LicensePlateNumber = q.LicensePlateNumber
                })
                .ToList();
            gvVehicleList.DataSource = cars;
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {

        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {

        }
    }
}
