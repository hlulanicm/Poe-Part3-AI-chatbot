using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ai_response;






//importing all necessarry tools
using System.Collections;
using ai_response;

namespace Chatbot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// GreetAndName usage to help store usernames
    /// </summary>
    /// 


    //this is the GUI code and how the GUI will operate and handle all user responses
    public partial class MainWindow : Window
    {

        //Creating an arrayList for both the replies and the ignore assets
        ArrayList reply = new ArrayList();
        ArrayList ignore = new ArrayList();

        string username = string.Empty;
        int counting = 0;
        Random indexer = new Random();


      
        //Greet and name the user from Part one of the chatbot
        GreetAndName greeter = new GreetAndName();




        public MainWindow()
        {


                
            


            InitializeComponent();


            //We are loading all the content and storing them in a constructor that has two proper arguments

            new chats(reply, ignore);


            //Play greeting File (File location has changed 

            try
            {
                string wavPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                var player = new System.Media.SoundPlayer(WavPath);
                player.Load();
                player.Play(); 


            }

            catch { }
        }

        private void proceed(object sender, RoutedEventArgs e)

        {

            home_grid.Visibility = Visibility.Hidden;//When the user proceeds the home grid must be hidden 
            username_grid.Visibility= Visibility.Visible;//Make the user input page visible

        }
        private void submit_name(object sender, RoutedEventArgs e)
        {

        String entered = username_input.Text.Trim();//Collecting name from the input box 





            //IF user name is empty display error message
            if (string.IsNullOrEmpty(entered) )
            {
                username_error.Visibility = Visibility.Visible; //Show the user input error message
                return;
        }

            username_error.Visibility = Visibility.Hidden; //User error message is initially hidden



            //Getting the username and using the GreetAndName class to store the userName and to save the users
            username = greeter.GetName(entered);



            //check if the user is returning (if they are display welcome back) 
            bool returning = IsReturningUser(username);
            SaveUser(username);

            username_grid.Visibility = Visibility.Hidden;
            chat_grid.Visibility = Visibility.Visible; //Display the chat grid and hide the username grid


            header_username.Text = "Logged in AS: " + username;

            //Use GreetAndName to build a good welcome message

            String welcomMessage = returning
                ? greeter.WelomeBack(username);
            :greeter.WelcomeNew(username);


            show_message("CyberBot", welcomMessage);

                
                
                
                }






    //Input handling 

  // SEND button
        private void send(object sender, RoutedEventArgs e) => ProcessInput();

        // EXIT button
        private void exit_click(object sender, RoutedEventArgs e) => ExitApp();

        // Enter key sends message
        private void question_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ProcessInput();
        }

        private void ProcessInput()
        {
            string rawQuestion = question.Text.Trim();

            if (string.IsNullOrWhiteSpace(rawQuestion))
            {
                show_message("CyberBot", "Please enter a question.");
                return;
            }

            // Exit keywords typed directly in the chat box
            string lower = rawQuestion.ToLower();
            if (lower == "exit" || lower == "quit" || lower == "stop" ||
                lower == "bye" || lower == "goodbye")
            {
                show_message(username, rawQuestion);
                question.Clear();
                ExitApp();
                return;
            }

            string cleaned = RemoveSpecialCharacters(rawQuestion);

            show_message(username, rawQuestion);
            question.Clear();

            // Show interests reminder every 3 messages
            auto_show_interest();

            // Get AI response
            ai_check(cleaned);
        }
