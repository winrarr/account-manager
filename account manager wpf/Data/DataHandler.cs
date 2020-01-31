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

        public static string deleteAccount(Account account)
        {
            if (account != null)
            {
                string player = account.player;
                string server = account.server;

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

                return account.name;
            }
            return null;
        }

        public static int addAccount(string player, string username, string password, string server, string name)
        {
            Account account = new Account(player, username, password, server, name);

            if (account.failed)
            {
                return 2;
            }
            if (data.puuIds.Contains(account.puuId))
            {
                MessageBox.Show("Account already added");
                return 1;
            }
            data.puuIds.Add(account.puuId);


            foreach (string p in data.accounts.Keys)
            {
                if (p.ToUpper().Equals(player.ToUpper()))
                {

                }
            }
            if (!data.accounts.ContainsKey(player)) // If player not present
            {
                data.accounts[player] = new Dictionary<string, List<Account>>(); // Add <player, server dictionary> to dictionary
            }

            if (!data.accounts[player].ContainsKey(server)) // If server not present in player
            {
                data.accounts[player][server] = new List<Account>(); // Add empty server list<account> to player
            }

            data.accounts[player][server].Add(account); // Add account to server

            serialize();

            return 0;
        }
    }
}
