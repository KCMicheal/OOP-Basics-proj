using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace EmailCollector
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello! and Welcome to the KCM EmailCollector app\n");
            Console.WriteLine("Please input a some words and emails together and\n");
            Console.WriteLine("I'll find them all\n");
            string userInput = Console.ReadLine();

            try
            {
                List<string> emails = new List<string>();
                var emailList = ExtractEmails(userInput);
                Console.WriteLine("\nYour emails are:\n\n------------\n");
                foreach (var email in emailList)
                {
                    Console.WriteLine(email + "\n");
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred! =>" + ex.Message);
                //throw;
            }

           
        }
               
        public static List<string> ExtractEmails(string textToScrape)
        {
            Regex reg = new Regex(@"[a-zA-Z0-9._%+-]+@[a-zA-Z]+(\.[a-zA-Z0-9]+)+", RegexOptions.IgnoreCase);
            Match match;

            List<string> results = new List<string>();
            for (match = reg.Match(textToScrape); match.Success; match = match.NextMatch())
            {
                if (!(results.Contains(match.Value)))
                    results.Add(match.Value);
            }

            return results;
        }

    }
}
