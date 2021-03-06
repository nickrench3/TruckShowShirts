﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TruckShowShirts
{
    public partial class Login : Form
    {
        private SqlConnection conSecure = new SqlConnection(@"Data Source=NICKRENTSCHLER\SQLEXPRESS01;Initial Catalog=Security;Integrated Security=True;Pooling=False");
        private SqlConnection con = new SqlConnection(@"Data Source=NICKRENTSCHLER\SQLEXPRESS01;Initial Catalog=TruckShow;Integrated Security=True;Pooling=False");
        private SqlCommand cmd;
        private string LastIP = "";
        private string HostName = "";

        public Login()
        {
            InitializeComponent();
            LastIP = GetLocalIPAddress();
            HostName = Dns.GetHostName();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            conSecure.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT UserID FROM [dbo].[Login] WHERE LoginName='" + userNameTextBox.Text + "' AND PasswordHash=HASHBYTES('SHA2_512', N'" + passwordTextBox.Text + "') AND Added='Y'", conSecure);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                cmd = new SqlCommand("INSERT LOGINEVENTLOG VALUES('" + userNameTextBox.Text.Trim() + "', '" + DateTime.Now + "', 'Truck Show Shirts', '" + LastIP + "', '" + HostName + "')", conSecure);
                cmd.ExecuteNonQuery();
                conSecure.Close();
                TruckShowShirts ts = new TruckShowShirts();
                ts.Show();
                this.Owner = ts;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Your account has not been activated yet or check your username and password", "Error");
            }
        }

        private void TruckShowShirts_FormClosed(object send, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Owner = register;
            this.Hide();
        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
