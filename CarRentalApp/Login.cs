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

                // Convert the input string to a byte array and compute the hash
                byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Create a new stringbuilder to collect the byte
                //and create a string
                StringBuilder sBuiler = new StringBuilder();

                //For loop through each byte of the hashed data and format
                //each one as a hexadecimal string
                for (int i = 0; i < data.Length; i++)
                {
                    sBuiler.Append(data[i].ToString("x2"));
                }

                // assign the value to a password or to a variable 
                var hashed_password = sBuiler.ToString();

                var user = _db.Users.FirstOrDefault(q => q.Username == username && q.Password == hashed_password);
                if (user == null )
                {
                    MessageBox.Show("Please provide valid credentials");
                }
                else
                {
                    var mainWindow = new MainWindow(this);
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
