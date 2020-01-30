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
    public static class AccountListHandler
    {
        public static Dictionary<string, Dictionary<string, List<Account>>> accounts = new Dictionary<string, Dictionary<string, List<Account>>>();
        public static List<string> puuIds = new List<string>();

        public static void getAllAccounts(string path = @"Accounts.json")
        {
            try
            {
                string json = File.ReadAllText(path);
                accounts = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, List<Account>>>>(json);
            }
            catch (Exception)
            {
                accounts = new Dictionary<string, Dictionary<string, List<Account>>>();
                accounts.Add("default", new Dictionary<string, List<Account>>());
            }
        }

        public static void serializeAllAccounts(string path = @"Accounts.json")
        {
            //FileAttributes attr = File.GetAttributes(@"Accounts.json");
            //attr = attr & ~FileAttributes.ReadOnly;
            //File.SetAttributes(path, attr);

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, accounts);
            }

            //attr = attr | FileAttributes.ReadOnly;
            //File.SetAttributes(path, attr);
        }

        public static void updateAllAccounts()
        {
            foreach (KeyValuePair<string, Dictionary<string, List<Account>>> player in AccountListHandler.accounts)
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

                AccountListHandler.accounts[player][server].Remove(account);
                if (AccountListHandler.accounts[player][server].Count == 0)
                {
                    AccountListHandler.accounts[player].Remove(server);
                }
                if (AccountListHandler.accounts[player].Count == 0)
                {
                    AccountListHandler.accounts.Remove(player);
                }

                AccountListHandler.serializeAllAccounts();

                return account.name;
            }
            return null;
        }

        public static void addAccount(string player, string username, string password, string server, string name)
        {
            Account account = new Account(player, username, password, server, name);

            if (account.failed)
            {
                MessageBox.Show("Something went wrong");
                return;
            }
            if (AccountListHandler.puuIds.Contains(account.puuId))
            {
                MessageBox.Show("Account already added");
                return;
            }
            AccountListHandler.puuIds.Add(account.puuId);


            if (!AccountListHandler.accounts.ContainsKey(player)) // If player not present
            {
                AccountListHandler.accounts[player] = new Dictionary<string, List<Account>>(); // Add <player, server dictionary> to dictionary
            }

            if (!AccountListHandler.accounts[player].ContainsKey(server)) // If server not present in player
            {
                AccountListHandler.accounts[player][server] = new List<Account>(); // Add empty server list<account> to player
            }

            AccountListHandler.accounts[player][server].Add(account); // Add account to server


            AccountListHandler.serializeAllAccounts();
        }
    }
}
