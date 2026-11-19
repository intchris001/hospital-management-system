using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;


namespace HMS
{
    class Doctor
    {
        public static void DoctorMenu(User current_user)
        {
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("| ---------------------------------------|");
            Console.WriteLine("|              Doctor Menu               |");
            Console.WriteLine("|----------------------------------------|");
           
            Console.WriteLine($"Welcome to DOTNET Hospital Management System {current_user.Name}\n");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List doctor details");
            Console.WriteLine("2. List patients");
            Console.WriteLine("3. List appointments");
            Console.WriteLine("4. Check particular patient");
            Console.WriteLine("5. List appointments with patient");
            Console.WriteLine("6. Logout");
            Console.WriteLine("7. Exit\n");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ListDoctorDetails(current_user);
                    break;
                case "2":
                    ListPatients(current_user);
                    break;
                case "3":
                    ListAppointments(current_user);
                    break;
                case "4":
                    CheckParticularPatient(current_user);
                    break;
                case "5":
                    ListAppointmentsWithPatient(current_user);
                    break;
                case "6":
                    Program.Login(); // Assuming Login method is static and accessible
                    break;
                case "7":
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.\n");
                    DoctorMenu(current_user);
                    break;
            }
        }

        private static void ListDoctorDetails(User current_user) {
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("| ---------------------------------------|");
            Console.WriteLine("|              My Details                |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine();
          
            Console.WriteLine("Name\t\t| Email Address\t\t\t| Phone\t\t| Address");
            Console.WriteLine(new string('-', 80));

            Console.WriteLine("{0,-15} | {1,-29} | {2,-13} | {3,-30}", current_user.Name , current_user.Email , current_user.Phone, current_user.Address);
            Console.ReadKey();
            DoctorMenu(current_user);

        }
        private static void ListPatients(User current_user) {
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|              My Patients               |");
            Console.WriteLine("|----------------------------------------|\n");
            Console.WriteLine($"Patients assigned to {current_user.Name}:\n");
            Console.WriteLine("Patient\t\t| Doctor\t\t| Email Address\t\t\t| Phone\t\t| Address");
            Console.WriteLine(new string('-', 80));

            foreach (var patientId in current_user.Relation.Split('|'))
            {
                User patient = User.GetUser(patientId);
                if (patient != null)
                {
                    Console.WriteLine("{0,-15} | {1,-21} | {2,-29} | {3,-14}|{4,-20}", patient.Name, current_user.Name, patient.Email, patient.Phone, patient.Address); 
                }
            }
            Console.ReadKey();
            DoctorMenu(current_user);
        }
        private static void ListAppointments(User current_user) {
            if (current_user.Relation == "")
            {
                Console.WriteLine("No appointments.");
                return;
            }
            string json = File.ReadAllText("appointments.json");
            List<Appointment> appointments = JsonConvert.DeserializeObject<List<Appointment>>(json);
            var userAppointments = appointments.Where(a => a.DoctorId == current_user.Id).ToList();
            if (userAppointments.Count == 0)
            {
                Console.WriteLine("No appointments found for this user.");
                return;
            }
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|              All Appointments          |");
            Console.WriteLine("|----------------------------------------|\n");
            Console.WriteLine("Doctor\t\t| Patient\t\t| Description");
            Console.WriteLine(new string('-', 80));
            foreach (var appointment in userAppointments)
            {
                string doctorName = User.GetUser(appointment.DoctorId).Name;
                string patientName = User.GetUser(appointment.PatientId).Name;

                Console.WriteLine("{0,-15} | {1,-21} | {2,-20}", doctorName, patientName, appointment.Description);
            }


            Console.ReadKey();
            DoctorMenu(current_user);
        }
        private static void CheckParticularPatient(User current_user) {
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|          Check Patient Details         |");
            Console.WriteLine("|----------------------------------------|\n");
            Console.Write("Enter the ID of the patient to check: ");
            string searchPatientId = Console.ReadLine();
            Console.WriteLine();
            Boolean patientExist = false;
            foreach (var patientId in current_user.Relation.Split('|'))
            {   
                if (patientId == searchPatientId)
                {
                    patientExist = true;
                    break;
                }      
            }
            if (patientExist)
            {
                User patient = User.GetUser(searchPatientId);
                Console.WriteLine("Patient\t\t| Doctor\t\t| Email Address\t\t\t| Phone\t\t| Address");
                Console.WriteLine("{0,-15} | {1,-21} | {2,-29} | {3,-14}|{4,-20}", patient.Name, current_user.Name, patient.Email, patient.Phone, patient.Address);
            }
            else
            {
                Console.WriteLine("No patient found with the provided ID.");
            }
            Console.ReadKey();
            DoctorMenu(current_user);
        }
        private static void ListAppointmentsWithPatient(User current_user) {
            Console.Clear();
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|    DOTNET Hospital Management System   |");
            Console.WriteLine("|----------------------------------------|");
            Console.WriteLine("|          Appointments with             |");
            Console.WriteLine("|----------------------------------------|\n");
            Console.Write("Enter the ID of the patient you would like to view appointments for: ");
            string searchPatientId = Console.ReadLine();
            Console.WriteLine();
            Boolean patientExist = false;
            foreach (var patientId in current_user.Relation.Split('|'))
            {
                if (patientId == searchPatientId)
                {
                    patientExist = true;
                    break;
                }
            }
            if (patientExist)
            {
                string json = File.ReadAllText("appointments.json");
                List<Appointment> appointments = JsonConvert.DeserializeObject<List<Appointment>>(json);
                var userAppointments = appointments.Where(a => a.DoctorId == current_user.Id && a.PatientId == searchPatientId).ToList();
                if (userAppointments.Count == 0)
                {
                    Console.WriteLine("No appointments found for this patient.");
                    Console.ReadKey();
                    DoctorMenu(current_user);
                    
                }
                Console.WriteLine("Doctor\t\t| Patient\t\t| Description");
                Console.WriteLine(new string('-', 80));
                foreach (var appointment in userAppointments)
                {
                    string doctorName = User.GetUser(appointment.DoctorId).Name;
                    string patientName = User.GetUser(appointment.PatientId).Name;
                    Console.WriteLine("{0,-15} | {1,-21} | {2,-20}", doctorName, patientName, appointment.Description);

                }
                Console.ReadKey();
                DoctorMenu(current_user);
            }
            else
            {
                Console.WriteLine("No patient found with the provided ID.");
                Console.ReadKey();
                DoctorMenu(current_user);
            }

        }
        private static void ExitProgram()
        {
            Console.WriteLine("Exiting the system...");
            Environment.Exit(0);
        }
    }
}
