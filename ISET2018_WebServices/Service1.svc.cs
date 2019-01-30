using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;

namespace ISET2018_WebServices
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "ServiceHEL" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez ServiceHEL.svc ou ServiceHEL.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class ServiceHEL : IServiceHEL //Aller dans Editer, Refactoriser, puis Renommer
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string HelloWorld()
        {
            return "Bonjour le monde !";
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public WS_Personne GetPersonneByID(int NID)
        {
            string chConn = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/WS_Data.mdf") + ";Integrated Security=True;Connect Timeout=30";
            WS_Personne p = new WS_Personne();
            SqlConnection Con = new SqlConnection(chConn);
            Con.Open();
            SqlCommand Com = new SqlCommand("SELECT Nom, Pre FROM WS_Personne WHERE ID=" + NID.ToString(), Con);
            SqlDataReader dr = Com.ExecuteReader();
            if (dr.Read())
            {
                p.ID = NID;
                p.Nom = dr["Nom"].ToString();
                p.Prenom = dr["Pre"].ToString();
            }
            return p;
        }

        public WS_Personne CheckPersonneMDP(string Nom, string Password)
        {
            string chConn = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/WS_Data.mdf") + ";Integrated Security=True;Connect Timeout=30";
            WS_Personne p = new WS_Personne();
            SqlConnection Con = new SqlConnection(chConn);
            Con.Open();
            SqlCommand Com = new SqlCommand("SELECT * FROM WS_Personne WHERE Nom='" + Nom + "' AND Password='" + Password + "'", Con);
            SqlDataReader dr = Com.ExecuteReader();
            if (dr.Read())
            {
                p.Nom = dr["Nom"].ToString();
                p.Password = dr["Password"].ToString();
                p.Prenom = dr["Pre"].ToString();
            }
            return p;
        }


        public WS_CheckISBN CheckISBNNumber(string numberToCheck)
        {
            int calculISBN;
            WS_CheckISBN p = new WS_CheckISBN();

            int[] ISBN = new int[numberToCheck.Length];

            for (int i = 0; i < numberToCheck.Length; i++)
                ISBN[i] = Int32.Parse(numberToCheck.Substring(i, 1));

            calculISBN = 10 - ((ISBN[0] * 1 + ISBN[1] * 3 + ISBN[2] * 1 + ISBN[3] * 3 + ISBN[4] * 1 + ISBN[5] * 3 + ISBN[6] * 1 + ISBN[7] * 3 + ISBN[8] * 1 + ISBN[9] * 3 + ISBN[10] * 1 + ISBN[11] * 3) % 10);

            if (calculISBN == ISBN[12])
            {
                p.codeValide = true;
                p.checkDigit = calculISBN;
            }
            else
            {
                p.codeValide = false;
                p.checkDigit = -1; //donc erreur
            }

            return p;
        }
    }
}
