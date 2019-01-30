using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ISET2018_WebServices
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IServiceHEL" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IServiceHEL
    {
        // [OperationContract] define the methods of service contract. 
        [OperationContract] 
        string GetData(int value);

        [OperationContract]
        string HelloWorld(); //l'ajoute à la liste des contrats

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        WS_Personne GetPersonneByID(int NID);

        [OperationContract]
        WS_Personne CheckPersonneMDP(string Nom, string Password);

        [OperationContract]
        WS_CheckISBN CheckISBNNumber(string numberToCheck);

        // TODO: ajoutez vos opérations de service ici
    }


    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    public class WS_Personne
    {
        int _ID;
        string _Nom, _Prenom, _Password;

        [DataMember]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [DataMember]
        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }

        [DataMember]
        public string Prenom
        {
            get { return _Prenom; }
            set { _Prenom = value; }
        }

        [DataMember]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }


    }

    public class WS_CheckISBN
    {
        string _ISBNnum;
        bool _codeValide = false;
        int _checkDigit;

        [DataMember]
        public string ISBNnum
        {
            get { return _ISBNnum; }
            set { _ISBNnum = value; }
        }

        [DataMember]
        public bool codeValide
        {
            get { return _codeValide; }
            set { _codeValide = value; }
        }

        [DataMember]
        public int checkDigit
        {
            get { return _checkDigit; }
            set { _checkDigit = value; }
        }
    }
}
