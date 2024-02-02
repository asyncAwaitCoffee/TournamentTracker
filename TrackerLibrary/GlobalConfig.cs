using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection;

        public static void InitializeConnections(DatabaseType db)
        {
            if (db == DatabaseType.SQL)
            {
                //TODO - SQL connections
                SqlConnector sql = new();
                Connection = sql;
            }

            else if (db == DatabaseType.TextFile)
            {
                //TODO - text file connections
                TextConnector textConnection = new();
                Connection = textConnection;
            }
        }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
