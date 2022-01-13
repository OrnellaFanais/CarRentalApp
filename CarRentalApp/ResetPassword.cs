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
    public partial class ResetPassword : Form
    {
        private readonly CarRentalEntities _db;
        private User _user;

        public ResetPassword(User user)
        {
            InitializeComponent();
            _db = new CarRentalEntities();
            _user = user;
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                var password = tbNewPassword.Text;
                var confirmPassword = tbConfirmPassword.Text;
                var user = _db.Users.FirstOrDefault(q => q.Id == _user.Id);
                if (password != confirmPassword)
                {
                    MessageBox.Show("Passwords don't match. Please try again");
                }
                user.Password = Utils.HashPassword(password);
                _db.SaveChanges();
                MessageBox.Show("Password was reset successfully");
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("An error has occured. Please try again");
            }
            
        }
    }
}
