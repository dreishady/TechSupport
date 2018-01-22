/*
 * Author     : Ben Moir
 * Date       : 10/11/2017
 * Student ID : 5101965116
 * Known Bugs : None
 * Summary    : Code for interaction with the database
 */

using System.Data.SqlClient;

namespace Assessment3
{
    public static class Database
    {
        public static SqlConnection Connection { get; }

        static Database()
        {
            var cb = new SqlConnectionStringBuilder
            {
                DataSource = "(LocalDB)\\MSSQLLocalDB",
                AttachDBFilename = "|DataDirectory|\\TechSupport.mdf",
                IntegratedSecurity = true,
            };

            Connection = new SqlConnection(cb.ToString());
            Connection.Open();
        }
    }
}
