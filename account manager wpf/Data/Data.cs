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
    public class Data
    {
        public string defaultPlayer { get; set; }
        public string defaultServer { get; set; }
        public Dictionary<string, Dictionary<string, List<Account>>> accounts = new Dictionary<string, Dictionary<string, List<Account>>>();
        public  List<string> puuIds = new List<string>();
    }
}
