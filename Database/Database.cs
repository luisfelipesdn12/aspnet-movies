using System;
using MySqlConnector;

namespace aspnet_movies.Database;

public class Database
{
    public static MySqlConnection Connection()
    {
        MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
        return conn;
    }
}
