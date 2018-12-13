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
using System.Security.Cryptography;

namespace WindowChrome.Demo
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public MainWindow _mainwindow;
        private string catcher;

        public string First_Name;
        public string Second_Name;
        public string Login;
        public string Password;
        public bool Administrator;

        public AddUserWindow()
        {
            InitializeComponent();

            _mainwindow = new MainWindow();

            FillField(_mainwindow.tmp_Add_User_First_Name, _mainwindow.tmp_Add_User_Second_Name, _mainwindow.tmp_Add_User_Login, _mainwindow.tmp_Add_User_Password, _mainwindow.tmp_Add_User_Administrator);
        }

        public void FillField(string First_Name, string Second_Name, string Login, string Password, string Administrator)
        {
            try
            {
                textBox3.Text = First_Name;
                textBox3_Copy.Text = Second_Name;
                textBox3_Copy1.Text = Login;
                textBox3_Copy2.Text = "";
                if (Administrator == "True")
                {
                    checkBox.IsChecked = true;
                }
                else if (Administrator == "False")
                {
                    checkBox.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                catcher = "First_Name";
                First_Name = (textBox3.Text);

                catcher = "Second_Name";
                Second_Name = (textBox3_Copy.Text);

                catcher = "Login";
                Login = (textBox3_Copy1.Text);

                catcher = "Password";
                byte[] bytes = Encoding.Unicode.GetBytes(textBox3_Copy2.Text);
                MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
                byte[] byteHash = CSP.ComputeHash(bytes);
                string hash = string.Empty;
                foreach (byte b in byteHash)
                {
                    hash += string.Format("{0:x2}", b);
                }
                Password = (hash);

                catcher = "Administrator";
                if (checkBox.IsChecked == true)
                {
                    Administrator = true;
                }
                else if (checkBox.IsChecked == false)
                {
                    Administrator = false;
                }

                this.DialogResult = true;
            }

            catch (FormatException ex)
            {
                MessageBox.Show("The entered data in <<" + catcher + ">> are incorrect type.");
                client.Logging("The entered data in <<" + catcher + ">> are incorrect type.", ex.StackTrace);
                this.DialogResult = false;
            }
        }
    }
}
