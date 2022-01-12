using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class Login : Form
    {
        private readonly CarRentalEntities _db;
        public Login()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //Add encryption
                SHA256 sha = SHA256.Create();

                var username = tbUsername.Text.Trim();
                var password = tbPassword.Text;


                // assign the value to a password or to a variable 
                var hashed_password = Utils.HashPassword(password);                    

                //Check for matching username, password and active flag
                var user = _db.Users.FirstOrDefault(q => q.Username == username && q.Password == hashed_password
                            && q.IsActive == true);
                if (user == null )
                {
                    MessageBox.Show("Please provide valid credentials");
                }
                else
                {
                    //var role = user.UserRoles.FirstOrDefault();
                    //var roleShortName = role.Role.ShortName; 
                    //var mainWindow = new MainWindow(this, roleShortName);
                    var mainWindow = new MainWindow(this, user);
                    mainWindow.Show();
                    Hide();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong. Please try again");
            }
        }
    }
}
