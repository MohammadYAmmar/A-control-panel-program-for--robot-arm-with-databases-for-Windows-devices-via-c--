using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Control_panel_program_for_the_robot_via_C_sharp
{
    public partial class configuration_database_Form2 : Form
    {
        public configuration_database_Form2()
        {
            InitializeComponent();
        }

        private void Delete_database_button_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult dialogResult = MessageBox.Show("Sure", "Do you agree to delete all information in the database?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    string connectionString = "datasource=localhost; port=3306;username=root;password=;database=Robot-arm-with-a-camera; CharSet=utf8;";//CharSet=utf8 mb4    
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        //Open connection
                        connection.Open();

                        //Query sql to delete
                        var sqlCommand = "DELETE FROM `direction_and_motor_values";


                        //Create mysql command and pass sql query
                        using (var command = new MySqlCommand(sqlCommand, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("All data in the database has been deleted, have a nice day");

                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Watch out for the next times, have a nice day");
                }
            }


            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }


        }
    }

