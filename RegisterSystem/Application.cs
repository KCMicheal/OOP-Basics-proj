using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Registration
{
    public static class Application
    {
        public static void Run()
        {
            var menu = new StringBuilder();
            menu.Append("Hello, Welcome to my App ");
            menu.AppendLine("Enter your Firstname ");

            Console.WriteLine(menu.ToString());

            var firstName = Console.ReadLine();

            while (IsBlank(firstName))
            {
                Console.WriteLine($"No empty field required , Enter your FirstName");
                firstName = Console.ReadLine();
            }

            Console.WriteLine("Enter your Lastname");
            var lastName = Console.ReadLine();

            while (IsBlank(lastName))
            {
                Console.WriteLine($"No empty field required , Enter your Lastname");
                lastName = Console.ReadLine();
            }

            
            Console.WriteLine("Enter your email");
            var email = Console.ReadLine();
            while (IsBlank(email))
            {
                Console.WriteLine("No empty field required , Enter your email");
                email = Console.ReadLine();
            }

            Console.WriteLine("Enter your Date of Birth");
            var birthday = Console.ReadLine();

            while (!isValidDate(birthday))
            {
                Console.WriteLine($"Enter a Valid Date Format eg 'dd/mm/yyyy' or 'yyyy/mm/dd' ");
                birthday = Console.ReadLine();
            }


            Console.WriteLine("Select your gender:\n 1:  Male\n 2: Female\n 3: Prefer Not To Say ");
            var gender = Console.ReadLine();

            while (gender != "1" && gender != "2" && gender != "3")
            {
                Console.WriteLine($"Invalid Option, select a valid options;\n '1' " +
                    $"for Male \n '2' for Female \n '3' for Prefer Not To Say ");
                gender = Console.ReadLine();
            }
            var selectedGender = GenderSelection(gender);

            Console.WriteLine("Enter your password");
            var password = Console.ReadLine();

            while (IsBlank(password))
            {
                Console.WriteLine("Password cannot be empty or spaces: Re-Enter Password");
                password = Console.ReadLine();
            }


            Console.WriteLine("Confirm your password");
            var confirmPassword = Console.ReadLine();

            while (IsBlank(confirmPassword))
            {
                Console.WriteLine($"Password cannot be empty or spaces: Re-Enter Confirm Password");
                confirmPassword = Console.ReadLine();
            }




            try
            {
                var formData = new Register
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Birthday = DateTime.Parse(birthday),
                    Password = password,
                    ConfirmPassword = confirmPassword,
                    Gender = selectedGender
                };

                AccountService.Register(formData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured!" + ex.Message);
                //throw;
            }
            
        }

        private static Gender GenderSelection(string gender)
        {

            switch (gender)
            {
                case "1":
                    return Gender.Male;

                case "2":
                    return Gender.Female;


                case "3":
                    return Gender.PreferNotToSay;


                default:
                    return Gender.SelectGender;
            }

        }

        private static bool IsBlank(string input)
        {   // returns false if not blank and true if blank
            if (!string.IsNullOrWhiteSpace(input))
                return false;
            return true;
        }

        private static bool isValidDate(string date)
        {
            DateTime result;
            if (!DateTime.TryParse(date, out result) || string.IsNullOrWhiteSpace(date))
                return false;
            return true;
        }
    }
}
