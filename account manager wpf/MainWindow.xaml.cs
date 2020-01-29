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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace account_manager_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            setUp();
        }

        private void setUp()
        {
            AccountListHandler.getAllAccounts();
            foreach (KeyValuePair<string, Dictionary<string, List<Account>>> player in AccountListHandler.accounts)
            {
                TreeViewItem tviPlayer = new TreeViewItem();
                tviPlayer.Header = player.Key;
                tvwAccounts.Items.Add(tviPlayer);

                foreach (KeyValuePair<string, List<Account>> server in player.Value)
                {
                    TreeViewItem tviServer = new TreeViewItem();
                    tviServer.Header = server.Key;
                    tviPlayer.Items.Add(tviServer);

                    foreach (Account account in server.Value)
                    {
                        TreeViewItem tviAccount = new TreeViewItem();
                        tviAccount.Header = account.name;
                        tviServer.Items.Add(tviAccount);
                    }
                }
            }
        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            AddAccountWindow addAccountWindow = new AddAccountWindow(this);
            addAccountWindow.Show();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
