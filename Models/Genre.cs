using System.Collections.Generic;
using aspnet_movies.Database;
using MySqlConnector;

namespace aspnet_movies.Models;

/**
    id int not null auto_increment primary key,
    name varchar(255) not null
*/
public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static List<Genre> GetAll()
    {
        List<Genre> allGenres = new List<Genre>();
        MySqlConnection conn = Database.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM genre;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int genreId = rdr.GetInt32(0);
            string genreName = rdr.GetString(1);
            Genre newGenre = new Genre(genreName, genreId);
            allGenres.Add(newGenre);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allGenres;
    }

    public static Genre GetById()
    {
        MySqlConnection conn = Database.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM genre WHERE id = @id;";
        cmd.Parameters.AddWithValue("@id", id);
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        Genre genre = new Genre();
        while(rdr.Read())
        {
            int genreId = rdr.GetInt32(0);
            string genreName = rdr.GetString(1);
            genre = new Genre(genreName, genreId);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return genre;
    }
}
