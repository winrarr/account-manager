using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace account_manager_wpf
{
    public interface IAPIRank
    {
        string queueType { get; set; }
        string summonerName { get; set; }
        bool hotStreak { get; set; }
        int wins { get; set; }
        bool veteran { get; set; }
        int losses { get; set; }
        string rank { get; set; }
        string tier { get; set; }
        bool inactive { get; set; }
        bool freshBlood { get; set; }
        string leagueId { get; set; }
        string summonerId { get; set; }
        int leaguePoints { get; set; }
    }
}
