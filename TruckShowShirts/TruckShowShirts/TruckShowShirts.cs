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
using System.Dynamic;

namespace TruckShowShirts
{
    public partial class TruckShowShirts : Form
    {
        private SqlConnection con = new SqlConnection("Data Source=NRENTSHLER;Initial Catalog=Test;Integrated Security=True");
        private SqlCommand cmd;

        // Initialize, get the username and check to see if it has Admin access.
        public TruckShowShirts()
        {
            InitializeComponent();
            //con.Open();
            //cmd = new SqlCommand("SELECT TOP 1 * FROM Security.dbo.LoginEventLog ORDER BY ExecutionTime desc", con);
            //SqlDataReader dr2 = cmd.ExecuteReader();
            //if (dr2.Read())
            //{
            //    string userName = (dr2["username"].ToString());
            //    if (userName != "nickrench3")
            //    {
            //        tabControl1.TabPages.Remove(tabPage3);
            //    }
            //}
            //con.Close();
        }

        // First enter button to display how many shirts are available
        private void EnterButton1_Click(object sender, EventArgs e)
        {
            
            string style = StyleComboBox1.Text;
            style = style.Trim();

            con.Open();
            cmd = new SqlCommand("SELECT * FROM Test1 WHERE Style= '" + style + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string Small = (dr["Small"].ToString());
                Small = Small.Trim();
                StextBox.Text = Small;

                string Medium = (dr["Medium"].ToString());
                Medium = Medium.Trim();
                MtextBox.Text = Medium;

                string Large = (dr["Large"].ToString());
                Large = Large.Trim();
                LtextBox.Text = Large;

                string XL = (dr["XL"].ToString());
                XL = XL.Trim();
                XLtextBox.Text = XL;

                string twoXL = (dr["twoXL"].ToString());
                twoXL = twoXL.Trim();
                twoXLtextBox.Text = twoXL;

                string threeXL = (dr["threeXL"].ToString());
                threeXL = threeXL.Trim();
                threeXLtextBox.Text = threeXL;

                string fourXL = (dr["fourXL"].ToString());
                fourXL = fourXL.Trim();
                fourXLtextBox.Text = fourXL;

                string fiveXL = (dr["fiveXL"].ToString());
                fiveXL = fiveXL.Trim();
                fiveXLtextBox.Text = fiveXL;

            }
            con.Close();
            BinLocation(style);
        }

        private void BinLocation(string Style)
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM Test1 t inner join TestBin tt on t.ID = tt.ShirtID WHERE Style= '" + Style + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string Bin = (dr["Bin"].ToString());
                string Size = (dr["Size"].ToString());
                if (Size == "S")
                {
                    Bin1.Text = Bin;
                }
                else if (Size == "M")
                {
                    Bin2.Text = Bin;
                }
                else if (Size == "L")
                {
                    Bin3.Text = Bin;
                }
                else if (Size == "XL")
                {
                    Bin4.Text = Bin;
                }
                else if (Size == "2XL")
                {
                    Bin5.Text = Bin;
                }
                else if (Size == "3XL")
                {
                    Bin6.Text = Bin;
                }
                else if (Size == "4XL")
                {
                    Bin7.Text = Bin;
                }
                else if (Size == "5XL")
                {
                    Bin8.Text = Bin;
                }
            }
            con.Close();
        }

        // Enter button to update the shirt totals
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
            StyleComboBox2.Text = "";
            SizeComboBox.Text = "";
            quantityTextBox.Text = "";
        }

        // Admin enter button to update the total stock of the shirt size
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
           StyleComboBox2.Text = "";
           SizeComboBox.Text = "";
           quantityTextBox.Text = "";
        }

        // Clears text on first tab
        private void ClearButton1_Click(object sender, EventArgs e)
        {
            ClearTextboxes();
        }

        // Clears text on the second tab
        private void ClearButton2_Click(object sender, EventArgs e)
        {
            ClearTextboxes();
        }

        // Clears text on the third tab
        private void ClearButton3_Click(object sender, EventArgs e)
        {
            ClearTextboxes();
        }

        private void ClearTextboxes()
        {
            StyleComboBox2.Text = "";
            SizeComboBox.Text = "";
            quantityTextBox.Text = "";
            StyleComboBox3.Text = "";
            SizeComboBox2.Text = "";
            quantityTextBox2.Text = "";
            StyleComboBox1.Text = "";
            StextBox.Text = "";
            MtextBox.Text = "";
            LtextBox.Text = "";
            XLtextBox.Text = "";
            twoXLtextBox.Text = "";
            threeXLtextBox.Text = "";
            fourXLtextBox.Text = "";
            fiveXLtextBox.Text = "";
            Bin1.Text = "";
            Bin2.Text = "";
            Bin3.Text = "";
            Bin4.Text = "";
            Bin5.Text = "";
            Bin6.Text = "";
            Bin7.Text = "";
            Bin8.Text = "";
        }

        private void UpdateBin_Click(object sender, EventArgs e)
        {
            string style = StyleComboBox4.Text;
            string Bin = NewBinText.Text;
            string Size = SizeCombo.Text;

            string ID = "";

            switch (style)
            {
                case "Central Illinois Truck Mafia":
                    ID = "1";
                    break;
                case "2nd Truck Show":
                    ID = "2";
                    break;
                case "3rd Truck Show":
                    ID = "3";
                    break;
                case "4th Truck Show":
                    ID = "4";
                    break;
            }

            con.Open();
            cmd = new SqlCommand("UPDATE TestBin SET Bin = '"+Bin+"' WHERE Size = '"+ Size + "' and ShirtID= '" + ID + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Bin updated for " + style + " shirt, Size " + Size + "");
        }
    }
}
