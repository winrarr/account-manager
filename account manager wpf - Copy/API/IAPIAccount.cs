using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace account_manager_wpf
{
    public interface IAPIAccount
    {
        int profileIconId { get; set; }
        string name { get; set; }
        string puuId { get; set; }
        long summonerLevel { get; set; }
        string accountId { get; set; }
        string id { get; set; }
        long revisionDate { get; set; }
    }
}
