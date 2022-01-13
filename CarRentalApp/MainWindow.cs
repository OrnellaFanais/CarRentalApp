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
    public partial class MainWindow : Form
    {
        private Login _login;
        public string _roleName;
        public User _user;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(Login login, User user)
        {
            InitializeComponent();
            _login = login;
            _user = user;
            //inner join
            _roleName = user.UserRoles.FirstOrDefault().Role.ShortName;
        }

        private void addRentalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            var addRentalRecord = new AddEditRentalRecord();
            //Simple way of allowing are ensuring that only one
            //istance of the window open
            addRentalRecord.ShowDialog();
            addRentalRecord.MdiParent = this;
        }

        private void manageVehicleListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //give me a list od the open forms of all of the data type form
            var OpenForms = Application.OpenForms.Cast<Form>();
            //declare a variable where I want to say is open
            var isOpen = OpenForms.Any(q => q.Name == "ManageVehicleListing");
            if (!isOpen)
            {
                var vehicleListing = new ManageVehicleListing();
                vehicleListing.MdiParent = this;
                vehicleListing.Show();
            }
            
        }

        private void viewArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var OpenForms = Application.OpenForms.Cast<Form>();
            //declare a variable where I want to say is open
            var isOpen = OpenForms.Any(q => q.Name == "ManageRentalRecords");
            //Utils.FormIsOpen("ManageRentalRecords");
            if (!isOpen)
            {
                var manageRentalRecords = new ManageRentalRecords();
                manageRentalRecords.MdiParent = this;
                manageRentalRecords.Show();
            }
                
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _login.Close();
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var OpenForms = Application.OpenForms.Cast<Form>();
            //declare a variable where I want to say is open
            var isOpen = OpenForms.Any(q => q.Name == "ManageUsers");
            if (!isOpen)
            {
                var manageUsers = new ManageUsers();
                manageUsers.MdiParent = this;
                manageUsers.Show();
            }       
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var username = _user.Username;
            tsiLoginText.Text = $"Logged in as: {username}";
            if (_roleName != "Admin")
            {             
                manageUsersToolStripMenuItem.Visible = false;
            }
        }
    }
}
 