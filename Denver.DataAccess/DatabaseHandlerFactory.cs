using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Denver.DataAccess
{
    public class DatabaseHandlerFactory
    {
        private ConnectionStringSettings connectionStringSettings;

        public DatabaseHandlerFactory(string connectionStringName)
        {
            connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
        }

        public IDatabaseHandler CreateDatabase()
        {
            IDatabaseHandler database = null;

            switch (connectionStringSettings.ProviderName.ToLower())
            {
                case "system.data.sqlclient":
                    database = new SqlDataAccess(connectionStringSettings.ConnectionString);
                    break;                
                case "Npgsql":
                    database = new PostgreSqlDataAccess(connectionStringSettings.ConnectionString);
                    break;               
                default:
                    break;
            }           
            return database;
        }

        public string GetProviderName()
        {
            return connectionStringSettings.ProviderName;
        }

    }
}
