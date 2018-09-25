using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SnookerStat.Model
{
    public class Players
    {

        public string PlayerName { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string NickName { get; set; }
        
        HttpClient client = new HttpClient();

        public async Task<ObservableCollection<Players>> ListPreviousPlayedNicks()
        {
            ObservableCollection<Players> PlayerList = new ObservableCollection<Players>();
            try
            {
                var response = await client.GetAsync("https://snookerapiproject.azurewebsites.net/api/friends/previousPlayed");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var playerObject = JsonConvert.DeserializeObject<ObservableCollection<Players>>(content);
                    foreach (Players playerName in playerObject)
                    {
                        PlayerList.Add(new Players() { NickName = playerName.NickName });
                    }
                } 
            }
            catch (Exception e)
            {
                Debug.WriteLine("Something went wrong " + e.StackTrace);
            }
            return PlayerList;
        }

        public async Task FindPlayerByNick(string nickName)
        {
            try
            {
                string url = "https://snookerapiproject.azurewebsites.net/api/users/searchFriendAndAddAsFriend";
                var obj = new Players { NickName = nickName };
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
                Debug.WriteLine("Something went wrong " + e.StackTrace);
            }
        }
    }
}
