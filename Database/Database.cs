using System;
using MySqlConnector;
using System.Configuration;

namespace aspnet_movies.Database;

public class Database
{
    /// <summary>
    /// Create a connection to the database
    /// </summary>
    /// <returns>
    /// A connection object.
    /// </returns>
    public static MySqlConnection Connection()
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;user=aspnet;password=aspnet@12345678;database=aspnet_movies");
        return conn;
    }
}
