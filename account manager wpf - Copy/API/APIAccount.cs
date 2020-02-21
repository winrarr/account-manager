using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace account_manager_wpf.API
{
    public class APIAccount : IAPIAccount
    {
        public int profileIconId { get; set; }
        public string name { get; set; }
        public string puuId { get; set; }
        public long summonerLevel { get; set; }
        public string accountId { get; set; }
        public string id { get; set; }
        public long revisionDate { get; set; }
    }
}
