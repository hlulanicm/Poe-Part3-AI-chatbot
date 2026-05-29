using ai_response;
using System;
using System.Collections;
//importing all necessarry tools
using System.Collections;
using System.Collections.Generic;
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
using static System.Net.Mime.MediaTypeNames;

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
                var player = new System.Media.SoundPlayer(wavPath); // FIX 1: was WavPath (wrong casing)
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
                ? greeter.WelomeBack(username)
                : greeter.WelcomeNew(username);


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
            }

            string[] words = input.ToLower().Split(
                new char[] { ' ', ' ', ' ', ' ', }, StringSplitOptions.RemoveEmptyEntries);

            bool found = false;
            string message = string.Empty;
            List<string> per_word = new List<string>();
            List<string> answers_found = new List<string>();

            foreach (string word in words)
            {
                if (word.Length < 3 || ignore.Contains(word.ToLower()))
                    continue;

                per_word.Clear();

               //if the user enters a like indicatot meaning they like a certain topic we will save the interest
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
                    // FIX 7: was malformed ternary split across lines with wrong punctuation
                    string display = colonIdx >= 0
                        ? chosen.Substring(colonIdx + 1).Trim()
                        : chosen;

                    answers_found.Add(display);


                }

            }

            if (found && answers_found.Count > 0)
            {
                answers_found = answers_found.Distinct().ToList();
                foreach (string ans in answers_found)
                    message += ans + "\n";
                show_message("CyberBot", message.TrimEnd('\n'));
            }
            else if (!string.IsNullOrWhiteSpace(message))
            {
                show_message("CyberBot", message.Trim());
            }
            else
            {
                show_message("CyberBot", bot.GetFallback());
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
            texblk.Inlines.Add(new Run(name + ": ")
            {
                Foreground = Brushes.Red,
                FontWeight = FontWeights.Bold
            });
            texblk.Inlines.Add(new Run(message) { Foreground = Brushes.White });

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
            show_message("CyberBot", greeter.Goodbye(username));
            System.Threading.Tasks.Task.Delay(1500).ContinueWith(_ =>
                Dispatcher.Invoke(() => Application.Current.Shutdown()));
        }

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
}