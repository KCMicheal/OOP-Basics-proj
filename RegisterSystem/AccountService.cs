using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration
{
    public static class AccountService
    {
        private static User _user;
        public static void Register(Register model)
        {

            model.Password = ReInputPassword(model.Password, "password");
            model.ConfirmPassword = ReInputPassword(model.ConfirmPassword, "confirm Password");

            var password = PasswordValidator(model.Password, model.ConfirmPassword);


            while (password == "false")
            {
                Console.WriteLine($"Passwords doesn't match!!, Re-Enter Password");
                var tempPassword = Console.ReadLine();
                Console.WriteLine($"Confirm Password: ");
                var tempConfirmPassword = Console.ReadLine();
                password = PasswordValidator(tempPassword, tempConfirmPassword);
            }
            _user = new User(model.Birthday)
            {
                Fullname = $"{model.FirstName.ToUpper()} {model.LastName.ToUpper()}",
                Gender = model.Gender,
                Email = model.Email,
                Password = password
            };

            Console.WriteLine($"Registration successful, {_user.Fullname}");
            Console.WriteLine($"Press 1 to login: \n");
            var input = Console.ReadLine();

            while (input != "1")
            {
                Console.WriteLine($"Press 1 to Login");
                input = Console.ReadLine();
            }
            if (input == "1")
            {
                Console.WriteLine("Enter your Email and Password seperated with a space");
                var credentials = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(credentials))
                {
                    Console.WriteLine($"Field cannot be empty or spaces");
                    Console.WriteLine($"Re-Enter Email and password separated with a space");
                    credentials = Console.ReadLine();
                }

                if (!string.IsNullOrWhiteSpace(credentials))
                {

                    // we cannot have a space in between password
                    while (credentials.Split().Length > 2 || credentials.Split().Length < 2)
                    {
                        Console.WriteLine($"Field should contain Email and password," +
                            $" seperated with a space.\n NB: Password Cannot contain space");
                        Console.WriteLine($"Re-Enter Email and password separated with a space");
                        credentials = Console.ReadLine();

                    }
                    var email = credentials.Split()[0].Trim().ToLower();
                    var passWord = credentials.Split()[1];
                    Login(email, passWord);

                }
                else
                {
                    Console.WriteLine(" The Field Is required");
                }





            }

            else
            {
                Console.WriteLine("invalid Input");
            }



        }

        public static void Login(string email, string password)
        {
            while (_user.Email.ToLower() != email.ToLower() || _user.Password != password)
            {
                Console.WriteLine($"Incorrect Email/Password!!!, Try Again");
                Console.WriteLine($"Enter your Email: ");
                email = Console.ReadLine();
                Console.WriteLine($"Enter your Password");
                password = Console.ReadLine();
            }
            if (_user.Email.ToLower() == email.ToLower() && _user.Password == password)
            {
                Console.WriteLine($"Login sucessful, {_user.Fullname}");
                Console.WriteLine($"Will you like to show Birthday in Profile: \nPress 1 for Yes\n" +
                    $"Press 2 for No ");
                string birthdayDisplay = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(birthdayDisplay) || birthdayDisplay != "1" && birthdayDisplay != "2")
                {
                    Console.WriteLine($"Enter a Valid Option:\nPress 1 for Yes\nPress 2 for No");
                    birthdayDisplay = Console.ReadLine();
                }
                switch (birthdayDisplay)
                {
                    case "1":
                        Console.WriteLine($"Welcome to your Profile.\n" +
                        $" Fullname: {_user.Fullname}\n" +
                        $" Email: {_user.Email}\n Gender: {_user.Gender}\n Age: {_user.Age}\n" +
                        $" Birthday: {_user.Birthday}\n Joined: {_user.Created}");
                        break;

                    case "2":
                        _user.ToggleAgePrivacy();
                        Console.WriteLine($"Welcome to your Profile.\n Fullname: {_user.Fullname}\n" +
                        $" Email: {_user.Email}\n Gender: {_user.Gender}\n Joined: {_user.Created}");
                        break;

                    default:
                        Console.WriteLine($"Invalid Options");
                        break;
                }

            }
            /* else
             {
                 Console.WriteLine($"invalid Login Details");
             }*/
        }

        private static string PasswordValidator(string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
                return string.Empty;

            return password == confirmPassword ? password : "false"; //do not trim password
        }

        private static string ReInputPassword(string password, string type)
        {
            while (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine($"password cannot be whitespace, Enter {type}:");
                password = Console.ReadLine();
            }
            return password;
        }



    }
}
