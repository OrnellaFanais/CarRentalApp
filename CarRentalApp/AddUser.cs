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
    public partial class AddUser : Form
    {
        private readonly CarRentalEntities _db;
        public AddUser()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            var roles = _db.Roles.ToList();

            cbRoles.DataSource = roles;
            cbRoles.ValueMember = "Id";
            cbRoles.DisplayMember = "Name";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //create the new user
                var username = tbUsername.Text;
                var roleId = (int)cbRoles.SelectedValue;
                var password = Utils.DefaultHashedPassword();
                var user = new User
                {
                    Username = username,
                    Password = password,
                    IsActive = true
                };
                _db.Users.Add(user);
                _db.SaveChanges();

                //Fetch an Id
                var userId = user.Id;

                //Assign it to the role
                var userRole = new UserRole
                {
                    RoleId = roleId,
                    UserId = userId
                };
                _db.UserRoles.Add(userRole);
                _db.SaveChanges();

                MessageBox.Show($"New User {user.Username} added successfully");
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("An error has occured.");
            }
        }
    }
}
