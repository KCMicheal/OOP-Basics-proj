using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public static class Application
    {
        public static void Run()
        {
            Console.WriteLine("You've reached the ATM Console Application\n");
            Console.WriteLine("Please enter your Name");
            string name = Console.ReadLine().Trim();

            while (string.IsNullOrWhiteSpace(name))
            {
                name = PromptUser("name");
            }

            Console.WriteLine("\nSet your 4-digit PIN");
            string pin = RequestPIN();

            while (string.IsNullOrWhiteSpace(pin) || pin.Length < 4 || pin.Length > 4)
            {
                pin = PromptPin();
            }

            int amountDeposited = 0;
            do
            {
                Console.WriteLine("\nHow much do you want to deposit?");
                string accountBalance = Console.ReadLine().Trim();
                while (string.IsNullOrWhiteSpace(accountBalance) || accountBalance.Split().Length > 1 || !(int.TryParse(accountBalance, out amountDeposited)))
                {
                    accountBalance = PromptUser("amount");
                }
                if (amountDeposited < 5000)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    int minimumDeposit = 5000;
                    Console.WriteLine($"Minimum deposit is N{minimumDeposit:n}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (amountDeposited < 5000);
            
            User user = new User
            {
                Name = name,
                Pin = pin,
                AccountBalance = amountDeposited
            };

            ATMService.Register(user);

            ATMService.ShowPin();
            bool isLoggedIn = false;

            Language language = ATMService.SelectLanguage();

            if (language == Language.English)
            {
                ATMService.ValidatePinEnglish(isLoggedIn);

                bool HasOperationEnded = false;
                while (!HasOperationEnded)
                {
                    AccountOperation accountOperation = ATMService.SelectOperationEnglish();

                    if (accountOperation == AccountOperation.Withdrawal)
                    {
                        ATMService.WithdrawalHandler(
                            ATMService.EnglishWithdrawalOption(),
                            Translation.EnglishWithdrawalSuccessMessage(),
                            Translation.EnglishWithdrawalErrorMessage(),
                            Translation.EnglishInsuffientFunds(),
                            Translation.EnglishDispenseError()
                        );
                    }

                    if (accountOperation == AccountOperation.CheckBalance)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        int accountBalance = ATMService.CheckBalance();
                        Console.WriteLine($"\nYour Account Balance is N{accountBalance:n}\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (accountOperation == AccountOperation.End)
                    {
                        Console.WriteLine("Your session has ended!");
                        HasOperationEnded = true;
                    }
                }
            }

            if (language == Language.Pidgin)
            {
                ATMService.ValidatePinPidgin(isLoggedIn);

                bool HasOperationEnded = false;
                while (!HasOperationEnded)
                {
                    AccountOperation accountOperation = ATMService.SelectOperationPidgin();

                    if (accountOperation == AccountOperation.Withdrawal)
                    {
                        ATMService.WithdrawalHandler(
                            ATMService.PidginWithdrawalOption(),
                            Translation.PidginWithdrawalSuccessMessage(),
                            Translation.PidginWithdrawalErrorMessage(),
                            Translation.PidginInsuffientFunds(),
                            Translation.PidginDispenseError()
                        );

                    }

                    if (accountOperation == AccountOperation.CheckBalance)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        int accountBalance = ATMService.CheckBalance();
                        Console.WriteLine($"\nAll your money na N{accountBalance:n}\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (accountOperation == AccountOperation.End)
                    {
                        Console.WriteLine("Abeg getat!");
                        HasOperationEnded = true;
                    }
                }
            }   
                
            if (language == Language.Igbo)
            {
                ATMService.ValidatePinIgbo(isLoggedIn);

                bool HasOperationEnded = false;
                while (!HasOperationEnded)
                {
                    AccountOperation accountOperation = ATMService.SelectOperationIgbo();

                    if (accountOperation == AccountOperation.Withdrawal)
                    {
                        ATMService.WithdrawalHandler(
                            ATMService.IgboWithdrawalOption(), 
                            Translation.IgboWithdrawalSuccessMessage(), 
                            Translation.IgboWithdrawalErrorMessage(), 
                            Translation.IgboInsuffientFunds(), 
                            Translation.IgboDispenseError()
                        );

                    }

                    if (accountOperation == AccountOperation.CheckBalance)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        int accountBalance = ATMService.CheckBalance();
                        Console.WriteLine($"\nEgo N'ile I Nwere Bu N{accountBalance:n}\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (accountOperation == AccountOperation.End)
                    {
                        Console.WriteLine("Ogwucha la!");
                        HasOperationEnded = true;
                    }
                }
            }
        }

        public static string RequestPIN()
        {
            // store characters in stringbuilder to be able to build the * characters
            StringBuilder stringBuilder = new StringBuilder();

            // variable for the console keys information
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true); // sets the console to not display the character

                // check if key pressed is a numberkey
                if (char.IsDigit(keyInfo.KeyChar))
                {
                    stringBuilder.Append(keyInfo.KeyChar);
                    Console.Write("*");
                }

            } while (keyInfo.Key != ConsoleKey.Enter); // while the key we're hitting is not Enter key
            return stringBuilder.ToString();
        }

        private static string PromptPin()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPIN must be 4-digits!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nSet your 4-digit PIN");
            return RequestPIN();
        }

        private static string PromptUser(string fieldName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Please, enter a valid {fieldName}");
            Console.ForegroundColor = ConsoleColor.White;
            return Console.ReadLine().Trim();
        }

    }
}
