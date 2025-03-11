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
    public partial class Edit : Form
    {
        public Login parent;
        public SqlConnection myconnection;
        public SqlCommand mycommand;
        public SqlDataReader myreader;
        public string[] args;

        public Edit(Login f1, string temp)
        {
            InitializeComponent();
            parent = f1;
            myconnection = f1.myconnection;
            myreader = f1.myreader;
            mycommand = f1.mycommand;

            // Populate combo boxes
            mycommand.CommandText = "SELECT car_branch, car_type FROM car";
            try
            {
                IList<string> branches = new List<string>();
                IList<string> types = new List<string>();
               

                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    branches.Add(myreader["car_branch"].ToString());
                    types.Add(myreader["car_type"].ToString());
                }
                branches = branches.Distinct().ToList();
                types = types.Distinct().ToList();

                
                edit_type.DataSource = types;
                edit_branch.DataSource = branches;

                branches.Insert(0, "Any");
                types.Insert(0, "Any");
                
            }
            
            catch (Exception e3) { MessageBox.Show(e3.ToString()); }
            myreader.Close();
            original_values.Text =  temp;
            args = temp.Split('\t');
            edit_type.SelectedItem = args[0];
            edit_branch.SelectedItem = args[1];
            edit_model.Text = args[3];
            edit_plate.Text = args[5];
           
        }

        private void Edit_Load(object sender, EventArgs e)
        {

        }

        private void editSubmit(object sender, EventArgs e)
        {
            
            MessageBox.Show("Are these changes correct?\n" + edit_type.SelectedItem.ToString() + " " + edit_branch.SelectedItem.ToString() + " " + edit_model.Text.ToString() + " " + edit_plate.Text.ToString(), "Confirm?", MessageBoxButtons.YesNo);
            mycommand.CommandText = "UPDATE car SET car_type = " + "'" + edit_type.SelectedItem.ToString() + "'" + ", car_branch = " + "'" + edit_branch.SelectedItem.ToString() + "'";
            mycommand.CommandText += ", model = " + "'" + edit_model.Text.ToString() + "'" + ", plate_num = " + "'" + edit_plate.Text.ToString() + "'" + " WHERE car_id = " + "'" + args[0] + "'";

            mycommand.ExecuteNonQuery();
            MessageBox.Show("Car was successfully updated!");
            this.Close();
            
            //MessageBox.Show(mycommand.CommandText.ToString());
        }
    }
}
