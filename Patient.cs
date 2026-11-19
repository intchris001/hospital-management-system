using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace HMS
{
    class Patient
    {
        public static void PatientMenu(User current_user)
        {
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("| ---------------------------------------|");
            Console.WriteLine("|              Patient Menu              |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine($"Welcome to DOTNET Hospital Management System {current_user.Name}\n");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List patient details");
            Console.WriteLine("2. List my doctor details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Book appointment");
            Console.WriteLine("5. Exit to login");
            Console.WriteLine("6. Exit System\n");


            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ViewDetails( current_user);
                    break;
                case "2":
                    ViewDoctorDetails(current_user);
                    break;
                case "3":
                    ListAppointments(current_user);
                    break;
                case "4":
                    BookAppointment(current_user);
                    break;
                case "5":
                    Program.Login(); // Assuming Login method is static and accessible
                    break;
                case "6":
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.\n");
                    PatientMenu(current_user);
                    break;
            }
        }

        private static void ViewDetails(User current_user) {
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|              My Details                |");
            Console.WriteLine("|----------------------------------------|\n");
            Console.WriteLine($"{current_user.Name}'s Details\n");
            Console.WriteLine($"Patient ID: {current_user.Id}");
            Console.WriteLine($"Full Name: {current_user.Name}");
            Console.WriteLine($"Address: {current_user.Address}");
            Console.WriteLine($"Email: {current_user.Email}");
            Console.WriteLine($"Phone: {current_user.Phone}\n");
            Console.ReadKey();
            PatientMenu(current_user);
        }
        private static void ViewDoctorDetails(User current_user) {
            string doctorId = current_user.Relation.ToString();
            
            User my_doctor = User.GetUser(doctorId);

            // Check if a doctor was found
            if (my_doctor != null)
            {
                Console.Clear();
                Console.WriteLine("|----------------------------------------|");
                Console.WriteLine("|    DOTNET Hospital Management System   |");
                Console.WriteLine("|----------------------------------------|");
                Console.WriteLine("|              My Doctor                 |");
                Console.WriteLine("|----------------------------------------|\n");
                Console.WriteLine("Your doctor:\n");
                Console.WriteLine("Name\t\t| Email Address\t\t\t| Phone\t\t| Address");
                Console.WriteLine(new string('-', 80));
                Console.WriteLine("{0,-15} | {1,-29} | {2,-13} | {3,-30}", my_doctor.Name, my_doctor.Email, my_doctor.Phone, my_doctor.Address);
            
            }
            else
            {
                Console.WriteLine("No details found for your doctor.");
            }
            Console.ReadKey();
            PatientMenu(current_user);

        }
        private static void ListAppointments(User current_user) {
            if (current_user.Relation == "")
            {
                Console.WriteLine("No doctor responsible for appointments.");
                return;
            }
            string json = File.ReadAllText("appointments.json");
            List<Appointment> appointments = JsonConvert.DeserializeObject<List<Appointment>>(json);
            var userAppointments = appointments.Where(a => a.PatientId == current_user.Id).ToList();
            if (userAppointments.Count == 0)
            {
                Console.WriteLine("No appointments found for this user.");
                return;
            }
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|              My Appointments           |");
            Console.WriteLine("|----------------------------------------|\n");
            Console.WriteLine("Appointments for " + current_user.Name+"\n");
            Console.WriteLine("Doctor\t\t| Patient\t\t| Description");
            Console.WriteLine(new string('-', 80));
            foreach (var appointment in userAppointments)
            {
                string doctorName = User.GetUser(appointment.DoctorId).Name;
                string patientName = User.GetUser(appointment.PatientId).Name;
               
                Console.WriteLine("{0,-15} | {1,-21} | {2,-20}", doctorName, patientName, appointment.Description);
            }

            
            Console.ReadKey();
            PatientMenu(current_user);

        }
        private static void BookAppointment(User current_user) { /* 实现预约逻辑 */ }
        private static void ExitProgram()
        {
            Console.WriteLine("Exiting the system...");
            Environment.Exit(0);
        }
    }
}
