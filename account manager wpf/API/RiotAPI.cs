using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace account_manager_wpf
{
    public class RiotAPI
    {
        private static string apiKey = "?api_key=" + "RGAPI-84959e05-8e35-4427-8c61-29ded10b059a";

        public static APIAccount GetAccountFromName<APIAccount>(string server, string name) where APIAccount : new()
        {
            using (var w = new WebClient())
            {
                var account = string.Empty;
                try
                {
                    string url = "https://" + server + ".api.riotgames.com/lol/summoner/v4/summoners/by-name/" + name + apiKey;
                    account = w.DownloadString(url);
                }
                catch (Exception) { }
                return !string.IsNullOrEmpty(account) ? JsonConvert.DeserializeObject<APIAccount>(account) : new APIAccount();
            }
        }

        public static APIAccount GetAccountFromPuuId<APIAccount>(string server, string puuId) where APIAccount : new()
        {
            using (var w = new WebClient())
            {
                var account = string.Empty;
                try
                {
                    string url = "https://" + server + ".api.riotgames.com/lol/summoner/v4/summoners/by-puuid/" + puuId + apiKey;
                    account = w.DownloadString(url);
                }
                catch (Exception) { }
                return !string.IsNullOrEmpty(account) ? JsonConvert.DeserializeObject<APIAccount>(account) : new APIAccount();
            }
        }

        public static APIRank GetRankFromId<APIRank>(string server, string id) where APIRank : new()
        {
            using (var w = new WebClient())
            {
                var rank = string.Empty;
                try
                {
                    string url = "https://" + server + ".api.riotgames.com/lol/league/v4/entries/by-summoner/" + id + apiKey;
                    rank = w.DownloadString(url);
                }
                catch (Exception) { }
                APIRank final = JsonConvert.DeserializeObject<List<APIRank>>(rank)[0];
                return !string.IsNullOrEmpty(rank) ? final : new APIRank();
            }
        }
    }
}
