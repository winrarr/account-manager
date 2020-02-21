using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace account_manager_wpf
{
    public static class DataHandler
    {
        public static Data data;

        /// <summary>
        /// Loads all the data from Accounts.json file
        /// </summary>
        /// <param name="path">Potential Explicit path for file to read from (defaults to same directory as exe file)</param>
        public static void deserialize(string path = @"Accounts.json")
        {
            try
            {
                string json = File.ReadAllText(path);
                data = JsonConvert.DeserializeObject<Data>(json);
            }
            catch (Exception)
            {
                data = new Data();
            }
        }

        /// <summary>
        /// Saves the data to Accounts.json file
        /// </summary>
        /// <param name="path">Potential explicit path for file to read to (defaults to same directory as exe file)</param>
        public static void serialize(string path = @"Accounts.json")
        {
            //FileAttributes attr = File.GetAttributes(@"Accounts.json");
            //attr = attr & ~FileAttributes.ReadOnly;
            //File.SetAttributes(path, attr);

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, data);
            }

            //attr = attr | FileAttributes.ReadOnly;
            //File.SetAttributes(path, attr);
        }

        /// <summary>
        /// Updates all the accounts in the data using the Riot Game API
        /// </summary>
        public static void updateAllAccounts()
        {
            foreach (KeyValuePair<string, Dictionary<string, List<Account>>> player in data.accounts)
            {
                foreach (KeyValuePair<string, List<Account>> server in player.Value)
                {
                    foreach (Account account in server.Value)
                    {
                        account.update();
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the given account from the data
        /// </summary>
        /// <param name="account">Account to delete from the data</param>
        public static void deleteAccount(Account account)
        {
            if (account != null)
            {
                string player = account.player;
                string server = account.server;

                // Delete parents (server + player) if empty after account delete
                data.accounts[player][server].Remove(account);
                if (data.accounts[player][server].Count == 0)
                {
                    data.accounts[player].Remove(server);
                }
                if (data.accounts[player].Count == 0)
                {
                    data.accounts.Remove(player);
                }

                serialize();
            }
        }

        /// <summary>
        /// Adds an account with the given parameters to the data
        /// </summary>
        /// <param name="player">Player who owns the account</param>
        /// <param name="username">Account username</param>
        /// <param name="password">Account password</param>
        /// <param name="server">Server of the account</param>
        /// <param name="name">Summoner name of the account</param>
        /// <returns>Returns an integer indicating the result of the operation (0 = Success, 1 = Account already added, 2 = Account could not be retrieved)</returns>
        public static int addAccount(string player, string username, string password, string server, string name)
        {
            Account account = new Account(player, username, password, server, name);

            if (account.failed)
            {
                return 2;
            }
            if (data.puuIds.Contains(account.puuId))
            {
                return 1;
            }
            data.puuIds.Add(account.puuId);

            
            string alreadyAddedPlayer = findMatchingString(player, new List<string>(data.accounts.Keys)); // Find already added player
            if (alreadyAddedPlayer == null) // If not found
            {
                data.accounts[player] = new Dictionary<string, List<Account>>(); // Create the player
                alreadyAddedPlayer = player;
            }
            account.player = alreadyAddedPlayer;

            string alreadyAddedServer = findMatchingString(server, new List<string>(data.accounts[alreadyAddedPlayer].Keys)); // Find already added server
            if (alreadyAddedServer == null) // If not found
            {
                data.accounts[alreadyAddedPlayer][server] = new List<Account>(); // Create the server
                alreadyAddedServer = server;
            }
            data.accounts[alreadyAddedPlayer][alreadyAddedServer].Add(account); // Add account to server
            account.server = alreadyAddedServer;

            serialize();
            return 0;
        }

        /// <summary>
        /// Finds a string from the given list which matches the given string when not case sensitive
        /// </summary>
        /// <param name="findString">String to be found in list</param>
        /// <param name="strings">List of strings to compare to</param>
        /// <returns>Returns the string from the given list that matches the given string</returns>
        private static string findMatchingString(string findString, List<string> strings)
        {
            foreach (string s in strings)
            {
                if (s.ToUpper().Equals(findString.ToUpper()))
                {
                    return s;
                }
            }
            return null;
        }
    }
}
