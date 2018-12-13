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
using Serilog.Configuration;

namespace WindowChrome.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    // (СДЕЛАНО) добавить форму для добавления записи
    // (СДЕЛАНО) добавить редактирование записи
    // (СДЕЛАНО) сделать проверку вводимых данных
    // (СДЕЛАНО) сделать вывод поля, в котором были введены некорректные данные
    // (СДЕЛАНО) сделать логирование ошибок: текст ошибки, дата время, название функции где она произошла
    // (СДЕЛАНО) добавить три фильтра: по cyber_discipline, network_mode и brutal_rating
    // (СДЕЛАНО) сделать форму авторизации
    // (СДЕЛАНО) настроить анимацию и запрет действий при выполнении асинхронного запроса
    // (СДЕЛАНО) настроить фильтрацию
    // (СДЕЛАНО) настроить логирование
    // (СДЕЛАНО) хеширование паролей
    // (СДЕЛАНО) добавить таблицу юзеров
    // (СДЕЛАНО) выделить возможности для учётных записей (админ может добавлять юзеров, юзеры могут редактить бд, гость может только просматривать)
    // (СДЕЛАНО) заполнять форму редактирования
    // (СДЕЛАНО) добавить взаимодействие с большим количеством таблиц

    public partial class MainWindow : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        #region Инициализация внутренник списков

        List<ServiceReference1.Games> AllGames = new List<ServiceReference1.Games>();
        List<ServiceReference1.Developers> AllDevelopers = new List<ServiceReference1.Developers>();
        List<ServiceReference1.Publishers> AllPublishers = new List<ServiceReference1.Publishers>();
        List<ServiceReference1.Developers> AllDevelopersWithoutAll = new List<ServiceReference1.Developers>();
        List<ServiceReference1.Publishers> AllPublishersWithoutAll = new List<ServiceReference1.Publishers>();
        List<ServiceReference1.Game_Area> AllPrices = new List<ServiceReference1.Game_Area>();
        List<ServiceReference1.Game_Exhibition> AllRatings = new List<ServiceReference1.Game_Exhibition>();
        List<ServiceReference1.UsersBD> AllUsers = new List<ServiceReference1.UsersBD>();

        #endregion

        #region Инициализация прочих окон

        public AddWindow _addwindow;
        public AddDeveloperWindow _adddeveloperwindow;
        public AddPublisherWindow _addpublisherwindow;
        public AddUserWindow _adduserwindow;

        #endregion

        #region Инициализация временных переменных для процедур редактирования

        public string tmpID_Developer;
        public string tmpID_Publisher;
        public string tmpGame_Name;
        public string tmpYear_of_Publication;
        public string tmpBrutal_Rating;
        public string tmpOfficial_Site;
        public string tmpCyber_Discipline;
        public string tmpNetwork_Mode;

        public string tmp_Add_Developer_Developer_name;
        public string tmp_Add_Developer_Official_Site;
        public string tmp_Add_Developer_Indie;

        public string tmp_Add_Publisher_Publisher_Name;
        public string tmp_Add_Publisher_Official_Site;

        public string tmp_Add_User_First_Name;
        public string tmp_Add_User_Second_Name;
        public string tmp_Add_User_Login;
        public string tmp_Add_User_Password;
        public string tmp_Add_User_Administrator;

        #endregion

        //Конструктор
        public MainWindow()
        {
            InitializeComponent();

            CPB3.Visibility = Visibility.Hidden;

            #region Инициализация таблиц

            LoadGamesGrid();
            LoadDevelopersGrid();
            LoadPublishersGrid();
            LoadGameAreaGrid();
            LoadGameExhibitionGrid();

            #endregion

            #region Инициализация комбобоксов

            LoadDeveloperCB();
            LoadPublisherCB();
            cbBrutals.SelectedIndex = 0;

            #endregion
        }

        #region Действия, выполняемые после завершения асинхронных операций на сервере
        private void Client_GetPublishersEF_WithoutAllCompleted(object sender, ServiceReference1.GetPublishersEF_WithoutAllCompletedEventArgs e)
        {
            AllPublishersWithoutAll = e.Result.ToList();
            dgPublishers.ItemsSource = AllPublishersWithoutAll;
        }

        private void Client_GetDevelopersEF_WithoutAllCompleted(object sender, ServiceReference1.GetDevelopersEF_WithoutAllCompletedEventArgs e)
        {
            AllDevelopersWithoutAll = e.Result.ToList();
            dgDevelopers.ItemsSource = AllDevelopersWithoutAll;
        }

        private void Client_GetUsersTableEFCompleted(object sender, ServiceReference1.GetUsersTableEFCompletedEventArgs e)
        {
            AllUsers = e.Result.ToList();
            dgUsers.ItemsSource = AllUsers;
        }

        private void Cli_GetPublishersEFCompleted(object sender, ServiceReference1.GetPublishersEFCompletedEventArgs e)
        {
            AllPublishers = e.Result.ToList();
            cbPublishers.ItemsSource = AllPublishers;
            cbPublishers.SelectedIndex = 0;
        }

        private void Cli_GetDevelopersEFCompleted(object sender, ServiceReference1.GetDevelopersEFCompletedEventArgs e)
        {
            AllDevelopers = e.Result.ToList();
            cbDevelopers.ItemsSource = AllDevelopers;
            cbDevelopers.SelectedIndex = 0;
        }

        private void Client_GetGameAreaEFCompleted(object sender, ServiceReference1.GetGameAreaEFCompletedEventArgs e)
        {
            AllPrices = e.Result.ToList();
            dgGameArea.ItemsSource = AllPrices;
        }

        private void Client_GetGameExhibitionEFCompleted(object sender, ServiceReference1.GetGameExhibitionEFCompletedEventArgs e)
        {
            AllRatings = e.Result.ToList();
            dgGameExhibition.ItemsSource = AllRatings;
        }

        private void Cli_GetGamesEFCompleted(object sender, ServiceReference1.GetGamesEFCompletedEventArgs e)
        {
            AllGames = e.Result.ToList();
            dgGames.ItemsSource = AllGames;
            CPB3.Visibility = Visibility.Hidden;
            if (button_Copy15.IsEnabled)
            {
                button_Copy2.IsEnabled = true;
                button_Copy1.IsEnabled = true;
                button_Copy.IsEnabled = true;
            }
        }

        private void Client_GetSpecialGamesEFCompleted(object sender, ServiceReference1.GetSpecialGamesEFCompletedEventArgs e)
        {
            dgGames.ItemsSource = e.Result.ToList();
            CPB3.Visibility = Visibility.Hidden;
            if (button_Copy15.IsEnabled)
            {
                button_Copy2.IsEnabled = true;
                button_Copy1.IsEnabled = true;
                button_Copy.IsEnabled = true;
            }
        }
        #endregion

        #region Загрузка таблиц
        public void LoadGamesGrid()
        {
            try
            {
                ServiceReference1.Games Game = new ServiceReference1.Games();
                client.GetGamesEFCompleted += Cli_GetGamesEFCompleted;
                CPB3.Visibility = Visibility.Visible;
                if (button_Copy15.IsEnabled)
                {
                    button_Copy2.IsEnabled = true;
                    button_Copy1.IsEnabled = true;
                    button_Copy.IsEnabled = true;
                }
                client.GetGamesEFAsync();
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        public void LoadDeveloperCB()
        {
            ServiceReference1.Developers Developer = new ServiceReference1.Developers();
            client.GetDevelopersEFCompleted += Cli_GetDevelopersEFCompleted;
            client.GetDevelopersEFAsync();
        }

        public void LoadPublisherCB()
        {
            ServiceReference1.Publishers Publisher = new ServiceReference1.Publishers();
            client.GetPublishersEFCompleted += Cli_GetPublishersEFCompleted;
            client.GetPublishersEFAsync();
        }

        public void LoadDevelopersGrid()
        {
            try
            {
                client.GetDevelopersEF_WithoutAllCompleted += Client_GetDevelopersEF_WithoutAllCompleted;
                client.GetDevelopersEF_WithoutAllAsync(null);
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        public void LoadPublishersGrid()
        {
            try
            {
                client.GetPublishersEF_WithoutAllCompleted += Client_GetPublishersEF_WithoutAllCompleted;
                client.GetPublishersEF_WithoutAllAsync();
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        public void LoadUsersGrid()
        {
            try
            {
                client.GetUsersTableEFCompleted += Client_GetUsersTableEFCompleted;
                client.GetUsersTableEFAsync();
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        public void LoadGameAreaGrid()
        {
            try
            {
                client.GetGameAreaEFCompleted += Client_GetGameAreaEFCompleted;
                client.GetGameAreaEFAsync();
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        public void LoadGameExhibitionGrid()
        {
            try
            {
                client.GetGameExhibitionEFCompleted += Client_GetGameExhibitionEFCompleted;
                client.GetGameExhibitionEFAsync();
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Фильтрация
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                client.GetDevelopersEF_WithoutAllAsync(true);
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                client.GetDevelopersEF_WithoutAllAsync(null);
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void CheckCBstatus(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ServiceReference1.Games Game = new ServiceReference1.Games();
                ServiceReference1.Developers dev = cbDevelopers.SelectedItem as ServiceReference1.Developers;
                ServiceReference1.Publishers pub = cbPublishers.SelectedItem as ServiceReference1.Publishers;
                client.GetSpecialGamesEFCompleted += Client_GetSpecialGamesEFCompleted;
                CPB3.Visibility = Visibility.Visible;
                button_Copy2.IsEnabled = false;
                button_Copy1.IsEnabled = false;
                button_Copy.IsEnabled = false;

                if (cbDevelopers.SelectedIndex == 0 && cbPublishers.SelectedIndex == 0 && cbBrutals.SelectedIndex == 0)
                {
                    LoadGamesGrid();
                }
                else if (cbDevelopers.SelectedIndex != 0 && cbPublishers.SelectedIndex == 0 && cbBrutals.SelectedIndex == 0)
                {
                    client.GetSpecialGamesEFAsync(dev.ID_Developer, null, null);
                }
                else if (cbDevelopers.SelectedIndex == 0 && cbPublishers.SelectedIndex != 0 && cbBrutals.SelectedIndex == 0)
                {
                    client.GetSpecialGamesEFAsync(null, pub.ID_Publisher, null);
                }
                else if (cbDevelopers.SelectedIndex == 0 && cbPublishers.SelectedIndex == 0 && cbBrutals.SelectedIndex != 0)
                {
                    //if (cbBrutals.SelectedIndex < 6) client.GetSpecialGamesEFAsync(null, null, Int32.Parse(cbBrutals.SelectedItem.ToString()));
                    //else client.GetSpecialGamesEFAsync(null, null, 18);
                    switch (cbBrutals.SelectedIndex)
                    {
                        case 1: client.GetSpecialGamesEFAsync(null, null, 0); break;
                        case 2: client.GetSpecialGamesEFAsync(null, null, 2); break;
                        case 3: client.GetSpecialGamesEFAsync(null, null, 6); break;
                        case 4: client.GetSpecialGamesEFAsync(null, null, 12); break;
                        case 5: client.GetSpecialGamesEFAsync(null, null, 16); break;
                        case 6: client.GetSpecialGamesEFAsync(null, null, 18); break;
                    }
                }
                else if (cbDevelopers.SelectedIndex != 0 && cbPublishers.SelectedIndex != 0 && cbBrutals.SelectedIndex == 0)
                {
                    client.GetSpecialGamesEFAsync(dev.ID_Developer, pub.ID_Publisher, null);
                }
                else if (cbDevelopers.SelectedIndex != 0 && cbPublishers.SelectedIndex == 0 && cbBrutals.SelectedIndex != 0)
                {
                    switch (cbBrutals.SelectedIndex)
                    {
                        case 1: client.GetSpecialGamesEFAsync(dev.ID_Developer, null, 0); break;
                        case 2: client.GetSpecialGamesEFAsync(dev.ID_Developer, null, 2); break;
                        case 3: client.GetSpecialGamesEFAsync(dev.ID_Developer, null, 6); break;
                        case 4: client.GetSpecialGamesEFAsync(dev.ID_Developer, null, 12); break;
                        case 5: client.GetSpecialGamesEFAsync(dev.ID_Developer, null, 16); break;
                        case 6: client.GetSpecialGamesEFAsync(dev.ID_Developer, null, 18); break;
                    }
                }
                else if (cbDevelopers.SelectedIndex == 0 && cbPublishers.SelectedIndex != 0 && cbBrutals.SelectedIndex != 0)
                {
                    switch (cbBrutals.SelectedIndex)
                    {
                        case 1: client.GetSpecialGamesEFAsync(null, pub.ID_Publisher, 0); break;
                        case 2: client.GetSpecialGamesEFAsync(null, pub.ID_Publisher, 2); break;
                        case 3: client.GetSpecialGamesEFAsync(null, pub.ID_Publisher, 6); break;
                        case 4: client.GetSpecialGamesEFAsync(null, pub.ID_Publisher, 12); break;
                        case 5: client.GetSpecialGamesEFAsync(null, pub.ID_Publisher, 16); break;
                        case 6: client.GetSpecialGamesEFAsync(null, pub.ID_Publisher, 18); break;
                    }
                }
                else if (cbDevelopers.SelectedIndex != 0 && cbPublishers.SelectedIndex != 0 && cbBrutals.SelectedIndex != 0)
                {
                    switch (cbBrutals.SelectedIndex)
                    {
                        case 1: client.GetSpecialGamesEFAsync(dev.ID_Developer, pub.ID_Publisher, 0); break;
                        case 2: client.GetSpecialGamesEFAsync(dev.ID_Developer, pub.ID_Publisher, 2); break;
                        case 3: client.GetSpecialGamesEFAsync(dev.ID_Developer, pub.ID_Publisher, 6); break;
                        case 4: client.GetSpecialGamesEFAsync(dev.ID_Developer, pub.ID_Publisher, 12); break;
                        case 5: client.GetSpecialGamesEFAsync(dev.ID_Developer, pub.ID_Publisher, 16); break;
                        case 6: client.GetSpecialGamesEFAsync(dev.ID_Developer, pub.ID_Publisher, 18); break;
                    }
                }
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Сброс фильтров, перезагрузка, добавление, редактирование и удаление для таблицы Игры
        private void button_Copy11_Click(object sender, RoutedEventArgs e)
        {
            cbDevelopers.SelectedIndex = 0;
            cbPublishers.SelectedIndex = 0;
            cbBrutals.SelectedIndex = 0;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            LoadGamesGrid();
            LoadDeveloperCB();
            LoadPublisherCB();
            cbDevelopers.SelectedIndex = 0;
            cbPublishers.SelectedIndex = 0;
            cbBrutals.SelectedIndex = 0;
        }

        private void button_Copy_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                _addwindow = new AddWindow();

                if (_addwindow.ShowDialog() == true)
                {
                    client.AddGame(_addwindow.ID_Developer, _addwindow.ID_Publisher, _addwindow.Game_Name, _addwindow.Year_of_Publication, _addwindow.Brutal_Rating, _addwindow.Official_Site, _addwindow.Cyber_Discipline, _addwindow.Network_Mode);
                    cbDevelopers.SelectedIndex = 0;
                    cbPublishers.SelectedIndex = 0;
                    cbBrutals.SelectedIndex = 0;
                    button_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("No changes happened");
                }
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _addwindow = new AddWindow();

                ServiceReference1.Games Game = new ServiceReference1.Games();
                int upGame = Convert.ToInt32(dgGames.SelectedIndex);
                upGame = AllGames[upGame].ID_Game;

                List<DataGridCellInfo> tmp = dgGames.SelectedCells.ToList();
                ServiceReference1.Games tmpGame = tmp[0].Item as ServiceReference1.Games;
                tmpID_Developer = tmpGame.Developers.Developer_Name.ToString();
                tmpID_Publisher = tmpGame.Publishers.Publisher_Name.ToString();
                tmpGame_Name = tmpGame.Game_Name.ToString();
                tmpYear_of_Publication = tmpGame.Year_of_Publication.ToString();
                tmpBrutal_Rating = tmpGame.Brutal_Rating.ToString();
                tmpOfficial_Site = tmpGame.Official_Site.ToString();
                tmpCyber_Discipline = tmpGame.Cyber_Discipline.ToString();
                tmpNetwork_Mode = tmpGame.Network_Mode.ToString();

                _addwindow.FillField(tmpID_Developer, tmpID_Publisher, tmpGame_Name, tmpYear_of_Publication, tmpBrutal_Rating, tmpOfficial_Site, tmpCyber_Discipline, tmpNetwork_Mode);

                if (_addwindow.ShowDialog() == true)
                {
                    client.UpdateGame(upGame, _addwindow.ID_Developer, _addwindow.ID_Publisher, _addwindow.Game_Name, _addwindow.Year_of_Publication, _addwindow.Brutal_Rating, _addwindow.Official_Site, _addwindow.Cyber_Discipline, _addwindow.Network_Mode);
                    button_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("No changes happened!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int dltGame1 = Convert.ToInt32(dgGames.SelectedIndex);
                    dltGame1 = AllGames[dltGame1].ID_Game;
                    client.DeleteGame(dltGame1);
                    button_Click(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Перезагрузка, добавление, редактирование и удаление для таблицы Разработчики
        private void button_Copy3_Click(object sender, RoutedEventArgs e)
        {
            LoadDevelopersGrid();
            LoadDeveloperCB();
        }

        private void button_Copy4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _adddeveloperwindow = new AddDeveloperWindow();

                if (_adddeveloperwindow.ShowDialog() == true)
                {
                    client.AddDeveloper(_adddeveloperwindow.Developer_Name, _adddeveloperwindow.Official_Site, _adddeveloperwindow.Indie);
                    button_Copy3_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("No changes happened");
                }
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void button_Copy5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _adddeveloperwindow = new AddDeveloperWindow();

                ServiceReference1.Developers Developer = new ServiceReference1.Developers();
                int upDeveloper = Convert.ToInt32(dgDevelopers.SelectedIndex);
                upDeveloper = AllDevelopersWithoutAll[upDeveloper].ID_Developer;

                List<DataGridCellInfo> tmp = dgDevelopers.SelectedCells.ToList();
                ServiceReference1.Developers tmpDeveloper = tmp[0].Item as ServiceReference1.Developers;
                tmp_Add_Developer_Developer_name = tmpDeveloper.Developer_Name.ToString();
                tmp_Add_Developer_Official_Site = tmpDeveloper.Official_Site.ToString();
                tmp_Add_Developer_Indie = tmpDeveloper.Indie.ToString();

                _adddeveloperwindow.FillField(tmp_Add_Developer_Developer_name, tmp_Add_Developer_Official_Site, tmp_Add_Developer_Indie);

                if (_adddeveloperwindow.ShowDialog() == true)
                {
                    client.UpdateDeveloper(upDeveloper, _adddeveloperwindow.Developer_Name, _adddeveloperwindow.Official_Site, _adddeveloperwindow.Indie);
                    button_Copy3_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("No changes happened!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void button_Copy6_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int dltDeveloper1 = Convert.ToInt32(dgDevelopers.SelectedIndex);
                    dltDeveloper1 = AllDevelopersWithoutAll[dltDeveloper1].ID_Developer;
                    client.DeleteDeveloper(dltDeveloper1);
                    button_Copy3_Click(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Перезагрузка, добавление, редактирование и удаление для таблицы Издательства
        private void button_Copy7_Click(object sender, RoutedEventArgs e)
        {
            LoadPublishersGrid();
            LoadPublisherCB();
        }

        private void button_Copy8_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _addpublisherwindow = new AddPublisherWindow();

                if (_addpublisherwindow.ShowDialog() == true)
                {
                    client.AddPublisher(_addpublisherwindow.Publisher_Name, _addpublisherwindow.Official_Site);
                    button_Copy7_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("No changes happened");
                }
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void button_Copy9_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _addpublisherwindow = new AddPublisherWindow();

                ServiceReference1.Publishers Publisher = new ServiceReference1.Publishers();
                int upPublisher = Convert.ToInt32(dgPublishers.SelectedIndex);
                upPublisher = AllPublishersWithoutAll[upPublisher].ID_Publisher;

                List<DataGridCellInfo> tmp = dgPublishers.SelectedCells.ToList();
                ServiceReference1.Publishers tmpPublisher = tmp[0].Item as ServiceReference1.Publishers;
                tmp_Add_Publisher_Publisher_Name = tmpPublisher.Publisher_Name.ToString();
                tmp_Add_Publisher_Official_Site = tmpPublisher.Official_Site.ToString();

                _addpublisherwindow.FillField(tmp_Add_Publisher_Publisher_Name, tmp_Add_Publisher_Official_Site);

                if (_addpublisherwindow.ShowDialog() == true)
                {
                    client.UpdatePublisher(upPublisher, _addpublisherwindow.Publisher_Name, _addpublisherwindow.Official_Site);
                    button_Copy7_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("No changes happened!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void button_Copy10_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int dltPublisher1 = Convert.ToInt32(dgPublishers.SelectedIndex);
                    dltPublisher1 = AllPublishersWithoutAll[dltPublisher1].ID_Publisher;
                    client.DeletePublisher(dltPublisher1);
                    button_Copy7_Click(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Перезагрузка, добавление, редактирование и удаление для таблицы Пользователи
        private void button_Copy12_Click(object sender, RoutedEventArgs e)
        {
            LoadUsersGrid();
        }

        private void button_Copy13_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _adduserwindow = new AddUserWindow();

                if (_adduserwindow.ShowDialog() == true)
                {
                    client.AddUser(_adduserwindow.First_Name, _adduserwindow.Second_Name, _adduserwindow.Login, _adduserwindow.Password, _adduserwindow.Administrator);
                    button_Copy12_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("No changes happened");
                }
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void button_Copy14_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _adduserwindow = new AddUserWindow();

                ServiceReference1.UsersBD User = new ServiceReference1.UsersBD();
                int upUser = Convert.ToInt32(dgUsers.SelectedIndex);
                upUser = AllUsers[upUser].ID_User;

                List<DataGridCellInfo> tmp = dgUsers.SelectedCells.ToList();
                ServiceReference1.UsersBD tmpUser = tmp[0].Item as ServiceReference1.UsersBD;
                tmp_Add_User_First_Name = tmpUser.First_Name.ToString();
                tmp_Add_User_Second_Name = tmpUser.Second_Name.ToString();
                tmp_Add_User_Login = tmpUser.Login.ToString();
                tmp_Add_User_Password = tmpUser.Password.ToString();
                tmp_Add_User_Administrator = tmpUser.Administrator.ToString();

                _adduserwindow.FillField(tmp_Add_User_First_Name, tmp_Add_User_Second_Name, tmp_Add_User_Login, tmp_Add_User_Password, tmp_Add_User_Administrator);

                if (_adduserwindow.ShowDialog() == true)
                {
                    client.UpdateUser(upUser, _adduserwindow.First_Name, _adduserwindow.Second_Name, _adduserwindow.Login, _adduserwindow.Password, _adduserwindow.Administrator);
                    button_Copy12_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("No changes happened!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                client.Logging(ex.Message, ex.StackTrace);
            }
        }

        private void button_Copy15_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int dltUser1 = Convert.ToInt32(dgUsers.SelectedIndex);
                    dltUser1 = AllUsers[dltUser1].ID_User;
                    client.DeleteUser(dltUser1);
                    button_Copy12_Click(sender, e);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Действия, выполняемые по закрытию приложения
        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            string msg = "Close programm?";
            MessageBoxResult result =
                MessageBox.Show(
                msg,
                "Data App",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region Ограничения возможностей в соответствии с типом учётной записи
        public void Limitations()
        {
            try
            {
                switch (label_Copy4.Content.ToString())
                {
                    case "GUEST":
                        {
                            button_Copy.IsEnabled = false;
                            button_Copy1.IsEnabled = false;
                            button_Copy2.IsEnabled = false;
                            button_Copy3.IsEnabled = false;
                            button_Copy4.IsEnabled = false;
                            button_Copy5.IsEnabled = false;
                            button_Copy6.IsEnabled = false;
                            button_Copy7.IsEnabled = false;
                            button_Copy8.IsEnabled = false;
                            button_Copy9.IsEnabled = false;
                            button_Copy10.IsEnabled = false;
                            button_Copy12.IsEnabled = false;
                            button_Copy13.IsEnabled = false;
                            button_Copy14.IsEnabled = false;
                            button_Copy15.IsEnabled = false;
                            button_Copy16.IsEnabled = false;
                            button_Copy17.IsEnabled = false;
                            button_Copy18.IsEnabled = false;
                            button_Copy19.IsEnabled = false;
                            button_Copy20.IsEnabled = false;
                            button_Copy21.IsEnabled = false;
                            button_Copy22.IsEnabled = false;
                            button_Copy23.IsEnabled = false;
                            break;
                        }
                    case "Administrator":
                        {
                            tabUsers.Visibility = Visibility.Visible;
                            LoadUsersGrid();
                            break;
                        }
                    case "User":
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                client.Logging(ex.Message, ex.StackTrace);
            }
        }
        #endregion

    }
}