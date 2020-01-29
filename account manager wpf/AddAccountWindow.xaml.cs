using System;
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
using System.Windows.Shapes;

namespace account_manager_wpf
{
    /// <summary>
    /// Interaction logic for AddAccountWindow.xaml
    /// </summary>
    public partial class AddAccountWindow : Window
    {
        private MainWindow mainWindow;

        public AddAccountWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            setUp();
        }

        private void setUp()
        {
            cmbServer.ItemsSource = new string[] { "EUW1", "NA1", "EUN1", "TR1", "LA1", "OC", "RU", "KR", "LA2", "JP1", "BR" };
            cmbServer.SelectedItem = mainWindow.cmbServer.SelectedItem;
        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account(txtPlayer.Text, txtUsername.Text, txtPassword.Text, cmbServer.Text, txtName.Text);

            if (account.failed)
            {
                MessageBox.Show("Something went wrong");
                return;
            }
            if (AccountListHandler.puuIds.Contains(account.puuId))
            {
                MessageBox.Show("Account already added");
                return;
            }
            AccountListHandler.puuIds.Add(account.puuId);


            if (!AccountListHandler.accounts.ContainsKey(txtPlayer.Text)) // If player not present
            {
                AccountListHandler.accounts[txtPlayer.Text] = new Dictionary<string, List<Account>>(); // Add <player, server dictionary> to dictionary
            }

            if (!AccountListHandler.accounts[txtPlayer.Text].ContainsKey(cmbServer.Text)) // If server not present in player
            {
                AccountListHandler.accounts[txtPlayer.Text][cmbServer.Text] = new List<Account>(); // Add empty server list<account> to player
            }

            AccountListHandler.accounts[txtPlayer.Text][cmbServer.Text].Add(account); // Add account to server


            AccountListHandler.serializeAllAccounts();

            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Visibility = Visibility.Visible;
        }
    }
}
