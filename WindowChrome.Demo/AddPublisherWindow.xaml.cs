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
    public partial class AddPublisherWindow : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public MainWindow _mainwindow;
        private string catcher;

        public string Publisher_Name;
        public string Official_Site;

        public AddPublisherWindow()
        {
            InitializeComponent();

            _mainwindow = new MainWindow();

            FillField(_mainwindow.tmp_Add_Publisher_Publisher_Name, _mainwindow.tmp_Add_Publisher_Official_Site);
        }

        public void FillField(string Publisher_Name, string Official_Site)
        {
            try
            {
                textBox3.Text = Publisher_Name;
                textBox6.Text = Official_Site;
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
                catcher = "Publisher_Name";
                Publisher_Name = (textBox3.Text);

                catcher = "Official_Site";
                Official_Site = (textBox6.Text);

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
