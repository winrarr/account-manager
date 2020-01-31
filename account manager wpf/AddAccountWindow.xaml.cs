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
            switch (DataHandler.addAccount(txtPlayer.Text, txtUsername.Text, txtPassword.Text, cmbServer.Text, txtName.Text))
            {
                case 0:
                    break;
                case 1:
                    MessageBox.Show("Account already added");
                    break;
                case 2:
                    MessageBox.Show("Something went wrong");
                    break;
            }
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.updateControls();
            mainWindow.Visibility = Visibility.Visible;
        }
    }
}
