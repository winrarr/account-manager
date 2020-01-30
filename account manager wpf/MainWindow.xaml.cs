﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace account_manager_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Account currentlySelectedAccount = null;

        public MainWindow()
        {
            InitializeComponent();
            setUp();
        }

        private void setUp()
        {
            AccountListHandler.getAllAccounts();
            AccountListHandler.updateAllAccounts();
            cmbPlayer.ItemsSource = AccountListHandler.accounts.Keys;
        }



        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            AddAccountWindow addAccountWindow = new AddAccountWindow(this);
            addAccountWindow.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void cmbPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentlySelectedAccount = null;
            cmbServer.ItemsSource = AccountListHandler.accounts[Convert.ToString(cmbPlayer.SelectedValue)].Keys;
        }

        private void cmbServer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentlySelectedAccount = null;
            try
            {
                foreach (Account account in AccountListHandler.accounts[Convert.ToString(cmbPlayer.SelectedValue)][Convert.ToString(cmbServer.SelectedValue)])
                {
                    ListBoxItem lbiAccount = new ListBoxItem();
                    lbiAccount.Content = account.name;
                    lstAccounts.Items.Add(lbiAccount);

                    lbiAccount.Selected += new System.Windows.RoutedEventHandler((item, args) =>
                    {
                        currentlySelectedAccount = account;
                        txtUsername.Text = account.username;
                        txtPassword.Text = account.password;
                        lblRank.Content = account.tier + " " + account.rank + " (" + account.leaguePoints + " LP)";
                        lblWinrate.Content = Math.Round((float)account.wins / ((float)account.wins + (float)account.losses) * 100, 2) + " %";
                    });
                }
            }
            catch (Exception) { }
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void updateListbox()
        {
            cmbServer_SelectionChanged(null, null);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            AccountListHandler.accounts["default"].Add(cmbPlayer.Text, new List<Account>());
            AccountListHandler.accounts["default"].Add(cmbServer.Text, new List<Account>());
        }

        private void btwUpdateAccounts_Click(object sender, RoutedEventArgs e)
        {
            AccountListHandler.updateAllAccounts();
        }
    }
}
