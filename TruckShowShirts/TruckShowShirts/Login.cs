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
    public partial class Login : Form
    {
        private SqlConnection con = new SqlConnection(@"Data Source=NICKRENTSCHLER\SQLEXPRESS;Initial Catalog=Savings;Integrated Security=True;Pooling=False");
        private SqlConnection con2 = new SqlConnection(@"Data Source=NICKRENTSCHLER\SQLEXPRESS;Initial Catalog=TruckShow;Integrated Security=True;Pooling=False");
        private SqlCommand cmd;

        public Login()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT UserID FROM [dbo].[Login] WHERE LoginName='" + userNameTextBox.Text + "' AND PasswordHash=HASHBYTES('SHA2_512', N'" + passwordTextBox.Text + "') AND Added='Y'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                con.Close();
                con2.Open();
                cmd = new SqlCommand("INSERT LOGINEVENTLOG VALUES('" + userNameTextBox.Text.Trim() + "', '" + DateTime.Now + "')", con2);
                cmd.ExecuteNonQuery();
                con2.Close();
                TruckShowShirts ts = new TruckShowShirts();
                ts.Show();
                this.Owner = ts;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Your account has not been activated yet or check your username and password", "Error");
            }
            con.Close();
        }

        private void TruckShowShirts_FormClosed(object send, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
