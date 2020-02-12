using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace CheckMoodleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string username = "";
                string password = "";

                Console.WriteLine("Enter your Moodle credentials: ");
                Console.Write("Username: ");

                username = Console.ReadLine();

                Console.Write("Password: ");
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    password += key.KeyChar;
                }

                // Based on the user credentials we get the token for the user and courses info requests.
                ApiCallHandler.SetToken(username, password);

                // First we get the user id 
                ApiCallHandler.SetUserId();

                // Then we get the courses in which the user is enrolled
                var courses = ApiCallHandler.GetCourses();

                Console.WriteLine("\n\nAvailable courses: ");

                int i;
                for (i = 0; i < courses.Count; i++)
                {
                    Console.WriteLine(String.Format(" {0,2}", i + 1) + ". " + courses[i].DisplayName);
                }

                Console.Write("\nEnter the number of the course you would like to check for updates: ");
                string number = Console.ReadLine();
                int courseNumber = 0;

                int.TryParse(number, out courseNumber);

                while (courseNumber < 1 || courseNumber > courses.Count)
                {
                    Console.WriteLine("Invalid input. Please enter one of the numbers of the available courses. ");
                    Console.Write("Course number: ");
                    number = Console.ReadLine();

                    int.TryParse(number, out courseNumber);
                }

                string selectedCourseId = courses[courseNumber - 1].Id;

                // We get the initial course contents

                string originalCourseContents = ApiCallHandler.GetCourseContents(selectedCourseId);
                string lastCourseContents = originalCourseContents;

                Console.WriteLine("\nCourse contents successfully acquired. " +
                    "You will be notified when there are any changes to the course contents. " +
                    "Please leave your volume high to hear the alarm.");

                // We keep checking for updates every 2 seconds
                
                while (lastCourseContents == originalCourseContents)
                {
                    Thread.Sleep(2000);
                    lastCourseContents = ApiCallHandler.GetCourseContents(selectedCourseId);
                }

                // This will beep for about 5 minutes

                i = 0;
                while (i < 150)
                {
                    Console.Beep(2000, 1000);
                    Console.Beep(1500, 1000);
                    i++;
                }
            }
            catch
            {
                Console.WriteLine("\nAn error appeared. Program ended."); 
            }

        }
    }
}
