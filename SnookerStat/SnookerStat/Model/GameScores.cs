using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SnookerStat.Model
{
    public class GameScores
    {

        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public int BestOf { get; set; }
        public int ScorePlayer1 { get; set; }
        public int ScorePlayer2 { get; set; }
        public int TopBreakPlayer1 { get; set; }
        public int TopBreakPlayer2 { get; set; }
        public string WinningPlayer { get; set; }
        public string GameNumber { get; set; }
        public DateTime GameDate { get; set; }

        HttpClient client = new HttpClient();

        public async Task SaveScore(int player1Score, int player2Score, int player1Break, int player2Break) {
            try
            {
                string url = "https://snookerapiproject.azurewebsites.net/api/gamescore/storeGameStats";
                var addGame = new GameScores {
                    ScorePlayer1 = player1Score,
                    ScorePlayer2 = player2Score,
                    TopBreakPlayer1 = player1Break,
                    TopBreakPlayer2 = player2Break,
                    GameDate = DateTime.Now
                };
                var json = JsonConvert.SerializeObject(addGame);
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
