﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace account_manager_wpf.API
{
    public class APIRank
    {
        public string queueType { get; set; }
        public string summonerName { get; set; }
        public bool hotStreak { get; set; }
        public int wins { get; set; }
        public bool veteran { get; set; }
        public int losses { get; set; }
        public string rank { get; set; }
        public string tier { get; set; }
        public bool inactive { get; set; }
        public bool freshBlood { get; set; }
        public string leagueId { get; set; }
        public string summonerId { get; set; }
        public int leaguePoints { get; set; }
    }
}
