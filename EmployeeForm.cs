using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RestaurantEmployeeManagement
{
    public partial class EmployeeForm: Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EJTS0UR\SQLEXPRESS;Initial Catalog=Employees_RestaurantDB;Integrated Security=True");
        public EmployeeForm()
        {
            InitializeComponent();
            
        }
        private void LoadEmployees()
        {
            string query = "SELECT * FROM Employees";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void ClearAllFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtPosition.Clear();
            txtSalary.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            dateTimePicker1.Value = DateTime.Now;
            comboBoxStatus.SelectedIndex = 0;
        }
        private bool IsInputValid()
        {
            if (txtName.Text == "" || txtPosition.Text == "" || txtSalary.Text == "" || txtPhone.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Please, Fill All Fields!");
                return false;
            }
            return true;
        }

        private void btnAdd(object sender, EventArgs e)
        {
            if (!IsInputValid())
                return;
            string query = "INSERT INTO Employees (FullName, Position, Salary, Phone, Email, HireDate, Status) VALUES (@name, @position, @salary, @phone, @email, @hireDate, @status)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@position", txtPosition.Text);
            cmd.Parameters.AddWithValue("@salary", txtSalary.Text);
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@hireDate", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@status", comboBoxStatus.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            LoadEmployees();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            comboBoxStatus.Items.Clear();
            comboBoxStatus.Items.Add("Active");
            comboBoxStatus.Items.Add("Inactive");
            comboBoxStatus.SelectedIndex = 0;
            txtID.Select();
            LoadEmployees();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!IsInputValid())
                return;
            string query = "UPDATE Employees SET FullName = @name, Position = @position, Salary = @salary, Phone = @phone, Email = @email, HireDate = @hireDate, Status = @status WHERE EmployeeID = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtID.Text);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@position", txtPosition.Text);
            cmd.Parameters.AddWithValue("@salary", txtSalary.Text);
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@hireDate", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@status", comboBoxStatus.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Employee updated successfully!");
            LoadEmployees();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Want to Delete This Employee??", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes);
            {
                string query = "DELETE FROM Employees WHERE EmployeeID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", txtID.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            LoadEmployees();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void deleteAllEmployees_Click(object sender, EventArgs e)
        {
            string query = "TRUNCATE TABLE Employees";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("All Employees Deleted Successfully!");
        }

        private void reload_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }
    }
}
