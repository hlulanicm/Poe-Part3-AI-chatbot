using System;

namespace ai_response
{

    //  GreetAndName.cs:  Handles all greeting and farewell messages.
    //  MainWindow calls these methods so all user facing text lives here.
    //IN this file i updated Greet and Name t



    public class GreetAndName
    {
        private Random rand = new Random();



        public string GetName( string enteredName)
        {
            if (string.IsNullOrEmpty(enteredName))
                return "user ";
            //if the user does not entere any names the system must just save them as user


            return enteredName.Trim();

        }

        public string WelcomeNew( string name ) {
            "Hello " + name + " I am cyber bot your cybersecurity AI assistant how can i help you",
            "Hi " + name + "! I am your cybersecurity awareness companion. Feel free to ask me about passwords, phishing, malware, and more.",
            "Hey there, " + name + "! I am CyberBot AI, ready to help you stay safe in the digital world. What is on your mind?",
            };

            return messages[rand.Next(messages.Length)];


public string Goodbye(string name) 
        {
            string displayName = string.IsNullOrWhiteSpace(name) ? "friend" : name;

            string[] messages =
           {
                "Goodbye, " + displayName + "! Stay safe online!",
                "Take care, " + displayName + "! Remember to stay vigilant online.",
                "See you next time, " + displayName + "! Keep your passwords strong and your guard up.",
                "Farewell, " + displayName + "! Stay cyber-safe out there."
            };
            return messages[rand.Next(messages.Length)];




        }
}
