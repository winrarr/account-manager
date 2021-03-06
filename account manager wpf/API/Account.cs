﻿using account_manager_wpf.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace account_manager_wpf
{
    public class Account
    {
        public APIAccount apia;
        public APIRank apir;

        public bool failed = false;

        /// <summary>
        /// Creates a new account from the given account information using the Riot Games API
        /// </summary>
        /// <param name="player">Player who owns the account</param>
        /// <param name="username">Account username</param>
        /// <param name="password">Account password</param>
        /// <param name="server">Server of the account</param>
        /// <param name="name">Summoner name of the account</param>
        public Account(string player, string username, string password, string server, string name)
        {
            this.player = player;
            this.username = username;
            this.password = password;
            this.server = server;

            try // Load account information from Riot Games API
            {
                setAPIAccount(RiotAPI.GetAccountFromName<APIAccount>(server, name));
                setAPIRank(RiotAPI.GetRankFromId<APIRank>(server, apia.id));
            }
            catch (Exception) { failed = true; }
        }

        /// <summary>
        /// Updates the account using the Riot Games API
        /// </summary>
        public void update()
        {
            try
            {
                setAPIAccount(RiotAPI.GetAccountFromPuuId<APIAccount>(this.server, apia.puuId));
                setAPIRank(RiotAPI.GetRankFromId<APIRank>(this.server, apia.id));
            } catch (Exception) { findNewServer(); }
        }

        /// <summary>
        /// Searches for a new server where the account is present
        /// </summary>
        private void findNewServer()
        {
            List<string> servers = new List<string> { "EUW1", "NA1", "EUN1", "TR1", "LA1", "OC", "RU", "KR", "LA2", "JP1", "BR" };

            foreach (string server in servers)
            {
                try
                {
                    setAPIAccount(RiotAPI.GetAccountFromPuuId<APIAccount>(server, apia.puuId));
                }
                catch (Exception) { }

                try
                {
                    setAPIRank(RiotAPI.GetRankFromId<APIRank>(this.server, apia.id));
                } catch (Exception) { }
            }
        }

        #region credentials
        public string player { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string server { get; set; }
        #endregion

        /// <summary>
        /// Updates the APIAccount properties from another APIAccount object
        /// </summary>
        /// <param name="apia">APIAccount object to update the account to</param>
        public void setAPIAccount(APIAccount apia)
        {
            this.apia = apia;
        }

        /// <summary>
        /// Updates the APIRank properties from another APIRank object
        /// </summary>
        /// <param name="apir">APIRankt object to update the account to</param>
        public void setAPIRank(APIRank apir)
        {
            this.apir = apir;
        }
    }
}
