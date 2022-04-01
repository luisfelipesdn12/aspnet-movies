using System.Collections.Generic;
using aspnet_movies.Database;
using MySqlConnector;

namespace aspnet_movies.Models;

/**
    id int not null auto_increment primary key,
    name varchar(255) not null
*/
public class Director
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static List<Director> GetAll()
    {
        List<Director> allDirectors = new List<Director>();
        MySqlConnection conn = Database.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM director;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int directorId = rdr.GetInt32(0);
            string directorName = rdr.GetString(1);
            Director newDirector = new Director(directorName, directorId);
            allDirectors.Add(newDirector);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allDirectors;
    }

    public static Director GetById(int id)
    {
        MySqlConnection conn = Database.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM director WHERE id = @id;";
        cmd.Parameters.AddWithValue("@id", id);
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        Director director = new Director();
        while(rdr.Read())
        {
            int directorId = rdr.GetInt32(0);
            string directorName = rdr.GetString(1);
            director = new Director(directorName, directorId);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return director;
    }
}
