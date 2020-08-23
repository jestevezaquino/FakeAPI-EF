using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DevColaboration.Models.FakeAPI;
using Newtonsoft.Json;

namespace DevColaboration.Services
{
    public class UsersServices
    {

        private readonly UsersEFServices _usersEfSvc;

        public UsersServices(UsersEFServices usersEfSvc)
        {
            _usersEfSvc = usersEfSvc;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
                string json = await response.Content.ReadAsStringAsync();
                List<User> users = JsonConvert.DeserializeObject<List<User>>(json);

                return users;
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");
                string json = await response.Content.ReadAsStringAsync();
                User user = JsonConvert.DeserializeObject<User>(json);
                return user;
            }
        }

        public async Task<bool> PostUserAsync(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(user, Formatting.Indented);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://jsonplaceholder.typicode.com/users", content);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 201)
                {
                    bool result = _usersEfSvc.AddUserDB(user);
                    if(result)
                        return true;
                    return false;
                }
                return false;
            }
        }
    }
}
