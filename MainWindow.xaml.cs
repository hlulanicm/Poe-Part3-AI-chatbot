using ai_response;
using System;
using System.Collections;
//importing all necessarry tools
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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

        //Tracker and bot declared as fields so they are accessible across all methods
        InterestTracker tracker = new InterestTracker();
        FallbackResponse bot = new FallbackResponse();




        public MainWindow()
        {




            InitializeComponent();


            //We are loading all the content and storing them in a constructor that has two proper arguments

            new chats(reply, ignore);


            //Play greeting File (File location has changed 

            try
            {
                string wavPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                var player = new System.Media.SoundPlayer(wavPath);
                player.Load();
                player.Play();


            }

            catch { }
        }

        private void proceed(object sender, RoutedEventArgs e)

        {

            home_grid.Visibility = Visibility.Hidden;//When the user proceeds the home grid must be hidden 
            username_grid.Visibility = Visibility.Visible;//Make the user input page visible

        }
        private void submit_name(object sender, RoutedEventArgs e)
        {

            String entered = username_input.Text.Trim();//Collecting name from the input box 


            //IF user name is empty display error message
            if (string.IsNullOrEmpty(entered))
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

            // FIX 2: was malformed ternary with misplaced semicolon and colon
            String welcomMessage = returning
                ? greeter.WelcomeBack(username)
                : greeter.NewUserGreeting(username);


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
                //prompting the user toenter a question
                show_message("CyberBot", "Please enter a question.");
                return;
            }


            //Input validation (end process)
            //IF THE user inputs any exit questions we will stop the process 
            string lower = rawQuestion.ToLower();
            if (lower == "exit" || lower == "quit" || lower == "stop" ||
                lower == "bye" || lower == "goodbye")
            {
                show_message(username, rawQuestion);
                question.Clear();
                ExitApp();
                return;
            }
            //Input validation (special characters)
            //Regecting any special characters 
            String cleaned = Regex.Replace(rawQuestion, @"[^a-zA-Z0-9\s'-]", " ").Trim();

            show_message(username, rawQuestion);
            question.Clear();


            auto_show_interest();

            ai_check(cleaned);

        }

        private void ai_check(string input)
        {

            if (string.IsNullOrWhiteSpace(input))
            {
                show_message("CyberBot", "Please enter a valid question no Special characters e.g @#$%^$%^&");
                return;

                //If the user has entered any special characters throw an appropriate error
            }


            string[] words = input.ToLower().Split(
                   new char[] { ' ', ',', '.', '?', '!', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);//slit all words into an array to ensure no blank words are stored

            bool found = false;
            string message = string.Empty;
            List<string> per_word = new List<string>();//Array created for storing the words and ensuring proper memory management
            List<string> answers_found = new List<string>();


            //if the first word is less than 3 words the Ai will read the next word and skip the current word (less than 3) 
            foreach (string word in words)
            {
                if (word.Length < 3 || ignore.Contains(word.ToLower()))
                    continue; //ignore short word

                per_word.Clear();

                //if the user enters a like indicator meaning they like a certain topic we will save the interest
                if (word.Contains("interested") || word.Contains("like"))
                {
                    message += tracker.SaveInterests(username, words, ignore) + "";
                }

                foreach (string answer in reply)
                {
                    if (answer.ToLower().Contains(word))
                    {
                        found = true;
                        per_word.Add(answer);

                    }

                }

                if (per_word.Count > 0)
                {
                    String chosen = per_word[indexer.Next(0, per_word.Count)];
                    int colonIdx = chosen.IndexOf(':');

                    string display = colonIdx >= 0
                        ? chosen.Substring(colonIdx + 1).Trim()
                        : chosen;

                    answers_found.Add(display);


                }

            }
            //Final input validation processs the ai will peform to ensure smooth flow and validity of user input
            if (found && answers_found.Count > 0)
            {
                answers_found = answers_found.Distinct().ToList();
                foreach (string ans in answers_found)

                    message += ans + "\n";
                show_message("CyberBot", message.TrimEnd('\n'));
            }

            //no key word found but user saved an interest (error)
            else if (!string.IsNullOrWhiteSpace(message))
            {
                show_message("CyberBot", message.Trim());
            }
            else
            {
                show_message("CyberBot", bot.GetFallback());//if the chatbot does not understand what the user said show an appropriate error message
            }
        }

        private void auto_show_interest()
        {
            counting++;
            if (counting < 3) return;

            counting = 0;

            string interests = tracker.GetInterests(username);
            if (string.IsNullOrWhiteSpace(interests)) return;

            show_message("CyberBot", "Just a reminder - you are interested in: " + interests);
            ai_check(Regex.Replace(interests, @"[^a-zA-Z0-9\s'-]", " ").Trim());
        }

        private void show_message(string name, string message)
        {

            TextBlock texblk = new TextBlock { TextWrapping = TextWrapping.Wrap };
            //Styles for the text block the user text and the AI must have colors




            texblk.Inlines.Add(new Run(name + ": ")
            {

                Foreground = Brushes.Blue,
                FontWeight = FontWeights.Bold
            });
            texblk.Inlines.Add(new Run(message) { Foreground = Brushes.White }); //the dialog text must be white but the user names must be blue

            Border bubble = new Border
            {
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(10, 6, 10, 6),
                Child = texblk
            };

            if (name == "CyberBot")
            {
                bubble.Background = new SolidColorBrush(Color.FromRgb(20, 44, 75));
                bubble.HorizontalAlignment = HorizontalAlignment.Left;

                bubble.Margin = new Thickness(0, 3, 80, 3);

                texblk.Inlines.ElementAt(0).Foreground = Brushes.Red;

                //The cyber bot color must be red 
            }
            else
            {
                bubble.Background = new SolidColorBrush(Color.FromRgb(30, 60, 100));
                bubble.HorizontalAlignment = HorizontalAlignment.Right;
                bubble.Margin = new Thickness(80, 3, 0, 3);
            }

            chat_panel.Children.Add(bubble);
            chat_scroll.ScrollToBottom();
        }

        private void ExitApp()
        {

            //display an appropriate error message when the user wants to exit 
            show_message("CyberBot", greeter.Goodbye(username));

            //delay for a few moments to avoid immediate closure
            System.Threading.Tasks.Task.Delay(1500).ContinueWith(_ =>
                Dispatcher.Invoke(() => System.Windows.Application.Current.Shutdown()));
        }

        //Extra Feature saving the user preferences in A text File


        //Method to check if the user is new or returning by comparing stored users(Will be removed Part 3 for a DB)
        private bool IsReturningUser(string name)
        {
            if (!File.Exists("users.txt")) return false;
            return File.ReadAllLines("users.txt")
                       .Any(l => l.Trim().Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private void SaveUser(string name)
        {
            if (!IsReturningUser(name))
                File.AppendAllText("users.txt", name + Environment.NewLine);
        }
    }
}//End of Namespace