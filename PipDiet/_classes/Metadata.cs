using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PipDiet
{

    public class Metadata
    {
        static public string CurrentConnectionString = ConnectionString.defaultString;
        static public bool CurrentAppRole;
        static public int CurrentUserId;
        //static public SqlConnection conn = new SqlConnection(CurrentConnectionString);
        public class ConnectionString
        {
            static public string defaultString = "Data Source=DESKTOP-B9P5U74\\SQLEXPRESS;Initial Catalog=PipDietDB;Integrated Security=true;";
            static public string user = "Data Source=DESKTOP-B9P5U74\\SQLEXPRESS;Initial Catalog=PipDietDB;Integrated Security = False; User ID = userdb; Password=userdb;ApplicationIntent=ReadWrite;";
            static public string admin = "Data Source=DESKTOP-B9P5U74\\SQLEXPRESS;Initial Catalog=PipDietDB;Integrated Security=False;User ID=admindb;Password=admindb;ApplicationIntent=ReadWrite;";
        }
        public enum AuthRoles
        {
            ADMIN = 2,
            USER = 1,
            ANON = 0
        }

        public static string Hash(string input) //готово
        {
            byte[] hash = Encoding.ASCII.GetBytes(input);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string output = "";
            foreach (var b in hashenc)
            {
                output += b.ToString("x2");
            }
            return output;
        }

    }
}
