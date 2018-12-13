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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.ComponentModel;
using System.Security.Cryptography;

namespace WindowChrome.Demo
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        List<ServiceReference1.UsersBD> User = new List<ServiceReference1.UsersBD>();

        private MainWindow _mainwindow;

        public LoginWindow()
        {
            InitializeComponent();

            client.GetUsersEFCompleted += Client_GetUsersEFCompleted;
            _mainwindow = new MainWindow();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.Close();
            this.Close();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.label_Copy2.Content = "GUEST";
            _mainwindow.label_Copy3.Content = "GUEST";
            _mainwindow.label_Copy4.Content = "GUEST";
            _mainwindow.Guest_icon_png.Visibility = Visibility.Visible;
            _mainwindow.Limitations();
            _mainwindow.Show();
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox3.Text != "" && passwordBox.Password != "")
            {
                button.IsEnabled = false;
                byte[] bytes = Encoding.Unicode.GetBytes(passwordBox.Password);
                MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
                byte[] byteHash = CSP.ComputeHash(bytes);
                string hash = string.Empty;
                foreach (byte b in byteHash)
                {
                    hash += string.Format("{0:x2}", b);
                }
                client.GetUsersEFAsync(textBox3.Text, hash);
            }
        }

        private void Client_GetUsersEFCompleted(object sender, ServiceReference1.GetUsersEFCompletedEventArgs e)
        {
            try
            {
                button.IsEnabled = true;
                User = e.Result.ToList();
                if (User.Count == 0)
                {

                }
                else
                {
                    _mainwindow.label_Copy2.Content = User[0].First_Name;
                    _mainwindow.label_Copy3.Content = User[0].Second_Name;
                    if (User[0].Administrator == true)
                    {
                        _mainwindow.label_Copy4.Content = "Administrator";
                        _mainwindow.Admin_icon_png.Visibility = Visibility.Visible;
                        _mainwindow.Limitations();
                    }
                    else
                    {
                        _mainwindow.label_Copy4.Content = "User";
                        _mainwindow.User_icon_png.Visibility = Visibility.Visible;
                        _mainwindow.Limitations();
                    }
                    _mainwindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                textBox3.Text = "admin";
                passwordBox.Password = "admin";
            }
        }
    }
}
