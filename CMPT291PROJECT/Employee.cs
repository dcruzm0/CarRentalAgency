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
    public partial class Employee : Form
    {
        public Login parent;
        public SqlConnection myconnection;
        public SqlCommand mycommand;
        public SqlDataReader myreader;
        public IAsyncResult asyncResult;
        public string default_date;
        public int max_id;
        IList<string> arguments = new List<string>();


        public Employee(Login f1)
        {
            InitializeComponent();
            default_date = date_from.Value.ToString();
            // Inherit SQL Connection
            parent = f1;
            myconnection = f1.myconnection;
            myreader = f1.myreader;
            mycommand = f1.mycommand;

            //Make filters for reports not visible
            
            report_branch.Visible = false;
            report_type.Visible = false;
            report_datefrom.Visible = false;
            report_dateto.Visible = false;
            report_submit.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;

            report_filters.Click += new EventHandler(ReportFilters_Click);

            // Populate report branch combobox
            mycommand.CommandText = "SELECT city FROM branch";

            myreader.Close();
            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
            {
                report_branch.Items.Add(myreader["city"].ToString());
            }

            // Populate report type combobox
            
            myreader.Close();
            mycommand.CommandText = "select distinct description from [type]";
            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
            {
                report_type.Items.Add(myreader["description"].ToString());
            }
            myreader.Close();
            // Populate combo boxes
            mycommand.CommandText = "SELECT b.city, t.description FROM type t, branch b ";
            try
            {
                IList<string> branches = new List<string>();
                IList<string> types = new List<string>();
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    branches.Add(myreader["city"].ToString());
                    types.Add(myreader["description"].ToString());
                }
                branches = branches.Distinct().ToList();
                types = types.Distinct().ToList();

                // Combo boxes without "Any"
                addcar_branch.DataSource = branches;
                addcar_type.DataSource = types;
                remcar_branch.DataSource = branches;
                remcar_type.DataSource = types;
                edit_type.DataSource = types;
                edit_branch.DataSource = branches;
                report_type.DataSource = types;
                report_branch.DataSource = branches;

                // Combo boxes with "Any"
                branches.Insert(0, "Any");
                types.Insert(0, "Any");
                
                pickup.DataSource = branches;
                dropoff.DataSource = branches;
                vehicle_type.DataSource = types;

                 }

            
            
                
                
                


            catch (Exception e3) { MessageBox.Show(e3.ToString()); }
            myreader.Close();
            
            
        
           
        }

        //Displays the filters
        void ReportFilters_Click(object sender, EventArgs e)
        {
            report_branch.Visible = false;
            report_type.Visible = false;
            report_datefrom.Visible = false;
            report_dateto.Visible = false;
            report_submit.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            if (radioButton5.Checked == true)
            {
                report_submit.Visible = true;
                MessageBox.Show("No filters available");
            }
            else if (radioButton1.Checked == true)
            {
                report_branch.Visible = true;
                report_type.Visible = true;
                report_datefrom.Visible = true;
                report_dateto.Visible = true;
                report_submit.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
            }
            else if(radioButton2.Checked == true)
            {
                report_branch.Visible = true;
                label9.Visible = true;
                report_submit.Visible=true;
            }
            else if( radioButton3.Checked == true)
            {
                report_branch.Visible = true;
                label9.Visible = true;
                report_submit.Visible = true;
            }
            else if (radioButton4.Checked == true)
            {
                report_branch.Visible= true;
                report_type.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                report_submit.Visible = true;


            }
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Employee_Load(object sender, EventArgs e)
        {

        }

        private void Employee_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Verify customer ID input and display information on customer
            if (user_id.Text.Length == 0)
            {
                user_info.Text = "Please Enter Customer ID";
                user_info.ForeColor = Color.Red;
                return;
            }
            mycommand.CommandText = "SELECT * FROM customer WHERE cust_id = '" + user_id.Text.ToString() + "'";
            int i = 0;
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    i++;
                    user_info.ForeColor = Color.Black;
                    user_info.Text = myreader["fname"].ToString() + " " + myreader["Lname"].ToString() + "\n";
                    user_info.Text += myreader["street"].ToString() + " " + myreader["city"].ToString() + " " + myreader["province"];
                    if (myreader["Gold_status"].ToString() == "1") { user_info.Text += "\nGold Status"; }
                }
            }
            catch (Exception e3)
            {
                MessageBox.Show(e3.Message.ToString());
                user_info.ForeColor = Color.Red;
                user_info.Text = "Customer Not Found";
                return;
            }
            myreader.Close();
            // Get information on all cars fitting choices
            mycommand.CommandText = "SELECT * FROM car c, type t, branch b WHERE c.car_type = t.type_id and c.car_branch = b.branch_id ";
            if (pickup.SelectedItem.ToString() != "Any")
            {   
                mycommand.CommandText += "and b.city = '" + pickup.SelectedItem.ToString() + "' ";
            }
            if (vehicle_type.SelectedItem.ToString() != "Any")
            {   
                mycommand.CommandText += "and t.description = '" + vehicle_type.SelectedItem.ToString() + "' ";
            }
            if (date_from.Value.ToString() != default_date || date_to.Value.ToString() != default_date)
            {
                 //Get all bookings interfering with dates selected, get car_id not in that list
                // (existing start <= new end) and (new start <= existing end) Then they overlap //
                mycommand.CommandText += "and car_id not in (select car_id from Booking b where ";
                mycommand.CommandText += "(b.Date_From <= '" + date_to.Value.ToString() + "') and ('";
                mycommand.CommandText += date_from.Value.ToString() + "' <= b.Date_To)) ";              //TODO Check if date formats are compatible
            }
            //Clipboard.SetText(mycommand.CommandText);

            // Display all available vehicles to output
            IList<string> result = new List<string>();
            string temp;
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    temp = myreader["city"].ToString() + "\t" + myreader["description"].ToString() + "\t";
                    temp += myreader["model"].ToString() + "\t" + myreader["year"].ToString() + "\t";
                    temp += myreader["plate_num"].ToString() + "\t" + myreader["daily"].ToString(); //TODO CALCUTATE ACTUAL PRICE BASE ON DATE RANGE
                   
                    result.Add(temp);
                }
            }
            catch (Exception e3) { MessageBox.Show(e3.ToString()); }

            myreader.Close();
            booking_output.DataSource = result;
            //output.Text = String.Join("", result.Distinct().ToList());
            booking_output.Visible = true;

        }
        private void booking_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            // Verify customer ID input and display information on customer
            if (user_id.Text.Length == 0)
            {
                user_info.Text = "Please Enter Customer ID";
                user_info.ForeColor = Color.Red;
                return;
            }
            mycommand.CommandText = "SELECT * FROM customer WHERE cust_id = '" + user_id.Text.ToString() + "'";
            int i = 0;
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    i++;
                    user_info.ForeColor = Color.Black;
                    user_info.Text = myreader["fname"].ToString() + " " + myreader["Lname"].ToString() + "\n";
                    user_info.Text += myreader["street"].ToString() + " " + myreader["city"].ToString() + " " + myreader["province"];
                    if (myreader["Gold_status"].ToString() == "1") { user_info.Text += "\nGold Status"; }
                }
            }
            catch (Exception e3)
            {
                MessageBox.Show(e3.Message.ToString());
                user_info.ForeColor = Color.Red;
                user_info.Text = "Customer Not Found";
                return;
            }
            myreader.Close();
            if (i == 0) { user_info.ForeColor = Color.Red; user_info.Text = "Customer Not Found"; return; }

            if (booking_output.SelectedValue != null)
            {
                string[] args = booking_output.SelectedItem.ToString().Split('\t');
                MessageBox.Show("Confirm Booking Of");
                string selected_car_id = get_car_id_from_plate(args[args.Length - 2]);
                string branch_id = "", type_id = "";

                // Get type_id and branch_id from selected text
                mycommand.CommandText = "SELECT DISTINCT type_id, branch_id FROM type, branch WHERE type.description = '" + args[1];
                mycommand.CommandText += "' and branch.city = '" + args[0] + "'";
                Clipboard.SetText(mycommand.CommandText);
                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {
                        branch_id = myreader["branch_id"].ToString();
                        type_id = myreader["type_id"].ToString();
                    }
                    myreader.Close();
                }
                catch
                {
                    MessageBox.Show("Cannot Find Car In DB");
                }
                string message = "Create booking: " + args[1] + " From " + date_from.Text + " To " + date_to.Text + " For " + args[5];
                DialogResult confirm = MessageBox.Show(message, "Confirm Booking", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes) {
                    // Create insertion command and commit to database
                    string new_id = get_new_id("booking");
                    mycommand.CommandText = "INSERT INTO booking VALUES('" + new_id + "', '";
                    mycommand.CommandText += user_id.Text + "', '" + selected_car_id + "', '" + date_from.Text + "', '";
                    mycommand.CommandText += date_to.Text + "', NULL, " + args[args.Length - 1] + ", '";
                    mycommand.CommandText += type_id + "', '" + branch_id + "', ";
                    if (drop_off_check.Checked == true)
                    {
                        mycommand.CommandText += "'" + dropoff.SelectedText + "')";
                    }
                    else
                    {
                        mycommand.CommandText += "NULL)";
                    }
                    Clipboard.SetText(mycommand.CommandText);
                    
                    try
                    {
                        mycommand.ExecuteNonQuery();
                        MessageBox.Show("Booking Created. ID: " + new_id);
                    }
                    catch
                    {
                        MessageBox.Show("Cannot Insert Booking Into Table");
                    }
                }
            }

        }
        private string get_new_id(string table)
        {
            /*
             * Returns the incremental next id for given table as string formatted to 3 digits
             * Takes string table, exact name of table in database
             */
            string new_id = "";
            int max_id = 0;
            mycommand.CommandText = "SELECT count(*) as max_id FROM " + table;
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read()) { max_id = Int32.Parse(myreader["max_id"].ToString()); }
                myreader.Close();
                new_id = (max_id++).ToString();
                if (new_id.Length == 1)
                {
                    new_id = "00" + max_id.ToString();
                }
                else if (new_id.Length == 2)
                {
                    new_id = "0" + max_id.ToString();
                }
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
            return new_id;
        }
        private void results_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void addcar_submit_Click(object sender, EventArgs e)
        {
            string confirm;
            confirm = "Add " + addcar_type.SelectedItem.ToString() + " to " + addcar_branch.SelectedItem.ToString();
            confirm += " with " + addcar_colour.Text + " colour and " + addcar_milage.Text + "km";
            MessageBox.Show(confirm, "Confirm", MessageBoxButtons.YesNo);
            mycommand.CommandText = "SELECT COUNT(*) FROM car";
            myreader = mycommand.ExecuteReader();
            string numOfCars = "";
            while (myreader.Read())
            {
                numOfCars = myreader[0].ToString();
            }
            myreader.Close();
            
            
            int carID;
            carID = Convert.ToInt32(numOfCars) + 1;
            numOfCars = carID.ToString();
            int a = numOfCars.Length;
            if (a == 1)
                numOfCars = "00" + carID.ToString();
            if (a == 2)
                numOfCars = "0" + carID.ToString();
            string carToAdd;
            carToAdd = "Insert into car (car_id, car_type, car_branch, model, year, plate_num) values (" + "'" + numOfCars + "'" + ", " + "'" + addcar_type.SelectedItem.ToString() + "'" + ", " + "'" + addcar_branch.SelectedItem.ToString() + "'" + ", " + "'" + addcar_model.Text.ToString() + "'" + ", " + "'" + addcar_milage.Text.ToString() + "'" + ", " + "'" + addcar_plate.Text.Replace("-", "") + "'" + ")";
            mycommand.CommandText = carToAdd;
            int result;
            asyncResult = mycommand.BeginExecuteNonQuery();
            result = mycommand.EndExecuteNonQuery(asyncResult);
            MessageBox.Show("Added " + result.ToString() + " car to inventory.");
            addcar_branch.ResetText();
            addcar_type.ResetText();
            addcar_colour.Clear();
            addcar_milage.Clear();
            addcar_plate.Clear();
            addcar_model.Clear();

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            remove_output.Items.Add("TEMP");
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (remove_output.SelectedItem != null)
            {
                string car;
                string message = "Are you sure you would like to remove: ";
                message += remove_output.SelectedItem.ToString();
                MessageBox.Show(message, "Remove", MessageBoxButtons.YesNo);
                car = remove_output.SelectedItem.ToString();
                string[] args = car.Split('\t');
                string[] cols = { "car_id", "car_type", "car_branch", "model", "year", "plate_num" };
                mycommand.CommandText = "DELETE FROM car WHERE ";
                int i;
                for (i = 0; i < args.Length; i++)
                {
                    
                    mycommand.CommandText += cols[i] + " = ";
                    mycommand.CommandText += "'" + args[i] + "'";
                    if (i + 1 == args.Length)
                        break;
                    mycommand.CommandText += " AND ";
                   
                }
                mycommand.ExecuteNonQuery();
                MessageBox.Show(mycommand.CommandText.ToString());

                
            }
        }

        private void edit_output_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (edit_output.SelectedItem != null)
            {
                Edit edit = new Edit(parent, edit_output.SelectedItem.ToString());
                edit.Show();
                edit_output.Items.Clear();
                //MessageBox.Show("Editing: " + edit_output.SelectedItem.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            edit_output.Items.Add("TEMP");
        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        

        private void report_submit_Click(object sender, EventArgs e)
        {
            IList<string> report = new List<string>();
            reportbox.Text = "";
            if (radioButton5.Checked == true)
            {
                mycommand.CommandText =
                    "(select customer.cust_id as output " +
                    "from booking, customer " +
                    "where booking.cust_id = customer.cust_ID and Gold_status = 1) " +
                    "except " +
                    "(select cust_id " +
                    "from booking, car " +
                    "where booking.car_id = car.car_id and type_requested != car_type);";
                myreader = mycommand.ExecuteReader();
            }
            else if (radioButton1.Checked == true)
            {
                mycommand.CommandText = "select distinct branchFrom, sum(price) as [output] from booking where branchFrom = '" 
                    + report_branch.SelectedItem.ToString()
                    + "' and type_requested = '" + report_type.SelectedItem.ToString() + "'" +
                    "and date_from >= '" + report_datefrom.Text + "' and date_to <= '"
                    + report_dateto.Text + "' group by branchFrom;";
                myreader = mycommand.ExecuteReader();
            }
            else if(radioButton2.Checked == true)
            {
                mycommand.CommandText = "select count(*) as [output] " +
                    "from booking " +
                    "where date_to != returned and branchFrom = '" + report_branch.SelectedItem.ToString() + "';";
                myreader = mycommand.ExecuteReader();
            }
            else if(radioButton3.Checked == true)
            {
                mycommand.CommandText = "select car_id as [output], max(num1) " +
                    "from( " +
                    "select car_id, count(*) as num1 " +
                    "from booking " +
                    "where branchFrom = '" +
                    report_branch.SelectedItem.ToString() +
                    "' group by car_id) as tem " +
                    "group by car_id ";
                myreader = mycommand.ExecuteReader();
            }
            else if(radioButton4.Checked == true)
            {
                mycommand.CommandText = "select sum([days]) as [output] " +
                    "from( " +
                    "select booking_id, DATEDIFF(day, date_from, date_to) as [days] " +
                    "from booking where branchFrom = '" +
                    report_branch.SelectedItem.ToString() + 
                    "' and type_requested = '"+
                    report_type.SelectedItem.ToString() +
                    "') as Temp;";
                myreader = mycommand.ExecuteReader();

            }
            MessageBox.Show(mycommand.CommandText);
            while (myreader.Read())
            {
                reportbox.Text += myreader["output"].ToString() + "\n";
            }

            myreader.Close();
            if (String.IsNullOrEmpty(reportbox.Text))
            {
                MessageBox.Show("No data available");
            }
            reportbox.Visible = true;
        }

        private void drop_off_check_CheckedChanged(object sender, EventArgs e)
        {

        }

       

        private void remcar_id_TextChanged(object sender, EventArgs e)
        {
            remove_output.Items.Clear();
            searchForCarsWithID(this, remcar_id.Text.ToString());
        }

        private void remtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            remove_output.Items.Clear();
            /*arguments.Insert(0, remcar_branch.SelectedItem.ToString());
            arguments.Insert(1, remcar_type.SelectedItem.ToString());
            searchForCarsWithoutID(this, arguments);*/
        }

        private void rembranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            remove_output.Items.Clear();
            /*arguments.Insert(0, remcar_branch.SelectedItem.ToString());
            arguments.Insert(1, remcar_type.SelectedItem.ToString());
            searchForCarsWithoutID(this, arguments);*/
        }

        private void remSubmit_Click(object sender, EventArgs e)
        {
            remove_output.Items.Clear();
            if (remcar_id.Text.Length != 0)
            {
                mycommand.CommandText = "SELECT * FROM car WHERE car_id = " + "'" + remcar_id.Text + "'";
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    remove_output.Items.Add(myreader["car_id"].ToString() + "\t" + myreader["car_type"].ToString() + "\t" + myreader["car_branch"].ToString() + "\t" + myreader["model"].ToString() + "\t" + myreader["year"].ToString() + "\t" + myreader["plate_num"].ToString());
                }
                myreader.Close();

            }
            else
            {
                mycommand.CommandText = "SELECT * FROM car WHERE car_type = " + "'" + remcar_type.SelectedItem.ToString() + "'" + " AND car_branch = " + "'" + remcar_branch.SelectedItem.ToString() + "'";
                if (remcar_plate.Text.ToString().Length != 0)
                    mycommand.CommandText += " AND plate_num = " + "'" + remcar_plate + "'";
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    remove_output.Items.Add(myreader["car_id"].ToString() + "\t" + myreader["car_type"].ToString() + "\t" + myreader["car_branch"].ToString() + "\t" + myreader["model"].ToString() + "\t" + myreader["year"].ToString() + "\t" + myreader["plate_num"].ToString());
                }
                myreader.Close();

            }
        }

        private void searchForCarsWithID(object sender, string anID)
        {
            mycommand.CommandText = "SELECT * FROM car WHERE car_id like " + "'" + anID + "%'";
            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
            {
                remove_output.Items.Add(myreader["car_id"].ToString() + "\t" + myreader["car_type"].ToString() + "\t" + myreader["car_branch"].ToString() + "\t" + myreader["model"].ToString() + "\t" + myreader["year"].ToString() + "\t" + myreader["plate_num"].ToString());
            }
            myreader.Close();
        }


        private void searchForCarsWithID1(object sender, string anID)
        {
            mycommand.CommandText = "SELECT * FROM car WHERE car_id like " + "'" + anID + "%'";
            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
            {
                edit_output.Items.Add(myreader["car_id"].ToString() + "\t" + myreader["car_type"].ToString() + "\t" + myreader["car_branch"].ToString() + "\t" + myreader["model"].ToString() + "\t" + myreader["year"].ToString() + "\t" + myreader["plate_num"].ToString());
            }
            myreader.Close();
        }
        private void searchForCarsWithoutID(object sender, IList<string> args)
        {
            mycommand.CommandText = "SELECT * FROM car WHERE car_branch = " + "'" + args[0] + "'" + " AND car_type = " + "'" + args[1] + "'";

            /*if (remcar_plate.Text.ToString().Length != 0)
                mycommand.CommandText += " AND plate_num like " + "'" + remcar_plate.Text.ToString() + "%'";

            if (remcar_model.Text.ToString().Length != 0)
                mycommand.CommandText += " AND model like " + "'" + remcar_model.Text.ToString() + "%'";

            if (remcar_year.Text.ToString().Length != 0)
                mycommand.CommandText += " AND year like " + "'" + remcar_year.Text.ToString() + "%'";*/

            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
            {
                remove_output.Items.Add(myreader["car_id"].ToString() + "\t" + myreader["car_type"].ToString() + "\t" + myreader["car_branch"].ToString() + "\t" + myreader["model"].ToString() + "\t" + myreader["year"].ToString() + "\t" + myreader["plate_num"].ToString());
            }
            myreader.Close();
        }
        public string get_car_id_from_plate(string plate_no)
        {
            string car_id = "";
            mycommand.CommandText = "SELECT car_id FROM car WHERE car.plate_num = '" + plate_no + "'";
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    car_id = myreader["car_id"].ToString();
                }
                myreader.Close();
            }catch { MessageBox.Show("Cannot Find Car With Plate: " + plate_no); }
            if (car_id.Length == 0) { MessageBox.Show("No Car_id with plate: " + plate_no); }
            return car_id;
        }
        private void editCar_id(object sender, EventArgs e)
        {
            edit_output.Items.Clear();
            searchForCarsWithID1(this, editcar_id.Text.ToString());
            
        }

        private void editCar_Submit(object sender, EventArgs e)
        {
            edit_output.Items.Clear();
            if (editcar_id.Text.Length != 0)
            {
                mycommand.CommandText = "SELECT * FROM car WHERE car_id = " + "'" + editcar_id.Text + "'";
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    edit_output.Items.Add(myreader["car_id"].ToString() + "\t" + myreader["car_type"].ToString() + "\t" + myreader["car_branch"].ToString() + "\t" + myreader["model"].ToString() + "\t" + myreader["year"].ToString() + "\t" + myreader["plate_num"].ToString());
                }
                myreader.Close();

            }
            else
            {
                mycommand.CommandText = "SELECT * FROM car WHERE car_id like " + "'" + editcar_id.Text.ToString() + "'" + " OR car_type = " + "'" + edit_type.SelectedItem.ToString() + "'" + " AND car_branch = " + "'" + edit_branch.SelectedItem.ToString() + "'";
                if (edit_plate.Text.ToString().Length != 0)
                    mycommand.CommandText += " OR plate_num like " + "'" + edit_plate + "%'";
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    edit_output.Items.Add(myreader["car_id"].ToString() + "\t" + myreader["car_type"].ToString() + "\t" + myreader["car_branch"].ToString() + "\t" + myreader["model"].ToString() + "\t" + myreader["year"].ToString() + "\t" + myreader["plate_num"].ToString());
                }
                myreader.Close();

            }
        }

        private void user_id_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void user_id_KeyPress(object sender, KeyPressEventArgs e)
        {
           

        }
    }
}
