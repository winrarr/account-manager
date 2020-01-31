using System;
using System.Windows;
using System.Windows.Controls;

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
            DataHandler.deserialize();
            DataHandler.updateAllAccounts();
            cmbPlayer.ItemsSource = DataHandler.data.accounts.Keys;
            cmbPlayer.SelectedIndex = cmbPlayer.Items.IndexOf(DataHandler.data.defaultPlayer);
            cmbServer.SelectedIndex = cmbServer.Items.IndexOf(DataHandler.data.defaultServer);
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
            cmbServer.ItemsSource = DataHandler.data.accounts[Convert.ToString(cmbPlayer.SelectedValue)].Keys;
        }

        private void cmbServer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateListbox();
        }

        private void updateListbox()
        {
            currentlySelectedAccount = null;
            try
            {
                foreach (Account account in DataHandler.data.accounts[Convert.ToString(cmbPlayer.SelectedValue)][Convert.ToString(cmbServer.SelectedValue)])
                {
                    ListBoxItem lbiAccount = new ListBoxItem();
                    lbiAccount.Content = account.name;
                    lstAccounts.Items.Add(lbiAccount);

                    lbiAccount.Selected += new System.Windows.RoutedEventHandler((item, args) =>
                    {
                        currentlySelectedAccount = account;
                        txtUsername.Text = account.username;
                        txtPasswordbox.Password = account.password;
                        lblRank.Content = account.tier + " " + account.rank + " (" + account.leaguePoints + " LP)";
                        lblWinrate.Content = Math.Round((float)account.wins / ((float)account.wins + (float)account.losses) * 100, 2) + " %";
                    });
                }
            }
            catch (Exception) { }
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            DataHandler.deleteAccount(currentlySelectedAccount);
        }

        public void updateControls()
        {
            cmbPlayer.ItemsSource = DataHandler.data.accounts.Keys;
            updateListbox();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DataHandler.data.defaultPlayer = cmbPlayer.Text;
            DataHandler.data.defaultServer = cmbServer.Text;
            DataHandler.serialize();
        }

        private void btnUpdateAllAccounts_Click(object sender, RoutedEventArgs e)
        {
            DataHandler.updateAllAccounts();
        }

        private void chkShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (chkShowPassword.IsChecked == true)
            {
                txtPassword.Text = txtPasswordbox.Password;
                txtPassword.Visibility = Visibility.Visible;
                txtPasswordbox.Visibility = Visibility.Hidden;
            }
            else if (chkShowPassword.IsChecked == false)
            {
                txtPasswordbox.Password = txtPassword.Text;
                txtPasswordbox.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Hidden;
            }
        }
    }
}
