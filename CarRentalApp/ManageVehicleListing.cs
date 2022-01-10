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

            //var cars = _db.Cars
            //    .Select(q => new
            //    {
            //        Make = q.Make,
            //        Model = q.Model,
            //        Vin = q.Vin,
            //        Year = q.Year,
            //        LicensePlateNumber = q.LicensePlateNumber,
            //        q.Id
            //    })
            //    .ToList();
            //gvVehicleList.DataSource = cars;
            //gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            //gvVehicleList.Columns[5].Visible = false;
            try
            {
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Error: {ex.Message}");
            }
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            AddEditVehicle addEditVehicle = new AddEditVehicle();
            addEditVehicle.MdiParent = this.MdiParent;
            addEditVehicle.Show();
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try
            {
                // get Id of sellected row = when click to row
                // give me the selected the first selected row, and give me 
                // the cell call id (it's column) and the value it's not
                //visible 
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                //query database for record
                var car = _db.Cars.FirstOrDefault(q => q.Id == id);

                //launch AddEditVehicle window with data
                var addEditVehicle = new AddEditVehicle(car);
                addEditVehicle.MdiParent = this.MdiParent;
                addEditVehicle.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Error: {ex.Message}");
            }
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                // get Id of sellected row = when click to row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                //query database for record
                var car = _db.Cars.FirstOrDefault(q => q.Id == id);

                //delete vehicle from table
                _db.Cars.Remove(car);
                _db.SaveChanges();

                gvVehicleList.Refresh();  
            }
            catch (Exception ex)
            {

                MessageBox.Show($" Error: {ex.Message}");
            }
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Simple Refresh Option
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            var cars = _db.Cars
                .Select(q => new
                {
                    Make = q.Make,
                    Model = q.Model,
                    Vin = q.Vin,
                    Year = q.Year,
                    LicensePlateNumber = q.LicensePlateNumber,
                    q.Id
                })
                .ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            gvVehicleList.Columns["Id"].Visible = false;
        }
    }
}
