using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Windows.Forms;

namespace RestaurantEmployeeManagement
{
    public partial class LogIn_Form: Form
    {
        public LogIn_Form()
        {
            InitializeComponent();
        }

        private void checkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(checkPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            checkPassword.Checked = false;
            txtUserName.Focus();

        }

        private void log_in_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;

            if(userName == "admin" && password == "1234")
            {
                this.Hide();

                EmployeeForm employeeForm = new EmployeeForm();
                employeeForm.ShowDialog();

                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }

        }
    }
}
