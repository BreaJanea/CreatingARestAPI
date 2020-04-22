using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieAPI.Models;
using Dapper;

namespace MovieAPI.Services
{
    public interface IDAL
    {
        Movie GetMovieById(int id);
        int CreateProduct(Movie m);
        string[] GetProductsByCategory();
        IEnumerable<Movie> GetProductsByCategory(string category);
        IEnumerable<Movie> GetMoviesAll();
    }
}
