using System.Collections.Generic;
using aspnet_movies.Database;
using System.ComponentModel.DataAnnotations;
using MySqlConnector;

namespace aspnet_movies.Models;

/**
    id int not null auto_increment primary key,
    name varchar(255) not null
*/
public class Director
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Insira o nome do diretor")]
    [MaxLength(255)]
    [Display(Name = "Nome do diretor")]
    public string Name { get; set; }

    /// <summary>
    /// It gets all the directors from the database and returns them as a list of Director objects.
    /// </summary>
    /// <returns>
    /// A list of Director objects.
    /// </returns>
    public static List<Director> GetAll()
    {
        List<Director> allDirectors = new List<Director>();
        MySqlConnection conn = Database.Database.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM director;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while (rdr.Read())
        {
            int directorId = rdr.GetInt32(0);
            string directorName = rdr.GetString(1);
            Director newDirector = new Director { Id = directorId, Name = directorName };
            allDirectors.Add(newDirector);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allDirectors;
    }

    /// <summary>
    /// It gets a director by id.
    /// </summary>
    /// <param name="id">The id of the director to retrieve.</param>
    /// <returns>
    /// A Director object.
    /// </returns>
    public static Director GetById(int id)
    {
        MySqlConnection conn = Database.Database.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM director WHERE id = @id;";
        cmd.Parameters.AddWithValue("@id", id);
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        Director director = new Director();
        while (rdr.Read())
        {
            int directorId = rdr.GetInt32(0);
            string directorName = rdr.GetString(1);
            director = new Director { Id = directorId, Name = directorName };
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return director;
    }

    /// <summary>
    /// It adds a new director to the database.
    /// </summary>
    /// <param name="Director">The object that we are adding to the database.</param>
    /// <returns>
    /// Nothing.
    /// </returns>
    public static int Add(Director director)
    {
        MySqlConnection conn = Database.Database.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO director(name) values(@name);";
        cmd.Parameters.AddWithValue("@name", director.Name);
        int result = cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return result;
    }
}
