using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SnookerStat.Model
{
    public class RegisterPlayer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CorrectPassword { get; set; }

        HttpClient client = new HttpClient();
        public async Task RegisterNewPlayer(RegisterPlayer player)
        {
            try
            {
                string url = "https://snookerapiproject.azurewebsites.net/api/users/RegisterPlayer";
                var obj = new RegisterPlayer
                {
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    NickName = player.NickName,
                    Email = player.Email,
                    Password = player.Password
                };
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var request = await client.PostAsync(url, content);
                if (request.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Success");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Something went wrong", e.StackTrace);
            }
        }
    }
}
