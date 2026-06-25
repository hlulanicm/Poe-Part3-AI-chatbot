using System;
using Microsoft.Data.SqlClient;

namespace Chatbot
{
    public class user_tasks
    {
        // Connect to master first to create the database
        string masterConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";

        string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=tasks_DB;Integrated Security=True;";

        // Call this when the app starts
        public void initialize_database()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(masterConnection))
                {
                    conn.Open();

                    // Create database if it doesn't exist
                    string createDB = @"IF NOT EXISTS 
                        (SELECT name FROM sys.databases WHERE name = 'tasks_DB')
                        CREATE DATABASE tasks_DB";

                    new SqlCommand(createDB, conn).ExecuteNonQuery();

                    // Create table if it doesn't exist
                    string createTable = @"USE tasks_DB;
                        IF NOT EXISTS 
                        (SELECT * FROM sysobjects WHERE name='all_tasks')
                        CREATE TABLE all_tasks (
                            task_id INT PRIMARY KEY IDENTITY(1,1),
                            task_name VARCHAR(50),
                            description_task VARCHAR(100),
                            task_date VARCHAR(20),
                            task_status VARCHAR(20)
                        )";

                    new SqlCommand(createTable, conn).ExecuteNonQuery();
                    Console.WriteLine("Database ready!");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Setup failed: " + error.Message);
            }
        }
    }
}