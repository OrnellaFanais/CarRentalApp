using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public static class Utils
    {
        public static bool FormIsOpen(string name)
        {
            var OpenForms = Application.OpenForms.Cast<Form>();
            var isOpen = OpenForms.Any(q => q.Name == name);
            return isOpen;
        }

        public static string HashPassword(string password)
        {
            // Add encryption
            SHA256 sha = SHA256.Create();

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
            //var hashed_password = sBuiler.ToString();

            return sBuiler.ToString();
        }

    }
}
