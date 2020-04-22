using Microsoft.Extensions.Configuration;
using System;
using Dapper;
using MovieAPI.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public class DALSqlServer : IDAL
    {
        private string connectionString;

        public DALSqlServer (IConfiguration config)
        {
            connectionString = config.GetConnectionString("MovieDB");
        }

        public int CreateProduct(Movie m)
        {
            SqlConnection connection = null;
            string queryString = "INSERT INTO MovieList (Title, Category)";
            queryString += " VALUES (@Title, @DCategory)";
            queryString += " SELECT SCOPE_IDENTITY();";
            int newId;

            try
            {
                connection = new SqlConnection(connectionString);
                newId = connection.ExecuteScalar<int>(queryString, m);
            }
            catch (Exception e)
            {
                newId = -1;
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return newId;
        }

        public IEnumerable<Movie> GetMoviesAll()
        {
            SqlConnection connection = null;
            string queryString = "SELECT * FROM MovieList";
            IEnumerable<Movie> MovieList = null;

            try
            {
                connection = new SqlConnection(connectionString);
                MovieList = connection.Query<Movie>(queryString);
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return MovieList;
        }


        public Movie GetMovieById(int id)
        {
            SqlConnection connection = null;
            string queryString = "SELECT * FROM MovieList WHERE Id = @id";
            Movie MovieList = null;

            try
            {
                connection = new SqlConnection(connectionString);
                MovieList = connection.QueryFirstOrDefault<Movie>(queryString, new { id = id });
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return MovieList;
        }


        public string[] GetProductsByCategory()
        {
            SqlConnection connection = null;
            string queryString = "SELECT DISTINCT Category FROM MovieList";
            IEnumerable<Movie> MovieList = null;

            try
            {
                connection = new SqlConnection(connectionString);
                MovieList = connection.Query<Movie>(queryString);
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            if (MovieList == null)
            {
                return null;
            }
            else
            {
                string[] categories = new string[MovieList.Count()];
                int count = 0;

                foreach (Movie m in MovieList)
                {
                    categories[count] = m.Category;
                    count++;
                }

                return categories;
            }

        }

        public IEnumerable<Movie> GetProductsByCategory(string category)
        {
            SqlConnection connection = null;
            string queryString = "SELECT * FROM MovieList WHERE Category = @cat";
            IEnumerable<Movie> MovieList = null;

            try
            {
                connection = new SqlConnection(connectionString);
                MovieList = connection.Query<Movie>(queryString, new { cat = category });
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return MovieList;
        }
    }
}
