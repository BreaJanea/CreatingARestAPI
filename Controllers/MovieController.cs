using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Services;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IDAL dal;

        public MovieController (IDAL dalObject)
        {
            dal = dalObject;
        }

        [HttpPost]
        public Object Post(Movie m)
        {
            int newId = dal.CreateProduct(m);

            if (newId < 0)
            {
                return new { success = false };
            }
            return new { status = true, id = newId };
        }

        [HttpGet]
        public IEnumerable<Movie> Get(string category = null)
        {
            if (category == null)
            {
                IEnumerable<Movie> MovieList = dal.GetMoviesAll();
                return MovieList; //serialize the parameter into JSON and return an Ok (20x)
            }
            else
            {
                IEnumerable<Movie> MovieList = dal.GetProductsByCategory(category);
                return MovieList;
            }
        }
    }
}