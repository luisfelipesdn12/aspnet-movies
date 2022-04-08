using System.Collections.Generic;
using aspnet_movies.Database;
using System.ComponentModel.DataAnnotations;
using MySqlConnector;

namespace aspnet_movies.Models;

/**
    id int not null auto_increment primary key,
    name varchar(255) not null
*/
public class Genre
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Insira o nome do gênero")]
    [MaxLength(255)]
    [Display(Name = "Nome do gênero")]
    public string Name { get; set; }

    /// <summary>
    /// It gets all the genres from the database and returns them as a list of Genre objects.
    /// </summary>
    /// <returns>
    /// A list of Genre objects.
    /// </returns>
    public static List<Genre> GetAll()
    {
        List<Genre> allGenres = new List<Genre>();
        MySqlConnection conn = Database.Database.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM genre;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int genreId = rdr.GetInt32(0);
            string genreName = rdr.GetString(1);
            Genre newGenre = new Genre{Id = genreId, Name = genreName};
            allGenres.Add(newGenre);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allGenres;
    }

    /// <summary>
    /// It returns a Genre object based on the id passed in.
    /// </summary>
    /// <param name="id">The id of the genre to retrieve.</param>
    /// <returns>
    /// A Genre object.
    /// </returns>
    public static Genre GetById(int id)
    {
        MySqlConnection conn = Database.Database.Connection();
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
            genre = new Genre{Id = genreId, Name = genreName};
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return genre;
    }

    public static int Add(Genre genre)
    {
        MySqlConnection conn = Database.Database.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO genre(name) values(@name);";
        cmd.Parameters.AddWithValue("@name", genre.Name);
        int result = cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return result;
    }
}
