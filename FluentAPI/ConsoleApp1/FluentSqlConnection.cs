using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    //Implement all interfaces.
    public class FluentSqlConnection :
        IServerSelectionStage,
        IDatabaseSelectionStage,
        IUserSelectionStage,
        IPasswordSelectionStage,
        IConnectionInitializerStage
    {
        private string _server;
        private string _database;
        private string _user;
        private string _password;

        private FluentSqlConnection() { }

        //Example of lambda
        public static IServerSelectionStage CreateConnection(Action<ConnectionConfiguration> config)
        {
            var configuration = new ConnectionConfiguration();
            //This will populate the configuration variable above.
            config?.Invoke(configuration);
            return new FluentSqlConnection();
        }

        //Implement it just like a Builder pattern.
        public IDatabaseSelectionStage ForServer(string server)
        {
            _server = server;
            return this;
        }

        public IUserSelectionStage AndDatabase(string database)
        {
            _database = database;
            return this;
        }

        public IPasswordSelectionStage AsUser(string user)
        {
            _user = user;
            return this;
        }

        public IConnectionInitializerStage WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public IDbConnection Connect()
        {
            var connection = new SqlConnection($"Server={_server};Database={_database};User Id={_user};Password={_password}");
            connection.Open();
            return connection;
        }
    }

    public class ConnectionConfiguration
    {
        public int TimeoutInSeconds { get; set; }
    }

    #region Interfaces
    public interface IServerSelectionStage
    {
        //Always return the next chain method that you want to be called after the current one.
        public IDatabaseSelectionStage ForServer(string server);
    }

    public interface IDatabaseSelectionStage
    {
        public IUserSelectionStage AndDatabase(string database);
    }

    public interface IUserSelectionStage
    {
        public IPasswordSelectionStage AsUser(string username);
    }

    public interface IPasswordSelectionStage
    {
        public IConnectionInitializerStage WithPassword(string password);
    }

    public interface IConnectionInitializerStage
    {
        public IDbConnection Connect();
    }
    #endregion
}
