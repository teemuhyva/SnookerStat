using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SnookerStat.Model
{
    public class LoginPlayer
    {
        public string NickName { get; set; }
        public string Password { get; set; }
        public string GivenPasswordHash { get; set; }
        public string GivenPassword { get; set; }
        public string CorrectPassword { get; set; }
        public string GivenPasswordSalt { get; set; }

        HttpClient client = new HttpClient();
        public async Task LoginAsExistingPlayer(LoginPlayer player)
        {
            try
            {
                string url = "https://snookerapiproject.azurewebsites.net/api/users/loginPlayer";
                var obj = new LoginPlayer
                {
                    NickName = player.NickName
                };
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var request = await client.PostAsync(url, content);
                if (request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    var playerObject = JsonConvert.DeserializeObject<LoginPlayer>(response);
                    bool isCorrectPassword = PasswordHash.VerifyPassword(player.GivenPassword, player.GivenPasswordSalt, player.GivenPasswordHash);

                    if (!isCorrectPassword)
                    {
                        CorrectPassword = "Password was not correct";
                    }
                    else
                    {
                        CorrectPassword = "Password was correct";
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Something went wrong", e.StackTrace);
            }
        }
    }
}
