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
        private static string apiKey = "?api_key=" + "RGAPI-6cb7df72-3d8f-4285-917e-3c14507306f2";

        /// <summary>
        /// Retrieves APIAccount object from given server and name using the Riot Games API
        /// </summary>
        /// <typeparam name="APIAccount"></typeparam>
        /// <param name="server">Server of the account</param>
        /// <param name="name">Summoner name of the account</param>
        /// <returns>Returns an APIAccount object retrieved using the Riot Games API</returns>
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

        /// <summary>
        /// Retrieves APIAccount object from given server and puuid using the Riot Games API
        /// </summary>
        /// <typeparam name="APIAccount"></typeparam>
        /// <param name="server">Server of the account</param>
        /// <param name="puuId">Puuid of the account</param>
        /// <returns>Returns an APIAccount object retrieved using the Riot Games API</returns>
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

        /// <summary>
        /// Retrieves APIRank object from given server and summoner id using the Riot Games API
        /// </summary>
        /// <typeparam name="APIRank"></typeparam>
        /// <param name="server">Server of the account</param>
        /// <param name="id">Summoner id of the account</param>
        /// <returns>Returns an APIRank object retrieved using the Riot Games API</returns>
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
