﻿using System;
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
    public partial class ManageUsers : Form
    {
        private readonly CarRentalEntities _db;
        public ManageUsers()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            //populateGrid()
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                // get Id of sellected row = when click to row
                // give me the selected the first selected row, and give me 
                // the cell call id (it's column) and the value it's not
                //visible 
                var id = (int)gvUserList.SelectedRows[0].Cells["Id"].Value;

                //query database for record
                var user = _db.Users.FirstOrDefault(q => q.Id == id);

                var genericPassword = "Password@123";
                var hashed_password = Utils.HashPassword(genericPassword);
                user.Password = hashed_password;
                _db.SaveChanges();

                MessageBox.Show($"{user.Username}'s Password has been reset");
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Error: {ex.Message}");
            }
        }

        private void btnDeactivateUser_Click(object sender, EventArgs e)
        {
            try
            {
                // get Id of sellected row = when click to row
                // give me the selected the first selected row, and give me 
                // the cell call id (it's column) and the value it's not
                //visible 
                var id = (int)gvUserList.SelectedRows[0].Cells["Id"].Value;

                //query database for record
                var user = _db.Users.FirstOrDefault(q => q.Id == id);
                // if(user.isActive == true)
                //  user.isActive = false
                //else
                //  user.isActive = true
                user.IsActive = user.IsActive == true ? false : true;
                _db.SaveChanges();

                MessageBox.Show($"{user.Username} active status has changed");
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Error: {ex.Message}");
            }
        }
    }
}
