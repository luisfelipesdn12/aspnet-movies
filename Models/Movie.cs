using System.Collections.Generic;
using aspnet_movies.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MySqlConnector;

namespace aspnet_movies.Models;

/**
    id int not null auto_increment primary key,
    title varchar(255) not null,
    year int not null,
    director_id int not null,
    genre_id int not null,
    foreign key (director_id) references director(id),
    foreign key (genre_id) references genre(id)
*/
public class Movie
{
    public int Id { get; set; }

    [MaxLength(255)]
    [Display(Name = "Título")]
    [Required(ErrorMessage = "Insira o título do filme")]
    public string Title { get; set; }

    [Display(Name = "Ano")]
    [Required(ErrorMessage = "Insira o ano do filme")]
    public int Year { get; set; }

    public Director Director { get; set; }
    public Genre Genre { get; set; }

    /// <summary>
    /// It gets all the movies from the database and returns them as a list of Movie objects.
    /// </summary>
    /// <returns>
    /// A list of Movie objects.
    /// </returns>
    public static List<Movie> GetAll()
    {
        List<Movie> allMovies = new List<Movie>();
        MySqlConnection conn = Database.Database.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM movie;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int movieId = rdr.GetInt32(0);
            string movieTitle = rdr.GetString(1);
            int movieYear = rdr.GetInt32(2);
            int movieDirectorId = rdr.GetInt32(3);
            int movieGenreId = rdr.GetInt32(4);
            
            Movie newMovie = new Movie();
            newMovie.Id = movieId;
            newMovie.Title = movieTitle;
            newMovie.Year = movieYear;
            newMovie.Director = Director.GetById(movieDirectorId);
            newMovie.Genre = Genre.GetById(movieGenreId);

            allMovies.Add(newMovie);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allMovies;
    }

    public static int Add(Movie movie)
    {
        MySqlConnection conn = Database.Database.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO movie(
            title, year, director_id, genre_id
        ) VALUES (
            @title, @year, @director_id, @genre_id
        );";
        cmd.Parameters.AddWithValue("@title", movie.Title);
        cmd.Parameters.AddWithValue("@year", movie.Year);
        cmd.Parameters.AddWithValue("@director_id", movie.Director.Id);
        cmd.Parameters.AddWithValue("@genre_id", movie.Genre.Id);
        int result = cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return result;
    }
}
