﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace account_manager_wpf
{
    public class Account : IAPIAccount, IAPIRank
    {
        public bool failed = false;

        public Account(string player, string username, string password, string server, string name)
        {
            this.player = player;
            this.username = username;
            this.password = password;
            this.server = server;
            this.name = name;

            try
            {
                addAPIAccount(RiotAPI.GetAccountFromName<API.APIAccount>(server, name));
                addAPIRank(RiotAPI.GetRankFromId<API.APIRank>(server, this.id));
            }
            catch (Exception) { failed = true; }
        }

        public void update()
        {
            try
            {
                addAPIAccount(RiotAPI.GetAccountFromPuuId<API.APIAccount>(this.server, this.puuId));
                addAPIRank(RiotAPI.GetRankFromId<API.APIRank>(this.server, this.id));
            } catch (Exception) { findNewServer(); }
        }

        private void findNewServer()
        {
            List<string> servers = new List<string> { "EUW1", "NA1", "EUN1", "TR1", "LA1", "OC", "RU", "KR", "LA2", "JP1", "BR" };

            foreach (string server in servers)
            {
                try
                {
                    addAPIAccount(RiotAPI.GetAccountFromPuuId<API.APIAccount>(server, this.puuId));
                }
                catch (Exception) { }

                try
                {
                    addAPIRank(RiotAPI.GetRankFromId<API.APIRank>(this.server, this.id));
                } catch (Exception) { }
            }
        }

        #region credentials
        public string player { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string server { get; set; }
        #endregion

        #region apia
        public int profileIconId { get; set; }
        public string name { get; set; }
        public string puuId { get; set; }
        public long summonerLevel { get; set; }
        public string accountId { get; set; }
        public string id { get; set; }
        public long revisionDate { get; set; }
        #endregion

        #region apir
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
        #endregion

        public void addAPIAccount(API.APIAccount apia)
        {
            profileIconId = apia.profileIconId;
            name = apia.name;
            puuId = apia.puuId;
            summonerLevel = apia.summonerLevel;
            accountId = apia.accountId;
            id = apia.id;
            revisionDate = apia.revisionDate;
        }

        public void addAPIRank(API.APIRank apir)
        {
            queueType = apir.queueType;
            name = apir.summonerName;
            hotStreak = apir.hotStreak;
            wins = apir.wins;
            veteran = apir.veteran;
            losses = apir.losses;
            rank = apir.rank;
            tier = apir.tier;
            inactive = apir.inactive;
            freshBlood = apir.freshBlood;
            leagueId = apir.leagueId;
            summonerId = apir.summonerId;
            leaguePoints = apir.leaguePoints;
        }
    }
}