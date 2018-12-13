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
    public partial class AddWindow : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public MainWindow _mainwindow;
        private string catcher;

        public int ID_Developer;
        public int ID_Publisher;
        public string Game_Name;
        public int Year_of_Publication;
        public int Brutal_Rating;
        public string Official_Site;
        public bool Cyber_Discipline;
        public bool Network_Mode;

        public AddWindow()
        {
            InitializeComponent();

            _mainwindow = new MainWindow();
            
            client.GetDevelopersEF_WithoutAllCompleted += Cli_GetDevelopersEF_WithoutAllCompleted;
            client.GetDevelopersEF_WithoutAllAsync(null);
            client.GetPublishersEF_WithoutAllCompleted += Cli_GetPublishersEF_WithoutAllCompleted;
            client.GetPublishersEF_WithoutAllAsync();

            comboBox.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            comboBox_Copy.SelectedIndex = 0;
            comboBox_Copy1.SelectedIndex = 0;
            comboBox_Copy2.SelectedIndex = 0;

            FillField(_mainwindow.tmpID_Developer, _mainwindow.tmpID_Publisher, _mainwindow.tmpGame_Name, _mainwindow.tmpYear_of_Publication, _mainwindow.tmpBrutal_Rating, _mainwindow.tmpOfficial_Site, _mainwindow.tmpCyber_Discipline, _mainwindow.tmpNetwork_Mode);
        }

        private void Cli_GetPublishersEF_WithoutAllCompleted(object sender, ServiceReference1.GetPublishersEF_WithoutAllCompletedEventArgs e)
        {
            comboBox_Copy1.ItemsSource = e.Result.ToList();
        }

        private void Cli_GetDevelopersEF_WithoutAllCompleted(object sender, ServiceReference1.GetDevelopersEF_WithoutAllCompletedEventArgs e)
        {
            comboBox_Copy.ItemsSource = e.Result.ToList();
        }

        public void FillField(string ID_Developer, string ID_Publisher, string Game_Name, string Year_of_Publication, string Brutal_Rating, string Official_Site, string Cyber_Discipline, string Network_Mode)
        {
            try
            {
                switch (ID_Developer)
                {
                    case "CD Project RED": comboBox_Copy.SelectedIndex = 0; break;
                    case "Capcom": comboBox_Copy.SelectedIndex = 1; break;
                    case "Bethesda Softworks": comboBox_Copy.SelectedIndex = 2; break;
                    case "Activision": comboBox_Copy.SelectedIndex = 3; break;
                    case "Electronic Arts": comboBox_Copy.SelectedIndex = 4; break;
                    case "Infinity Ward": comboBox_Copy.SelectedIndex = 5; break;
                    case "BioWare": comboBox_Copy.SelectedIndex = 6; break;
                    case "Gearbox Software": comboBox_Copy.SelectedIndex = 7; break;
                    case "Valve": comboBox_Copy.SelectedIndex = 8; break;
                    case "id Software": comboBox_Copy.SelectedIndex = 9; break;
                    case "BioWareTwo": comboBox_Copy.SelectedIndex = 10; break;
                }

                switch (ID_Publisher)
                {
                    case "2K Games": comboBox_Copy1.SelectedIndex = 0; break;
                    case "1С - СофтКлаб": comboBox_Copy1.SelectedIndex = 1; break;
                    case "Capcom": comboBox_Copy1.SelectedIndex = 2; break;
                    case "Bethesda Softworks": comboBox_Copy1.SelectedIndex = 3; break;
                    case "Activision": comboBox_Copy1.SelectedIndex = 4; break;
                    case "Electronic Arts": comboBox_Copy1.SelectedIndex = 5; break;
                    case "Новый Диск": comboBox_Copy1.SelectedIndex = 6; break;
                    case "Valve": comboBox_Copy1.SelectedIndex = 7; break;
                }

                textBox3.Text = Game_Name;
                textBox4.Text = Year_of_Publication;

                switch (Brutal_Rating)
                {
                    case "0": comboBox_Copy2.SelectedIndex = 0; break;
                    case "2": comboBox_Copy2.SelectedIndex = 1; break;
                    case "6": comboBox_Copy2.SelectedIndex = 2; break;
                    case "12": comboBox_Copy2.SelectedIndex = 3; break;
                    case "16": comboBox_Copy2.SelectedIndex = 4; break;
                    case "18": comboBox_Copy2.SelectedIndex = 5; break;
                }

                textBox6.Text = Official_Site;

                if (Cyber_Discipline == "False")
                {
                    comboBox.SelectedIndex = 1;
                }
                else if (Cyber_Discipline == "True")
                {
                    comboBox.SelectedIndex = 0;
                }

                if (Network_Mode == "False")
                {
                    comboBox1.SelectedIndex = 1;
                }
                else if (Network_Mode == "True")
                {
                    comboBox1.SelectedIndex = 0;
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
                catcher = "Developer";
                ServiceReference1.Developers dev = comboBox_Copy.SelectedItem as ServiceReference1.Developers;
                ID_Developer = dev.ID_Developer;

                catcher = "Publisher";
                ServiceReference1.Publishers pub = comboBox_Copy1.SelectedItem as ServiceReference1.Publishers;
                ID_Publisher = pub.ID_Publisher;

                catcher = "Game_Name";
                Game_Name = (textBox3.Text);

                catcher = "Year_of_Publication";
                Year_of_Publication = int.Parse(textBox4.Text);

                catcher = "Brutal_Rating";
                if (comboBox_Copy2.SelectedIndex == 5)
                {
                    Brutal_Rating = 18;
                }
                else
                {
                    Brutal_Rating = int.Parse(comboBox_Copy2.Text);
                }

                catcher = "Official_Site";
                Official_Site = (textBox6.Text);

                catcher = "Cyber_Discipline";
                if (comboBox.SelectedIndex == 0)
                {
                    Cyber_Discipline = true;
                }
                else if (comboBox.SelectedIndex == 1)
                {
                    Cyber_Discipline = false;
                }

                catcher = "Network_Mode";
                if (comboBox1.SelectedIndex == 0)
                {
                    Network_Mode = true;
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    Network_Mode = false;
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
