using System;
using System.Collections.Generic;
using App.Models;
using App.Tasks;

namespace App.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                System.Console.WriteLine("Выберите задачу:");
                System.Console.WriteLine("1. Basic Validation (Parse string to int)");
                System.Console.WriteLine("2. Custom Exceptions (Password validation)");
                System.Console.WriteLine("3. Advanced Registration (User registration)");
                System.Console.WriteLine("0. Выход");

                string choice = System.Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RunTask1();
                        break;
                    case "2":
                        RunTask2();
                        break;
                    case "3":
                        RunTask3();
                        break;
                    case "0":
                        return;
                    default:
                        System.Console.WriteLine("Неверный выбор");
                        break;
                }

                System.Console.WriteLine();
            }
        }

        static void RunTask1()
        {
            System.Console.WriteLine("Введите число для парсинга:");
            string input = System.Console.ReadLine();
            int result = Task1_BasicValidation.ParseStringToInt(input);
            System.Console.WriteLine($"Результат: {result}");
        }

        static void RunTask2()
        {
            System.Console.WriteLine("Введите пароль для валидации:");
            string password = System.Console.ReadLine();

            try
            {
                Task2_CustomExceptions.ValidatePassword(password);
                System.Console.WriteLine("Пароль корректен!");
            }
            catch (InvalidPasswordException ex)
            {
                System.Console.WriteLine($"Ошибка валидации пароля: {ex.Message}");
            }
        }

        static void RunTask3()
        {
            System.Console.WriteLine("Генерация тестовых пользователей...");
            List<User> users = UserService.GenerateUsers(5);

            foreach (var user in users)
            {
                try
                {
                    UserService.RegisterUser(user);
                    System.Console.WriteLine($"Пользователь {user.FirstName} {user.LastName} успешно зарегистрирован!");
                }
                catch (UnderageUserException ex)
                {
                    System.Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (UserRegistrationException ex)
                {
                    System.Console.WriteLine($"Ошибка регистрации: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        System.Console.WriteLine($"Внутренняя ошибка: {ex.InnerException.Message}");
                    }
                }
            }
        }
    }
}