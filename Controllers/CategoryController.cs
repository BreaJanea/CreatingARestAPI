using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private IDAL dal;

        public CategoryController(IDAL dalObject)
        {
            dal = dalObject;
        }

        [HttpGet]
        public string[] GetCategories()
        {
            return dal.GetProductsByCategory();
        }
    }
}