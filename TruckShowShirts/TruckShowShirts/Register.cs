using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TruckShowShirts
{
    public partial class Register : Form
    {
        private SqlConnection conSecure = new SqlConnection(@"Data Source=NICKRENTSCHLER\SQLEXPRESS01;Initial Catalog=Security;Integrated Security=True;Pooling=False");

        public Register()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            conSecure.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT LoginName FROM Login WHERE LoginName ='" + userNameTextBox.Text.Trim() + "'", conSecure);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                MessageBox.Show("Username is already taken, please try again");
                conSecure.Close();
            }
            else
            {
                conSecure.Close();
                conSecure.Open();
                string message = "Success, please wait while admin approves account";
                string title = "Registration";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
                SqlCommand cmd = new SqlCommand("dbo.uspAddUser", conSecure);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pLogin", userNameTextBox.Text);
                cmd.Parameters.AddWithValue("@pPassword", passwordTextBox.Text);
                cmd.ExecuteNonQuery();
                conSecure.Close();
                Login form1 = new Login();
                form1.Show();
                this.Owner = form1;
                this.Hide();
            }
        }
    }
}
