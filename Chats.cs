using System;
using System.Collections;





//ST10474357
//Part 2 summarry
namespace ai_response
{
    public class chats
    {
        //global array
        ArrayList answers = new ArrayList();
        ArrayList ignoring = new ArrayList();

        //random object for varied responses
        Random rand = new Random();
        ArrayList PositiveWord = new ArrayList();
        ArrayList NegativeWord = new ArrayList();

        //method to chat
        public void ai_chat(string name)
        {
            //Store all the answers the user
            answers.Add("Password must be 8 characters long and include numbers and symbols");
            answers.Add("Phishing is a scam email that tries to steal your personal information");
            answers.Add("Safe browsing means using https websites and avoiding suspicious links");
            answers.Add("Malware is harmful software that can damage or steal data from your computer");
            answers.Add("Two factor authentication adds an extra layer of security to your account");
            answers.Add("a VPN hides your internet activity and keeps your connection private");
            answers.Add("You should update your software regularly to fix security vulnerabilities");
            answers.Add("Backup your files regularly to protect against data loss or ransomware");
            answers.Add("A Insider threat is a threat from people within the organization itself and can leak or release data" );
            answers.Add("Crypto Jacking is secretly using someones device to mine crypto currency");
            answers.Add("Drive by downloads is Malware that installs itself automatically when you enter a website");
            answers.Add("A ddos attack occurs when a computer server is overu=run by reads and writes making it offline to actual users");

            //store ignore
            ignoring.Add("what");
            ignoring.Add("is");
            ignoring.Add("about");
            ignoring.Add("are");
            ignoring.Add("had");

            ignoring.Add("a");
            ignoring.Add("the");
            ignoring.Add("us");
            ignoring.Add("an");


            //storing ignore pronouns
            ignoring.Add("i");
            ignoring.Add("you");
            ignoring.Add("he");
            ignoring.Add("she");
            ignoring.Add("it");
            ignoring.Add("we");
            ignoring.Add("they");
            ignoring.Add("me");
            ignoring.Add("him");
            ignoring.Add("her");
            ignoring.Add("us");
            ignoring.Add("them");
            ignoring.Add("my");
            ignoring.Add("your");
            ignoring.Add("his");
            ignoring.Add("its");
            ignoring.Add("our");
            ignoring.Add("their");
            ignoring.Add("mine");
            ignoring.Add("yours");

            //storing ignore verbs

            ignoring.Add("be");
            ignoring.Add("am");
            ignoring.Add("is");
            ignoring.Add("are");
            ignoring.Add("was");
            ignoring.Add("have");
            ignoring.Add("were");

            ignoring.Add("has");
            ignoring.Add("had");
            ignoring.Add("do");
            ignoring.Add("did");
            ignoring.Add("does");
            ignoring.Add("will");
            ignoring.Add("can");
            ignoring.Add("could");
    
            ignoring.Add("may");
            ignoring.Add("would");
            ignoring.Add("should");
            ignoring.Add("shall");
  
            ignoring.Add("might");


            //store positive words
            PositiveWord.Add("good");
            PositiveWord.Add("happy");
            PositiveWord.Add("wonderful");

            PositiveWord.Add("great");
            PositiveWord.Add("delighted");
            PositiveWord.Add("pleased");
            PositiveWord.Add("well");
            //All the negative words to maximize user interaction and responsiveness

            NegativeWord.Add("bad");
            NegativeWord.Add("not happy");
            NegativeWord.Add("unhappy");
            NegativeWord.Add("sad");
            NegativeWord.Add("upset");
            NegativeWord.Add("angry");

            //All the Good words to maximize user interaction and responsiveness

            string[] greetingResponses = {

                //Save and compile all possible responses to the user
                "Cyberbot AI: Hello, " + name + "! How can I help you with cybersecurity today?",
                "Cyberbot AI: Hey there " + name + "! Great to see you. What cybersecurity topic can I help with?",
                "Cyberbot AI: Hi " + name + "! I am here to help keep you safe online. What would you like to know?",
                "Cyberbot AI: Welcome back " + name + "! What cybersecurity question do you have today?"
            };

            string[] unknownResponses =
                {
                //Save and randomize all responses to unknown user inputs 
                "Cyberbot AI: I didn't quite understand that. Could you please rephrase?",
                "Cyberbot AI: Hmm, I'm not sure about that. Try asking about passwords, phishing, or VPNs.",
                "Cyberbot AI: I only specialise in cybersecurity topics. Could you rephrase your question?",
                "Cyberbot AI: That's outside my knowledge. Ask me about malware, backups, or 2FA!"
            };

            string[] howAreYouResponses =
                {//All possible responese to how are you from the user 
                "Cyberbot AI: I am doing great, thank you " + name + "! Ready to help with cybersecurity!",
                "Cyberbot AI: All systems running smoothly! How can I help you today, " + name + "?",
                "Cyberbot AI: Feeling secure and protected! What can I help you with, " + name + "?"
            };

            //We starting with the AI logic here :
            string asking = string.Empty;
            do
            {
                Console.WriteLine(name + ": "); 
                asking = Console.ReadLine();
                if (asking == null) asking = "";
                string input = asking.ToLower();

                if (asking == "" || asking == null)//If the user enters an empty string or nothing
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("I dont understand. I only help with CyberSecurity");//Display an appropriate error message
                    Console.ResetColor();
                }
                else if (input == "exit" || input == "quit" || input == "stop")//Keywords the user might enter to stop the progam from running
                {
                    Console.WriteLine("Cyberbot Ai: Goodbye, " + name + "! Stay safe online!");//Display an appropriate goodbye message
                }


                else if (input.Contains("password"))
                {
                    Console.WriteLine("Cyberbot AI: " + answers[0]);
                    //Console will print the ai name and the first value from the answers array
                }


                else if (input.Contains("phishing"))

                {
                    Console.WriteLine("Cyberbot AI: " + answers[1]);
                    //Console will print the ai name and the second value from the answers array
                }



                else if (input.Contains("browsing") || input.Contains("searching") || input.Contains("google"))
                {
                    Console.WriteLine("Cyberbot AI: " + answers[2]);
                    //Console will print the ai name and the third value from the answers array
                }


                else if (input.Contains("malware") || input.Contains("virus"))
                {
                    Console.WriteLine("Cyberbot AI: " + answers[3]);
                    //Console will print the ai name and the forth value from the answers array
                }


                else if (input.Contains("2fa") || input.Contains("two factor authentication") || input.Contains("authentication") || input.Contains("two factor auth"))
                {
                    Console.WriteLine("Cyberbot AI: " + answers[4]);
                    //Console will print the ai name and the fith value from the answers array
                }

                else if (input.Contains("vpn"))
                {
                    Console.WriteLine("Cyberbot AI: " + answers[5]);
                    //Console will print the ai name and the sixth value from the answers array
                }


                else if (input.Contains("update") || input.Contains("patch"))
                {
                    Console.WriteLine("Cyberbot AI: " + answers[6]);
                    //Console will print the ai name and the seventh value from the answers array
                }
                else if (input.Contains("backup") || input.Contains("extra"))
                {
                    Console.WriteLine("Cyberbot AI: " + answers[7]);
                }

                else if (input.Contains("insider threat") || input.Contains("insider-threat"))
                {
                    Console.WriteLine("Cyberbot AI: " + answers[8]);
                }
                else if (input.Contains("CryptoJacking") || input.Contains("crypto-jacking"))
                {
                    Console.WriteLine("Cyberbot AI: " + answers[9]);
                }

                else if (input.Contains("drive by download") || input.Contains("drive-by-download"))
                {
                    Console.WriteLine("Cyberbot AI: " + answers[10]);
                }

                else if (input.Contains("DDOS") || input.Contains("ddos"))
                {
                    Console.WriteLine("Cyberbot AI: " + answers[11]);
                }
                //The user Greeting the AI 
                else if (input.Contains("hello") || input.Contains("hi"))
                {
                    Console.WriteLine(greetingResponses[rand.Next(greetingResponses.Length)]);
                }
                //The user seeing how the AI is doing - moved above positive words to prevent "great/well" intercepting
                else if (input.Contains("how are you"))
                {
                    Console.WriteLine(howAreYouResponses[rand.Next(howAreYouResponses.Length)]);
                }
                else if (input.Contains("what can you help with") || input.Contains("help"))//If user asks what the AI helps with 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Cyberbot AI: I can help you with the following cybersecurity topics:");
                    Console.WriteLine("  1) Passwords        2) Phishing");
                    Console.WriteLine("  3) Safe Browsing     4) Malware & Viruses");
                    Console.WriteLine("  5) 2FA              6) VPNs");
                     Console.WriteLine("  7) Software Updates 8) Backups");

                    Console.ResetColor();
                }

                else if (input.Contains("good") || input.Contains("happy") || input.Contains("wonderful") || input.Contains("great") || input.Contains("delighted") || input.Contains("pleased") || input.Contains("well"))
                {

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Cyberbot AI: That's great to hear, " + name + "! I'm happy to help keep you Stay safe!" );
                    Console.ResetColor();
                }
           
                else if (input.Contains("bad") ||  input.Contains("not happy") || input.Contains("unhappy") || input.Contains("sad") || input.Contains("upset") || input.Contains("angry"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Cyberbot AI:  I'm sorry to hear that, " + name + ". Let me try to help you better. Ask me anything about cybersecurity!");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(unknownResponses[rand.Next(unknownResponses.Length)]);//If the user entered something else display an error
                }

            } while (asking.ToLower() != "exit" && asking.ToLower() != "quit" && asking.ToLower() != "stop");//While the user did not enter the stop prompts keep the program running
        }
    }
}