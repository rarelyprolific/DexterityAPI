using System;
using System.Collections.Generic;

namespace DexterityAPI.ConsoleHost
{
    using Data;
    using Data.Interfaces;
    using Domain;

    class Program
    {
        static void Main(string[] args)
        {
            RunMenu();
        }

        static private void DisplayWelcomeAndMenuOptions()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("************************************************");
            Console.WriteLine("** Console Test Application for Dexterity API **");
            Console.WriteLine("************************************************");

            Console.WriteLine("Quick 'n' dirty interface to check database and Entity Framework looks good before implementing the WebAPI");
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Options: 1 - Show All Users,    2 - Add User,        3 - Edit User");
            Console.WriteLine("         4 - Enable User,       5 - Disable User,    6 - Delete User,    0 - Exit");
        }

        static private void RunMenu()
        {
            DisplayWelcomeAndMenuOptions();

            ConsoleKeyInfo selectedOption = Console.ReadKey();
            Console.Clear();

            switch (selectedOption.Key)
            {
                case ConsoleKey.D1:
                    GetUsers();
                    break;
                case ConsoleKey.D2:
                    AddUser();
                    break;
                case ConsoleKey.D3:
                    EditUser();
                    break;
                case ConsoleKey.D4:
                    EnableUser();
                    break;
                case ConsoleKey.D5:
                    DisableUser();
                    break;
                case ConsoleKey.D6:
                    DeleteUser();
                    break;
                case ConsoleKey.D0:
                    return;
                default:
                    Console.WriteLine("Invalid option [{0}]", selectedOption.Key);
                    break;
            }

            RunMenu();
        }

        static IUserRepository GetUserRepository()
        {
            return new DisconnectedUserRepository(new DexterityContext());
        }

        static private void GetUsers()
        {
            IUserRepository userRepository = GetUserRepository();
            IEnumerable<User> users = userRepository.GetUsers();

            foreach (var user in users)
            {
                Console.WriteLine("{0}: {1} {2} [{3}] Enabled:{4}", user.Id, user.Forename, user.Surname, user.EmailAddress, user.IsEnabled);
            }
        }

        static private void AddUser()
        {
            IUserRepository userRepository = GetUserRepository();

            var user = new User { IsEnabled = true };

            Console.Write("Enter forename of user: ");
            user.Forename = Console.ReadLine();

            Console.Write("Enter surname of user: ");
            user.Surname = Console.ReadLine();

            Console.Write("Enter email address of user: ");
            user.EmailAddress = Console.ReadLine();

            try
            {
                User addedUser = userRepository.AddUser(user);
                Console.WriteLine("Added user [{0}]", addedUser.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        static private void EditUser()
        {
            IUserRepository userRepository = GetUserRepository();

            GetUsers();

            int userId = PromptForUserId();

            if (userId == 0) return;
            var existingUserToEdit = new User { Id = userId };

            Console.Write("Enter forename of user: ");
            existingUserToEdit.Forename = Console.ReadLine();

            Console.Write("Enter surname of user: ");
            existingUserToEdit.Surname = Console.ReadLine();

            Console.Write("Enter email address of user: ");
            existingUserToEdit.EmailAddress = Console.ReadLine();

            try
            {
                userRepository.EditUser(existingUserToEdit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        static private void EnableUser()
        {
            IUserRepository userRepository = GetUserRepository();

            GetUsers();
            int userId = PromptForUserId();
            if (userId == 0) return;

            try
            {
                userRepository.EnableUser(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static private void DisableUser()
        {
            IUserRepository userRepository = GetUserRepository();

            GetUsers();
            int userId = PromptForUserId();
            if (userId == 0) return;

            try
            {
                userRepository.DisableUser(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static private void DeleteUser()
        {
            IUserRepository userRepository = GetUserRepository();

            GetUsers();
            int userId = PromptForUserId();
            if (userId == 0) return;

            try
            {
                userRepository.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static private int PromptForUserId()
        {
            int userId;

            Console.Write("Enter id of user (or zero to cancel): ");

            string rawUserInput = Console.ReadLine();

            if (!int.TryParse(rawUserInput, out userId))
            {
                PromptForUserId();
            }

            return userId;
        }
    }
}
