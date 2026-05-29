using System;
using System.Collections;

namespace ai_response
{
    public class GreetAndName
    {


        //Greet the user and get their name class
        public string GetName()
        {
            Console.WriteLine("Enter your name: ");//Prompt for them to enter their name 
            string name = Console.ReadLine();

            if (name == "" || name == null)
            {
                name = "User";//But if they do not enter a anything their name will just be user
            }

            Console.WriteLine("Welcome " + name + "!");

            return name;
        }
    }
}