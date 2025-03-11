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


namespace CMPT291PROJECT
{
    public partial class Customer : Form
    {
        public string s;
        public string default_date;
        public Login parent;
        public SqlConnection sqlConnection;
        public SqlCommand mycommand;
        public SqlDataReader SqlDataReader;
        public Customer(Login ftemp)
        {
            InitializeComponent();
            default_date = date_from.Value.ToString();
            parent = ftemp;

            
            sqlConnection = parent.myconnection;
            mycommand = parent.mycommand;
            SqlDataReader = parent.myreader;
            

            // Populate combo boxes
            mycommand.CommandText = "SELECT b.city, t.description FROM car c, type t, branch b ";
            mycommand.CommandText += "WHERE c.car_type = t.type_id and c.car_branch = b.branch_id";
            try
            {
                IList<string> branches = new List<string>();
                IList<string> types = new List<string>();
                branches.Add("Any");
                types.Add("Any");

                SqlDataReader = mycommand.ExecuteReader();
                while (SqlDataReader.Read())
                {
                    branches.Add(SqlDataReader["city"].ToString());
                    types.Add(SqlDataReader["description"].ToString());
                }
                branches = branches.Distinct().ToList();
                types = types.Distinct().ToList();
                pickup_branch.DataSource = branches;
                vehicle_type.DataSource = types;
            }
            catch (Exception e3) { MessageBox.Show(e3.ToString()); }
            SqlDataReader.Close();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            
        }

        private void submit_Click(object sender, EventArgs e)
        {


            // Build Query based off selections
            mycommand.CommandText = "SELECT * FROM car c, type t, branch b WHERE c.car_type = t.type_id and b.branch_id = c.car_branch ";

            if (pickup_branch.SelectedItem.ToString() != "Any")
            {   // If branch is selected add to commandtext
                mycommand.CommandText += "and b.city = '" + pickup_branch.SelectedItem.ToString() + "' ";

            }
            if (vehicle_type.SelectedItem.ToString() != "Any")
            {   //If only a type is selected
                mycommand.CommandText += "and t.description = '" + vehicle_type.SelectedItem.ToString() + "' ";
            }
            if (date_from.Value.ToString() != default_date || date_to.Value.ToString() != default_date)
            {
                
                // (existing start <= new end) and (new start <= eexisting end)///
                mycommand.CommandText += "and car_id not in (select car_id from Booking b where " ;
                mycommand.CommandText += "(b.Date_From <= '" + date_to.Value.ToString() + "') and ('";
                mycommand.CommandText += date_from.Value.ToString() + "' <= b.Date_To))";
               
            }

            // Display all available vehicles to output

            IList<string> result = new List<string>();
            string temp;
            //MessageBox.Show(SqlCommand.CommandText.ToString());
            try
            {
                SqlDataReader = mycommand.ExecuteReader();
                while (SqlDataReader.Read())
                {
                    temp = SqlDataReader["city"].ToString().PadRight(15) + SqlDataReader["description"].ToString().PadRight(15);
                    temp += SqlDataReader["daily"].ToString(); // TODO CALCULATE ACTUAL PRICE
                    result.Add(temp);
                }
            }
            catch (Exception e3) { MessageBox.Show(e3.ToString()); }

            SqlDataReader.Close();
            output.DataSource = result;
            //output.Text = String.Join("", result.Distinct().ToList());
            output.Visible = true;

        }

        private void results_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void log_out_Click(object sender, EventArgs e)
        {
            parent.Visible = true;
            this.Close();
        }

        private void Customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Visible = true;
        }
    }
}
