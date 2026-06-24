
using System.Data.SqlClient;



namespace Chatbot
{//start of namespace
    public class user_tasks
    {

        //global connetion string generation

        string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=cyberbot_db;Integrated Security=True;";
        //

        //testomg connection

        public void test_connection()
        {
            SqlCommand connect = new SqlCommand();

        }



    }//end of class
}//end of namespace