using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
