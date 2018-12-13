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

namespace WindowChrome.Demo
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddDeveloperWindow : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public MainWindow _mainwindow;
        private string catcher;

        public string Developer_Name;
        public string Official_Site;
        public bool Indie;

        public AddDeveloperWindow()
        {
            InitializeComponent();

            _mainwindow = new MainWindow();

            FillField(_mainwindow.tmp_Add_Developer_Developer_name, _mainwindow.tmp_Add_Developer_Official_Site, _mainwindow.tmp_Add_Developer_Indie);
        }

        public void FillField(string Developer_Name, string Official_Site, string Indie)
        {
            try
            {
                textBox3.Text = Developer_Name;
                textBox6.Text = Official_Site;
                if (Indie == "True")
                {
                    checkBox.IsChecked = true;
                }
                else if (Indie == "False")
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
                catcher = "Developer_Name";
                Developer_Name = (textBox3.Text);

                catcher = "Official_Site";
                Official_Site = (textBox6.Text);

                catcher = "Indie";
                if (checkBox.IsChecked == true)
                {
                    Indie = true;
                }
                else if (checkBox.IsChecked == false)
                {
                    Indie = false;
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
