using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace HMS
{
    class Program
    {
        static void Main(string[] args)
        {
            Login();
        }

        public static void Login()
        {
            // 清空控制台，确保界面整洁
            Console.Clear();

            // 打印标题
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("| ---------------------------------------|");
            Console.WriteLine("|                  Login                 |");
            Console.WriteLine("|----------------------------------------|");

            // 输入 ID
            Console.Write("ID: ");
            string id = Console.ReadLine();

            // 输入密码，隐藏输入字符
            Console.Write("Password: ");
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");  // 打印 * 代替密码字符
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");  // 删除最后一个星号
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            // 验证用户身份
            if (User.ValidateUser(id, password))
            {
                Console.WriteLine("Valid Credentials");  // 显示验证成功
                Console.ReadKey();  // 等待用户按任意键继续
                string userType = User.GetUserType(id);
                User temp=User.GetUser(id);
                switch (userType)
                {
                    case "Admin":
                        Administrator.AdminMenu();
                        break;
                    case "Doctor":
                        Doctor.DoctorMenu(temp);
                        break;
                    case "Patient":
                        Patient.PatientMenu(temp);
                        break;
                    default:
                        Console.WriteLine("Role Input Error");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid Credentials, please try again."); 
                Console.ReadKey(); 
                Login(); 
            }
        }
    }
}
