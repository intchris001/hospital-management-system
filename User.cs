using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace HMS
{
    class User
    {
        private static string filePath = "users.json";
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }

        public string  Relation { get; set; }
        //public void SetRelation()
        //{
        //    if (this.Role == "Doctor")
        //    {
        //        this.Relation = new List<string>();
        //    }
        //    else if (this.Role == "Patient")
        //    {
        //        this.Relation = "";
        //    }
        //}


        public static bool ValidateUser(string id, string password)
        {
            var users = LoadUsers();
            foreach (var user in users)
            {
                if (user.Id == id && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetUserType(string id)
        {
            var users = LoadUsers();
            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    return user.Role;
                }
            }
            return null;
        }

        public static List<User> LoadUsers()
        {
            if (!File.Exists(filePath))
            {
                return new List<User> { new User { Id = "00001", Password = "admin", Role = "Admin" } };
            }
            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonData);
        }

        public static void SaveUsers(List<User> users)
        {
            var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        public static void AddUser(string password, string name, string phone, string email, string address, string role)
        {
            List<User> users = LoadUsers();
            User newUser = new User
            {
                Id = GenerateUniqueId(), 
                Password = password,
                Name = name,
                Phone = phone,
                Email = email,
                Address = address,
                Role = role,
                Relation=""
            };
            users.Add(newUser);
            SaveUsers(users);
        }
        public static string GenerateUniqueId()
        {
            HashSet<string> existingIds = new HashSet<string>();
            var users = LoadUsers();

            foreach (var user in users)
            {
                existingIds.Add(user.Id);
            }

            Random random = new Random();
            string uniqueId;
            do
            {
                uniqueId = random.Next(10000, 99999).ToString();
            } while (existingIds.Contains(uniqueId));

            return uniqueId;
        }
        public static User GetUser(string id)
        {
            var users = LoadUsers(); // 假设您有一个方法来加载所有用户
            return users.FirstOrDefault(u => u.Id == id );
        }


    }
}
