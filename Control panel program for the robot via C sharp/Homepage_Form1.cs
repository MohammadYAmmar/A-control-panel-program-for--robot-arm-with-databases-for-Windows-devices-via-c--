using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Control_panel_program_for_the_robot_via_C_sharp
{
    public partial class Homepage_Form1 : Form
    {
        //It will be used for the time and date
        DateTime aDate = DateTime.Now;

        public Homepage_Form1()
        {
            InitializeComponent();
        }

        private void configuration_database_button_Click(object sender, EventArgs e)
        {
            configuration_database_Form2 secondForm = new configuration_database_Form2();
            secondForm.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetDataObject(MessageBox.Show("http://www.github.com/MohammadYAmmar"), true);
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            //CreateEntry
            string connectionString = "datasource=localhost; port=3306;username=root;password=;database=Robot-arm-with-a-camera; CharSet=utf8;";//CharSet=utf8 mb4    

            write_to_datbase(connectionString);
            MessageBox.Show("The information was recorded in the database");
        }

        public void write_to_datbase(string connectionString)
        {

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    //Open connection
                    connection.Open();

                    //Compose query using sql parameters
                    // var sqlCommand = "INSERT INTO direction_and_motor_values (date,motor_1,motor_2,motor_3,motor_4,motor_5,motor_6) VALUES (@date_value, @motor_1_value, @motor_2_value, @motor_3_value, @motor_4_value, @motor_5_value, @motor_6_value)";


                    //A problem with this line when adding; Because right and left are reserved words in the database
                    //var sqlCommand = "INSERT INTO direction_and_motor_values (date,Forwards,Left,Right,Backwards,motor_1,motor_2,motor_3,motor_4,motor_5,motor_6) VALUES (@date_value, @F_value, @L_value, @R_value, @B_value, @motor_1_value, @motor_2_value, @motor_3_value, @motor_4_value, @motor_5_value, @motor_6_value)";

                    var sqlCommand = "INSERT INTO direction_and_motor_values (date,Forwards,Left1,Right1,Backwards,motor_1,motor_2,motor_3,motor_4,motor_5,motor_6) VALUES (@date_value, @F_value, @L_value, @R_value, @B_value, @motor_1_value, @motor_2_value, @motor_3_value, @motor_4_value, @motor_5_value, @motor_6_value)";


                    //Create mysql command and pass sql query
                    using (var command = new MySqlCommand(sqlCommand, connection))
                    {
                        command.Parameters.AddWithValue("@date_value", aDate.ToString("MM/dd/yyyy hh: mm tt"));

                        //
                        command.Parameters.AddWithValue("@F_value", "");
                        command.Parameters.AddWithValue("@L_value", "");
                        command.Parameters.AddWithValue("@R_value", "");
                        command.Parameters.AddWithValue("@B_value", "");


                        // 
                        command.Parameters.AddWithValue("@motor_1_value", motor_trackBar1.Value);
                        command.Parameters.AddWithValue("@motor_2_value", motor_trackBar2.Value);
                        command.Parameters.AddWithValue("@motor_3_value", motor_trackBar3.Value);
                        command.Parameters.AddWithValue("@motor_4_value", motor_trackBar4.Value);
                        command.Parameters.AddWithValue("@motor_5_value", motor_trackBar5.Value);
                        command.Parameters.AddWithValue("@motor_6_value", motor_trackBar6.Value);
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void write_directions_to_datbase(string input_directions, string value, string connectionString)
        {

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    //Open connection
                    connection.Open();

                    //Compose query using sql parameters

                    var sqlCommand = "INSERT INTO direction_and_motor_values (date," + input_directions + ",motor_1,motor_2,motor_3,motor_4,motor_5,motor_6) VALUES (@date_value,@value, @motor_1_value, @motor_2_value, @motor_3_value, @motor_4_value, @motor_5_value, @motor_6_value)";

                    //Only to motors value

                    //var sqlCommand = "INSERT INTO direction_and_motor_values (date," + input_directions + " +) VALUES (@date_value,@input_directions)";


                    //Create mysql command and pass sql query
                    using (var command = new MySqlCommand(sqlCommand, connection))
                    {
                        command.Parameters.AddWithValue("@date_value", aDate.ToString("MM/dd/yyyy hh: mm tt"));
                        command.Parameters.AddWithValue("@value", value);

                        command.Parameters.AddWithValue("@motor_1_value", motor_trackBar1.Value);
                        command.Parameters.AddWithValue("@motor_2_value", motor_trackBar2.Value);
                        command.Parameters.AddWithValue("@motor_3_value", motor_trackBar3.Value);
                        command.Parameters.AddWithValue("@motor_4_value", motor_trackBar4.Value);
                        command.Parameters.AddWithValue("@motor_5_value", motor_trackBar5.Value);
                        command.Parameters.AddWithValue("@motor_6_value", motor_trackBar6.Value);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Reading_from_databases_button_Click(object sender, EventArgs e)
        {
            read_content_DB("localhost", "Robot-arm-with-a-camera", "Direction_and_motor_values");
        }

        public static void read_content_DB(string localhost_or_address, string data_base, string table)
        {
            string connectionString = "datasource=" + localhost_or_address + ";port=3306;username=root;password=;database=" + data_base + "; CharSet=utf8;";//CharSet=utf8 mb4    

            // Query
            string query = "SELECT * FROM " + table;

            // Prepare the connection
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            string temp_content = null;

            try
            {
                // Open the database
                databaseConnection.Open();
                MessageBox.Show("Database Status: Connected in SQL ", "DataBase", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Execute the query
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    temp_content += "Date and time::  " + reader["date"].ToString() + "\n"
                        + "Direction: " + reader["Forwards"].ToString()
                        + reader["Forwards"].ToString()
                        + reader["Backwards"].ToString()
                        + reader["Right1"].ToString()
                        + reader["Left1"].ToString()
                        + "\n"
                        + "Motor 1 :" + reader["motor_1"].ToString() + "\n"
                        + "Motor 2 :" + reader["motor_2"].ToString() + "\n"
                        + "Motor 3 :" + reader["motor_3"].ToString() + "\n"
                        + "Motor 4 :" + reader["motor_4"].ToString() + "\n"
                        + "Motor 5 :" + reader["motor_5"].ToString() + "\n"
                        + "Motor 6 :" + reader["motor_6"].ToString() + "\n";

                }//while
                if (temp_content != null)
                {
                    MessageBox.Show(temp_content, "DataBase : " + "content in DB", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Database is empty, add data and try again", "DataBase : " + "content in DB", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }//read_content_DB

        private void Forwards_button_Click(object sender, EventArgs e)
        {
            string connectionString = "datasource=localhost; port=3306;username=root;password=;database=Robot-arm-with-a-camera; CharSet=utf8;";//CharSet=utf8 mb4    
            write_directions_to_datbase("Forwards", "Forwards", connectionString);
        }

        private void Right_button_Click(object sender, EventArgs e)
        {
            string connectionString = "datasource=localhost; port=3306;username=root;password=;database=Robot-arm-with-a-camera; CharSet=utf8;";//CharSet=utf8 mb4    
            /*
             The structure in the database was changed by adding number 1; 
            because it is for the right and left direction as an error appears, 
            and after attempts were discovered because the words are reserved
             */
            write_directions_to_datbase("Right1", "Right", connectionString);
        }

        private void Left_button_Click(object sender, EventArgs e)
        {
            string connectionString = "datasource=localhost; port=3306;username=root;password=;database=Robot-arm-with-a-camera; CharSet=utf8;";//CharSet=utf8 mb4    
            /*
           The structure in the database was changed by adding number 1; 
          because it is for the right and left direction as an error appears, 
          and after attempts were discovered because the words are reserved
           */
            write_directions_to_datbase("Left1", "Left", connectionString);
        }

        private void Backwards_button_Click(object sender, EventArgs e)
        {
            string connectionString = "datasource=localhost; port=3306;username=root;password=;database=Robot-arm-with-a-camera; CharSet=utf8;";//CharSet=utf8 mb4    
            write_directions_to_datbase("Backwards", "Backwards", connectionString);
        }

        private void info_button_Click(object sender, EventArgs e)
        {
            string Release_information = "This is the first version of this program that was part of my second training at Smart Methods Company.\n" +
                "For more details, this is the company’s website:\n https://www.s-m.com.sa/ \n \n" +


                "For more information about the issue and more details on my GitHub account:\n https://github.com/MohammadYAmmar \n \n"
                + "\n My account in twitter: @mohammad_yammar \n \n"
                + "The program was launched in the first month of 2021, if a program update is issued, it will be in the repository of my GitHub account.";

            MessageBox.Show(Release_information, "Release information : " + "V1", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void run_button_Click(object sender, EventArgs e)
        {
            /*
             * Here the bot communication for transmission and motor movement will be included through HTTP commands
             */
        }

        private void motor_trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

      
    }
}
