using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TruckShowShirts
{
    public partial class TruckShowShirts : Form
    {
        private SqlConnection conSecure = new SqlConnection(@"Data Source=NICKRENTSCHLER\SQLEXPRESS;Initial Catalog=Security;Integrated Security=True;Pooling=False");
        private SqlConnection con = new SqlConnection(@"Data Source=NICKRENTSCHLER\SQLEXPRESS;Initial Catalog=TruckShow;Integrated Security=True;Pooling=False");
        private SqlCommand cmd;

        public TruckShowShirts()
        {
            InitializeComponent();
            conSecure.Open();
            cmd = new SqlCommand("SELECT TOP 1 * FROM LoginEventLog ORDER BY ExecutionTime desc", conSecure);
            SqlDataReader dr2 = cmd.ExecuteReader();
            if (dr2.Read())
            {
                string userName = (dr2["username"].ToString());
                if (userName != "nickrench3")
                {
                    tabControl1.TabPages.Remove(tabPage3);
                }

            }
            conSecure.Close();
        }

        private void EnterButton1_Click(object sender, EventArgs e)
        {
            StextBox.Clear();
            MtextBox.Clear();
            LtextBox.Clear();
            XLtextBox.Clear();
            twoXLtextBox.Clear();
            threeXLtextBox.Clear();
            fourXLtextBox.Clear();
            fiveXLtextBox.Clear();

            string style = StyleComboBox1.Text;
            style = style.Trim();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM Shirts WHERE Style= '" + style + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string Small = (dr["Small"].ToString());
                Small = Small.Trim();
                StextBox.AppendText(Small);

                string Medium = (dr["Medium"].ToString());
                Medium = Medium.Trim();
                MtextBox.AppendText(Medium);

                string Large = (dr["Large"].ToString());
                Large = Large.Trim();
                LtextBox.AppendText(Large);

                string XL = (dr["XL"].ToString());
                XL = XL.Trim();
                XLtextBox.AppendText(XL);

                string twoXL = (dr["twoXL"].ToString());
                twoXL = twoXL.Trim();
                twoXLtextBox.AppendText(twoXL);

                string threeXL = (dr["threeXL"].ToString());
                threeXL = threeXL.Trim();
                threeXLtextBox.AppendText(threeXL);

                string fourXL = (dr["fourXL"].ToString());
                fourXL = fourXL.Trim();
                fourXLtextBox.AppendText(fourXL);

                string fiveXL = (dr["fiveXL"].ToString());
                fiveXL = fiveXL.Trim();
                fiveXLtextBox.AppendText(fiveXL);
            }
            con.Close();

        }

        private void EnterButton2_Click(object sender, EventArgs e)
        {
            string style = StyleComboBox2.Text;
            string sizeInput = SizeComboBox.Text;
            string quantitySold = quantityTextBox.Text;

            switch (SizeComboBox.SelectedItem.ToString().Trim())
            {
                case "2XL":
                    sizeInput = "twoXL";
                    break;
                case "3XL":
                    sizeInput = "threeXL";
                    break;
                case "4XL":
                    sizeInput = "fourXL";
                    break;
                case "5XL":
                    sizeInput = "fiveXL";
                    break;
                default:
                    sizeInput = sizeInput;
                    break;
            }
            
            style = style.Trim();
            sizeInput = sizeInput.Trim();
            quantitySold = quantitySold.Trim();
            int quantityNum = System.Convert.ToInt32(quantitySold);

            con.Open();
            cmd = new SqlCommand("SELECT * FROM Shirts WHERE Style= '" + style + " "  +"'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string size = (dr[sizeInput].ToString());
                size = size.Trim();
                int shirtTotal = Int32.Parse(size);
                shirtTotal = shirtTotal - quantityNum;
                con.Close();
                con.Open();
                cmd = new SqlCommand("UPDATE Shirts SET " +sizeInput+"=" + shirtTotal.ToString() + " WHERE Style='" + style + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            con.Close();

            string message = "Shirt totals updated";
            string title = "Truck Show Shirt Update";
            MessageBox.Show(message, title);
            
        }

        private void EnterButton3_Click(object sender, EventArgs e)
        {
            string style = StyleComboBox3.Text;
            string sizeInput = SizeComboBox2.Text;
            string quantity = quantityTextBox2.Text;

            switch (SizeComboBox2.SelectedItem.ToString().Trim())
            {
                case "2XL":
                    sizeInput = "twoXL";
                    break;
                case "3XL":
                    sizeInput = "threeXL";
                    break;
                case "4XL":
                    sizeInput = "fourXL";
                    break;
                case "5XL":
                    sizeInput = "fiveXL";
                    break;
                default:
                    sizeInput = sizeInput;
                    break;
            }

            style = style.Trim();
            sizeInput = sizeInput.Trim();
            quantity = quantity.Trim();
            int quantityNum = System.Convert.ToInt32(quantity);

           con.Open();
           cmd = new SqlCommand("UPDATE Shirts SET " + sizeInput + "=" + quantityNum + " WHERE Style='" + style + "'", con);
           cmd.ExecuteNonQuery();
           con.Close();

           string message = "Shirt totals updated";
           string title = "Truck Show Shirt Update";
           MessageBox.Show(message, title);
        }

        private void ClearButton1_Click(object sender, EventArgs e)
        {
            StyleComboBox1.Text = "";
            StextBox.Text = "";
            MtextBox.Text = "";
            LtextBox.Text = "";
            XLtextBox.Text = "";
            twoXLtextBox.Text = "";
            threeXLtextBox.Text = "";
            fourXLtextBox.Text = "";
            fiveXLtextBox.Text = "";
        }

        private void ClearButton2_Click(object sender, EventArgs e)
        {
            StyleComboBox2.Text = "";
            SizeComboBox.Text = "";
            quantityTextBox.Text = "";
        }

        private void ClearButton3_Click(object sender, EventArgs e)
        {
            StyleComboBox3.Text = "";
            SizeComboBox2.Text = "";
            quantityTextBox2.Text = "";
        }
    }
}
