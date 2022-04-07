using System.Collections.Generic;
using aspnet_movies.Database;
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
    public string Title { get; set; }
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
}
