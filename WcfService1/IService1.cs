using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        List<Games> GetGamesEF();

        [OperationContract]
        List<Games> GetSpecialGamesEF(int? id_dev, int? id_pub, double? age);

        [OperationContract]
        List<Developers> GetDevelopersEF();

        [OperationContract]
        List<Publishers> GetPublishersEF();

        [OperationContract]
        List<Developers> GetDevelopersEF_WithoutAll(bool? Active);

        [OperationContract]
        List<Publishers> GetPublishersEF_WithoutAll();

        [OperationContract]
        List<UsersBD> GetUsersTableEF();

        [OperationContract]
        List<UsersBD> GetUsersEF(string Login, string Password);

        [OperationContract]
        List<Game_Area> GetGameAreaEF();

        [OperationContract]
        List<Game_Exhibition> GetGameExhibitionEF();

        [OperationContract]
       void Logging(string Message, string Stack);

        [OperationContract]
        void AddGame(int ID_Developer, int ID_Publisher, string Game_Name, int Year_of_Publication, int Brutal_Rating, string Official_Site, bool Cyber_Discipline, bool Network_Mode);

        [OperationContract]
        void UpdateGame(int ID_Game, int ID_Developer, int ID_Publisher, string Game_Name, int Year_of_Publication, double Brutal_Rating, string Official_Site, bool Cyber_Discipline, bool Network_Mode);

        [OperationContract]
        void DeleteGame(int ID);

        [OperationContract]
        void AddDeveloper(string Developer_Name, string Official_Site, bool Indie);

        [OperationContract]
        void UpdateDeveloper(int ID_Developer, string Developer_Name, string Official_Site, bool Indie);

        [OperationContract]
        void DeleteDeveloper(int ID);

        [OperationContract]
        void AddPublisher(string Publisher_Name, string Official_Site);

        [OperationContract]
        void UpdatePublisher(int ID_Publisher, string Publisher_Name, string Official_Site);

        [OperationContract]
        void DeletePublisher(int ID);

        [OperationContract]
        void AddUser(string First_Name, string Second_Name, string Login, string Password, bool Administrator);

        [OperationContract]
        void UpdateUser(int ID_User, string First_Name, string Second_Name, string Login, string Password, bool Administrator);

        [OperationContract]
        void DeleteUser(int ID);

        [OperationContract]
        void AddGame_Area(int ID_Game, int ID_Area, int Area_Game_Cost);

        [OperationContract]
        void UpdateGame_Area(int ID_Game_Area, int ID_Game, int ID_Area, int Area_Game_Cost);

        [OperationContract]
        void DeleteGame_Area(int ID);

        [OperationContract]
        void AddGame_Exhibition(int ID_Game, int ID_Exhibition, int Game_Rating_On_Exhibition);

        [OperationContract]
        void UpdateGame_Exhibition(int ID_Game_Exhibition, int ID_Game, int ID_Exhibition, int Game_Rating_On_Exhibition);

        [OperationContract]
        void DeleteGame_Exhibition(int ID);

        [OperationContract]
        void AddElectronicScreen(string Talon);

        [OperationContract]
        void SelectAndDeleteTalonFromOCHERED(int id_okna);

        [OperationContract]
        string SelectNaEKRAN(int id_okna);

        [OperationContract]
        void YaSVOBODEN(int id_okna);

        [OperationContract]
        List<Talons> GetTalons();

        [OperationContract]
        void Log(int id_okno, string name_talon);
    


    //// Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    }
}
