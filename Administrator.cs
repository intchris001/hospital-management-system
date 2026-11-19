using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Security.Policy;
using System.Xml.Linq;

namespace HMS
{
    class Administrator
    {
        public static void AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("| ---------------------------------------|");
            Console.WriteLine("|          Administrator Menu            |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("Welcome to DOTNET Hospital Management System admin\n");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List all doctors");
            Console.WriteLine("2. Check doctor details");
            Console.WriteLine("3. List all patients");
            Console.WriteLine("4. Check patient details");
            Console.WriteLine("5. Add doctor");
            Console.WriteLine("6. Add patient");
            Console.WriteLine("7. Logout");
            Console.WriteLine("8. Exit\n");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ListAllDoctors();
                    break;
                case "2":
                    CheckDoctorDetails();
                    break;
                case "3":
                    ListAllPatients();
                    break;
                case "4":
                    CheckPatientDetails();
                    break;
                case "5":
                    AddDoctor();
                    break;
                case "6":
                    AddPatient();
                    break;
                case "7":
                    Program.Login();  // Assuming Login method is static and accessible
                    break;
                case "8":
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.\n");
                    AdminMenu();
                    break;
            }
        }
        private static void ListAllDoctors() { /* 实现列出所有医生的逻辑 */
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("| ---------------------------------------|");
            Console.WriteLine("|              All Doctors               |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine();
            Console.WriteLine("All doctors registed to the DOTNET Hospital Management System");
            Console.WriteLine();
            var users = User.LoadUsers();
            Console.WriteLine("Name\t\t| Email Address\t\t\t| Phone\t\t| Address");
            Console.WriteLine(new string('-', 80)); // 打印分隔线
            foreach (var user in users)
            {
                if (user.Role == "Doctor")
                {
                    Console.WriteLine("{0,-15} | {1,-29} | {2,-13} | {3,-30}",
                         user.Name ,  user.Email, user.Phone ,user.Address);
                }
            }
            Console.ReadKey();
            AdminMenu();

        }
        private static void CheckDoctorDetails() { /* 实现检查医生详细信息的逻辑 */
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("| ---------------------------------------|");
            Console.WriteLine("|              Doctor Details            |");
            Console.WriteLine("|----------------------------------------|");
            Console.Write("Please enter the ID of the doctor whose details you are checking. Or press 'n' to return to menu: \n");
            string input = Console.ReadLine();
            if (input.ToLower() == "n")
            {
                return; // Return to the main menu
            }
            var users = User.LoadUsers();
            bool doctorFound = false;
            foreach (var user in users)
            {
                if (user != null && user.Role == "Doctor" && user.Id == input)
                {
                    Console.WriteLine("Details for {0}\n", user.Name);
                    Console.WriteLine("Name\t\t| Email Address\t\t\t| Phone\t\t| Address");
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine("{0,-15} | {1,-29} | {2,-13} | {3,-30}", user.Name, user.Email, user.Phone, user.Address);
                    doctorFound = true;
                    break;
                }
            }
            if (!doctorFound)
            {
                Console.WriteLine("No doctor found with ID {0}.", input); // Show error if no doctor found
            }
            Console.ReadKey();
            AdminMenu();
        }
        private static void ListAllPatients() { 
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("| ---------------------------------------|");
            Console.WriteLine("|              All Patients                |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine();
            Console.WriteLine("All patients registed to the DOTNET Hospital Management System");
            Console.WriteLine();
            var users = User.LoadUsers();
            Console.WriteLine("Name\t\t| Email Address\t\t\t| Phone\t\t| Address");
            Console.WriteLine(new string('-', 80));
            foreach (var user in users)
            {
                if (user.Role == "Patient")
                {
                    Console.WriteLine("{0,-15} | {1,-29} | {2,-13} | {3,-30}",
                         user.Name, user.Email, user.Phone, user.Address);
                }
            }
            Console.ReadKey();
            AdminMenu();
        }
        private static void CheckPatientDetails() {
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("| ---------------------------------------|");
            Console.WriteLine("|              Patient Details           |");
            Console.WriteLine("|----------------------------------------|");
            Console.Write("Please enter the ID of the patient whose details you are checking. Or press 'n' to return to menu: \n");
            string input = Console.ReadLine();
            if (input.ToLower() == "n")
            {
                return; // Return to the main menu
            }
            var users = User.LoadUsers();
            bool patientFound = false;
            foreach (var user in users)
            {
                if (user != null && user.Role == "Patient" && user.Id == input)
                {
                    Console.WriteLine("Details for {0}\n", user.Name);
                    Console.WriteLine("Name\t\t| Email Address\t\t\t| Phone\t\t| Address");
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine("{0,-15} | {1,-29} | {2,-13} | {3,-30}", user.Name, user.Email, user.Phone, user.Address);
                    patientFound = true;
                    break;
                }
            }
            if (!patientFound)
            {
                Console.WriteLine("No doctor found with ID {0}.", input); // Show error if no doctor found
            }
            Console.ReadKey();
            AdminMenu();
        }
        private static void AddDoctor() {

            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("| ---------------------------------------|");
            Console.WriteLine("|              Add Doctor                |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("Registering a new doctor with the DOTNET Hospital Management System");

            Console.Write("First Name: ");
            string name = Console.ReadLine();

            Console.Write("Last Name: ");
            name +=" " +Console.ReadLine();
           

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Street Number: ");
            string address = Console.ReadLine();

            Console.Write("Street: ");
            address += " " + Console.ReadLine();

            Console.Write("City: ");
            address +="," +Console.ReadLine();

            Console.Write("State: ");
            address += "," + Console.ReadLine();
            Console.WriteLine($"{name} added to the system!");
            // Save the new doctor to a file
            //id should be generated automatically 
          
            User.AddUser("doctor",name,phone,email,address, "Doctor");
            Console.ReadKey();
            AdminMenu();
            }
        private static void AddPatient() { /* 实现添加病人的逻辑 */

            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("| ---------------------------------------|");
            Console.WriteLine("|              Add Patient                |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");

            Console.Write("First Name: ");
            string name = Console.ReadLine();

            Console.Write("Last Name: ");
            name += " " + Console.ReadLine();


            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Street Number: ");
            string address = Console.ReadLine();

            Console.Write("Street: ");
            address += " " + Console.ReadLine();

            Console.Write("City: ");
            address += "," + Console.ReadLine();

            Console.Write("State: ");
            address += "," + Console.ReadLine();
            Console.WriteLine($"{name} added to the system!");
            // Save the new doctor to a file
            //id should be generated automatically 

            User.AddUser("patient", name, phone, email, address, "Patient");
            Console.ReadKey();
            AdminMenu();
        }
        private static void ExitProgram()
        {
            Console.WriteLine("Exiting the system...");
            Environment.Exit(0);
        }
    }
}
