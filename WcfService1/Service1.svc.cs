using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        db_MinligareevMAContext _context = new db_MinligareevMAContext();
        db_TalonsEntities1 _context2 = new db_TalonsEntities1();

        public void AddElectronicScreen(string Talon)
        {
            Talons talon = new Talons();
            talon.name = Talon;
            try
            {
                _context2.Talons.Add(talon);
                _context2.SaveChanges();
                //MessageBox.Show(string.Join(",", Talons.ToArray()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }

        public void SelectAndDeleteTalonFromOCHERED(int id_okna)
        {
            try
            {
                var talon = (from t in _context2.Talons select t.name).First().ToString();

                var status = _context2.WindowStatus.Where(s => s.id == id_okna).FirstOrDefault();
                Log(id_okna, talon);
                status.status = talon;
                _context2.SaveChanges();

                Talons del_talon = (from t in _context2.Talons where t.name == talon select t).SingleOrDefault();
                _context2.Talons.Remove(del_talon);
                _context2.SaveChanges();
            }
            catch
            {

            }
        }

        public string SelectNaEKRAN(int id_okna)
        {
            try
            {
                var talon = (from t in _context2.WindowStatus where t.id == id_okna select t.status).FirstOrDefault().ToString();

                return talon;
            }
            catch
            {
                return "-";
            }
        }

        public void YaSVOBODEN(int id_okna)
        {
            var status = _context2.WindowStatus.Where(s => s.id == id_okna).FirstOrDefault();
            status.status = "-";
            _context2.SaveChanges();

        }

        // Я подготовил тебе функцию для вывода всех Клиентов, ожидающих в очереди
        public List<Talons> GetTalons()
        {
            List<Talons> Talon = new List<Talons>();
            foreach (var el in _context2.Talons)
            {
                Talons t = new Talons();
                t.id = el.id;
                t.name = el.name;
                Talon.Add(t);
            }
            return Talon.ToList();
        }
       
        public void Log(int id_okno, string name_talon)
        {
           
            Logging3 log3 = new Logging3();
            log3.id_okna3 = id_okno;
            log3.name_talon3 = name_talon;
            log3.time = DateTime.Now.Minute.ToString();
            var oldtimesting = _context2.Logging3.Where(s => s.id_okna3 == id_okno).FirstOrDefault();
            log3.rabota_s_klientom = (DateTime.Now.Minute - Int32.Parse(oldtimesting.time)).ToString();

            try
            {
                _context2.Logging3.Add(log3);
                _context2.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }



        






















        #region old
        public List<Games> GetGamesEF()
        {
            List<Games> Game = new List<Games>();
            foreach (var el in _context.Games)
            {
                Games g = new Games();
                g.ID_Game = el.ID_Game;

                g.Developers = new Developers();
                g.Developers.ID_Developer = el.Developers.ID_Developer;
                g.Developers.Developer_Name = el.Developers.Developer_Name;

                g.Publishers = new Publishers();
                g.Publishers.ID_Publisher = el.Publishers.ID_Publisher;
                g.Publishers.Publisher_Name = el.Publishers.Publisher_Name;

                g.Game_Name = el.Game_Name;
                g.Year_of_Publication = el.Year_of_Publication;
                g.Brutal_Rating = el.Brutal_Rating;
                g.Official_Site = el.Official_Site;
                g.Cyber_Discipline = el.Cyber_Discipline;
                g.Network_Mode = el.Network_Mode;
                Game.Add(g);
            }
            return Game.ToList();
        }
        public List<Developers> GetDevelopersEF()
        {
            List<Developers> Developer = new List<Developers>();
            foreach (var el in _context.Developers)
            {
                Developers d = new Developers();
                d.ID_Developer = el.ID_Developer;
                d.Developer_Name = el.Developer_Name;
                d.Official_Site = el.Official_Site;
                d.Indie = el.Indie;
                Developer.Add(d);
            }
            Developers All = new Developers();
            All.ID_Developer = -1;
            All.Developer_Name = "All Developers";
            Developer.Insert(0, All);
            return Developer.ToList();
        }
        public List<Publishers> GetPublishersEF()
        {
            List<Publishers> Publisher = new List<Publishers>();
            foreach (var el in _context.Publishers)
            {
                Publishers p = new Publishers();
                p.ID_Publisher = el.ID_Publisher;
                p.Publisher_Name = el.Publisher_Name;
                p.Official_Site = el.Official_Site;
                Publisher.Add(p);
            }
            Publishers All = new Publishers();
            All.ID_Publisher = -1;
            All.Publisher_Name = "All Publishers";
            Publisher.Insert(0, All);
            return Publisher.ToList();
        }
        public List<Developers> GetDevelopersEF_WithoutAll(bool? Active)
        {
            List<Developers> resDeveloper = new List<Developers>();
            var dev = _context.Developers.Where(o => o.ID_Developer >= 0);
            if (Active.HasValue) dev = dev.Where(o => o.Indie == Active);
            //if (idGame.HasValue) dev = dev.Where(o => o.Games.Where(q => q.ID_Game == idGame).Count() > 0);
            foreach (var el in dev)
            {
                Developers d = new Developers();
                d.ID_Developer = el.ID_Developer;
                d.Developer_Name = el.Developer_Name;
                d.Official_Site = el.Official_Site;
                d.Indie = el.Indie;
                resDeveloper.Add(d);
            }
            return resDeveloper.ToList();
        }
        public List<Publishers> GetPublishersEF_WithoutAll()
        {
            List<Publishers> Publisher = new List<Publishers>();
            foreach (var el in _context.Publishers)
            {
                Publishers p = new Publishers();
                p.ID_Publisher = el.ID_Publisher;
                p.Publisher_Name = el.Publisher_Name;
                p.Official_Site = el.Official_Site;
                Publisher.Add(p);
            }
            return Publisher.ToList();
        }
        public List<Games> GetSpecialGamesEF(int? id_dev, int? id_pub, double? age)
        {
            List<Games> Game = new List<Games>();
            var _game = _context.Games.Where(o => o.ID_Game >= 0);
            if (id_dev.HasValue) _game = _game.Where(o => o.ID_Developer == id_dev);
            if (id_pub.HasValue) _game = _game.Where(o => o.ID_Publisher == id_pub);
            if (age.HasValue) _game = _game.Where(o => o.Brutal_Rating == age);
            //if (id_dev.HasValue && id_pub.HasValue) _game = _game.Where(o => o.ID_Developer == id_dev && o.ID_Publisher == id_pub);
            foreach (var el in _game)
            {
                Games g = new Games();
                g.ID_Game = el.ID_Game;

                g.Developers = new Developers();
                g.Developers.ID_Developer = el.Developers.ID_Developer;
                g.Developers.Developer_Name = el.Developers.Developer_Name;

                g.Publishers = new Publishers();
                g.Publishers.ID_Publisher = el.Publishers.ID_Publisher;
                g.Publishers.Publisher_Name = el.Publishers.Publisher_Name;

                g.Game_Name = el.Game_Name;
                g.Year_of_Publication = el.Year_of_Publication;
                g.Brutal_Rating = el.Brutal_Rating;
                g.Official_Site = el.Official_Site;
                g.Cyber_Discipline = el.Cyber_Discipline;
                g.Network_Mode = el.Network_Mode;
                Game.Add(g);
            }
            return Game.ToList();
        }
        public List<UsersBD> GetUsersTableEF()
        {
            List<UsersBD> User = new List<UsersBD>();
            foreach (var el in _context.UsersBD)
            {
                UsersBD u = new UsersBD();
                u.ID_User = el.ID_User;
                u.First_Name = el.First_Name;
                u.Second_Name = el.Second_Name;
                u.Login = el.Login;
                u.Password = el.Password;
                u.Administrator = el.Administrator;
                User.Add(u);
            }
            return User.ToList();
        }
        public List<UsersBD> GetUsersEF(string Login, string Password)
        {
            bool find = false;
            List<UsersBD> User = new List<UsersBD>();
            foreach (var el in _context.UsersBD)
            {
                if (el.Login == Login && el.Password == Password)
                {
                    UsersBD u = new UsersBD();
                    find = true;
                    u.First_Name = el.First_Name;
                    u.Second_Name = el.Second_Name;
                    u.Administrator = el.Administrator;
                    User.Add(u);
                }
            }
            if (find == false)
            {
                MessageBox.Show("User is not found");
            }
            return User.ToList();
        }
        public List<Game_Area> GetGameAreaEF()
        {
            List<Game_Area> GameArea = new List<Game_Area>();
            foreach (var el in _context.Game_Area)
            {
                Game_Area ga = new Game_Area();
                ga.ID_Game_Area = el.ID_Game_Area;

                ga.Games = new Games();
                ga.Games.ID_Game = el.Games.ID_Game;
                ga.Games.Game_Name = el.Games.Game_Name;

                ga.Area = new Area();
                ga.Area.ID_Area = el.Area.ID_Area;
                ga.Area.Area_Name = el.Area.Area_Name;

                ga.Area_Game_Cost = el.Area_Game_Cost;
                GameArea.Add(ga);
            }
            return GameArea.ToList();
        }
        public List<Game_Exhibition> GetGameExhibitionEF()
        {
            List<Game_Exhibition> GameExhibition = new List<Game_Exhibition>();
            foreach (var el in _context.Game_Exhibition)
            {
                Game_Exhibition ge = new Game_Exhibition();
                ge.ID_Game_Exhibition = el.ID_Game_Exhibition;

                ge.Games = new Games();
                ge.Games.ID_Game = el.Games.ID_Game;
                ge.Games.Game_Name = el.Games.Game_Name;

                ge.Exhibitions = new Exhibitions();
                ge.Exhibitions.ID_Exhibition = el.Exhibitions.ID_Exhibition;
                ge.Exhibitions.Exhibition_Name = el.Exhibitions.Exhibition_Name;

                ge.Game_Rating_On_Exhibition = el.Game_Rating_On_Exhibition;
                GameExhibition.Add(ge);
            }
            return GameExhibition.ToList();
        }

        public void Logging(string Message, string Stack)
        {
            if (string.IsNullOrEmpty(Message) && string.IsNullOrEmpty(Stack)) return;

            SqlConnection SQlCon = new SqlConnection(@"Data Source=G-NIGHT\GNIGHTSERVER;Initial Catalog=db_MinligareevMA;Integrated Security=True");
            SQlCon.Open();
            string sqlQuery = "Insert into errors (error_method,error_datatime,error_message) Values ('" + Stack + "','" + DateTime.Now + "','" + Message + "')";
            SqlCommand sqlCd = new SqlCommand(sqlQuery, SQlCon);
            sqlCd.ExecuteNonQuery();
            SQlCon.Close();

            //var _context = new db_MinligareevMAContext();
            //errors error = new errors();
            //error.error_message = Message;
            //error.error_method = Stack;
            //error.error_datatime = DateTime.Now;
            //_context.errors.Add(error);
            //_context.SaveChanges();
        }

        public void AddGame(int ID_Developer, int ID_Publisher, string Game_Name, int Year_of_Publication, int Brutal_Rating, string Official_Site, bool Cyber_Discipline, bool Network_Mode)
        {
            var _context = new db_MinligareevMAContext();
            Games game = new Games();
            game.ID_Developer = ID_Developer;
            game.ID_Publisher = ID_Publisher;
            game.Game_Name = Game_Name;
            game.Year_of_Publication = Year_of_Publication;
            game.Brutal_Rating = Brutal_Rating;
            game.Official_Site = Official_Site;
            game.Cyber_Discipline = Cyber_Discipline;
            game.Network_Mode = Network_Mode;
            try
            {
                _context.Games.Add(game);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }
        public void UpdateGame(int ID_Game, int ID_Developer, int ID_Publisher, string Game_Name, int Year_of_Publication, double Brutal_Rating, string Official_Site, bool Cyber_Discipline, bool Network_Mode)
        {
            //можно просто
            //var _context = new db_MinligareevMAContext();
            using (db_MinligareevMAContext _context = new db_MinligareevMAContext())
            {
                try
                {
                    var game = _context.Games.Where(g => g.ID_Game == ID_Game).FirstOrDefault();
                    game.ID_Developer = ID_Developer;
                    game.ID_Publisher = ID_Publisher;
                    game.Game_Name = Game_Name;
                    game.Year_of_Publication = Year_of_Publication;
                    game.Brutal_Rating = Brutal_Rating;
                    game.Official_Site = Official_Site;
                    game.Cyber_Discipline = Cyber_Discipline;
                    game.Network_Mode = Network_Mode;
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Warning");
                    Logging(ex.Message, ex.StackTrace);
                }
            }

        }
        public void DeleteGame(int ID)
        {
            Games game = (from g in _context.Games where g.ID_Game == ID select g).SingleOrDefault();
            try
            {
                _context.Games.Remove(game);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }

        public void AddDeveloper(string Developer_Name, string Official_Site, bool Indie)
        {
            var _context = new db_MinligareevMAContext();
            Developers developer = new Developers();
            developer.Developer_Name = Developer_Name;
            developer.Official_Site = Official_Site;
            developer.Indie = Indie;
            try
            {
                _context.Developers.Add(developer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }
        public void UpdateDeveloper(int ID_Developer, string Developer_Name, string Official_Site, bool Indie)
        {
            using (db_MinligareevMAContext _context = new db_MinligareevMAContext())
            {
                try
                {
                    var developer = _context.Developers.Where(d => d.ID_Developer == ID_Developer).FirstOrDefault();
                    developer.Developer_Name = Developer_Name;
                    developer.Official_Site = Official_Site;
                    developer.Indie = Indie;
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Warning");
                    Logging(ex.Message, ex.StackTrace);
                }
            }
        }
        public void DeleteDeveloper(int ID)
        {
            try
            {
                foreach (var el in _context.Games)
                {
                    if (el.ID_Developer == ID)
                    {
                        _context.Games.Remove(el);
                    }
                }
                Developers developer = (from d in _context.Developers where d.ID_Developer == ID select d).SingleOrDefault();
                _context.Developers.Remove(developer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }

        public void AddPublisher(string Publisher_Name, string Official_Site)
        {
            var _context = new db_MinligareevMAContext();
            Publishers publisher = new Publishers();
            publisher.Publisher_Name = Publisher_Name;
            publisher.Official_Site = Official_Site;
            try
            {
                _context.Publishers.Add(publisher);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }
        public void UpdatePublisher(int ID_Publisher, string Publisher_Name, string Official_Site)
        {
            using (db_MinligareevMAContext _context = new db_MinligareevMAContext())
            {
                try
                {
                    var publisher = _context.Publishers.Where(p => p.ID_Publisher == ID_Publisher).FirstOrDefault();
                    publisher.Publisher_Name = Publisher_Name;
                    publisher.Official_Site = Official_Site;
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Warning");
                    Logging(ex.Message, ex.StackTrace);
                }
            }
        }
        public void DeletePublisher(int ID)
        {
            try
            {
                foreach (var el in _context.Games)
                {
                    if (el.ID_Publisher == ID)
                    {
                        _context.Games.Remove(el);
                    }
                }
                Publishers publisher = (from p in _context.Publishers where p.ID_Publisher == ID select p).SingleOrDefault();
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }

        public void AddUser(string First_Name, string Second_Name, string Login, string Password, bool Administrator)
        {
            var _context = new db_MinligareevMAContext();
            UsersBD user = new UsersBD();
            user.First_Name = First_Name;
            user.Second_Name = Second_Name;
            user.Login = Login;
            user.Password = Password;
            user.Administrator = Administrator;
            try
            {
                _context.UsersBD.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }
        public void UpdateUser(int ID_User, string First_Name, string Second_Name, string Login, string Password, bool Administrator)
        {
            using (db_MinligareevMAContext _context = new db_MinligareevMAContext())
            {
                try
                {
                    var user = _context.UsersBD.Where(u => u.ID_User == ID_User).FirstOrDefault();
                    user.First_Name = First_Name;
                    user.Second_Name = Second_Name;
                    user.Login = Login;
                    user.Password = Password;
                    user.Administrator = Administrator;
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Warning");
                    Logging(ex.Message, ex.StackTrace);
                }
            }
        }
        public void DeleteUser(int ID)
        {
            UsersBD user = (from u in _context.UsersBD where u.ID_User == ID select u).SingleOrDefault();
            try
            {
                _context.UsersBD.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }

        public void AddGame_Area(int ID_Game, int ID_Area, int Area_Game_Cost)
        {
            var _context = new db_MinligareevMAContext();
            Game_Area GameArea = new Game_Area();
            GameArea.ID_Game = ID_Game;
            GameArea.ID_Area = ID_Area;
            GameArea.Area_Game_Cost = Area_Game_Cost;
            try
            {
                _context.Game_Area.Add(GameArea);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }
        public void UpdateGame_Area(int ID_Game_Area, int ID_Game, int ID_Area, int Area_Game_Cost)
        {
            using (db_MinligareevMAContext _context = new db_MinligareevMAContext())
            {
                try
                {
                    var GameArea = _context.Game_Area.Where(ga => ga.ID_Game_Area == ID_Game_Area).FirstOrDefault();
                    GameArea.ID_Game = ID_Game;
                    GameArea.ID_Area = ID_Area;
                    GameArea.Area_Game_Cost = Area_Game_Cost;
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Warning");
                    Logging(ex.Message, ex.StackTrace);
                }
            }
        }
        public void DeleteGame_Area(int ID)
        {
            Game_Area GameArea = (from ga in _context.Game_Area where ga.ID_Game_Area == ID select ga).SingleOrDefault();
            try
            {
                _context.Game_Area.Remove(GameArea);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }

        public void AddGame_Exhibition(int ID_Game, int ID_Exhibition, int Game_Rating_On_Exhibition)
        {
            var _context = new db_MinligareevMAContext();
            Game_Exhibition GameExhibition = new Game_Exhibition();
            GameExhibition.ID_Game = ID_Game;
            GameExhibition.ID_Exhibition = ID_Exhibition;
            GameExhibition.Game_Rating_On_Exhibition = Game_Rating_On_Exhibition;
            try
            {
                _context.Game_Exhibition.Add(GameExhibition);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }
        public void UpdateGame_Exhibition(int ID_Game_Exhibition, int ID_Game, int ID_Exhibition, int Game_Rating_On_Exhibition)
        {
            using (db_MinligareevMAContext _context = new db_MinligareevMAContext())
            {
                try
                {
                    var GameExhibition = _context.Game_Exhibition.Where(ge => ge.ID_Game_Exhibition == ID_Game_Exhibition).FirstOrDefault();
                    GameExhibition.ID_Game = ID_Game;
                    GameExhibition.ID_Exhibition = ID_Exhibition;
                    GameExhibition.Game_Rating_On_Exhibition = Game_Rating_On_Exhibition;
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Warning");
                    Logging(ex.Message, ex.StackTrace);
                }
            }
        }

        public void DeleteGame_Exhibition(int ID)
        {
            Game_Exhibition GameExhibition = (from ge in _context.Game_Exhibition where ge.ID_Game_Exhibition == ID select ge).SingleOrDefault();
            try
            {
                _context.Game_Exhibition.Remove(GameExhibition);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
                Logging(ex.Message, ex.StackTrace);
            }
        }
    }
}
#endregion