using Pizzeria.Repositories;
using System.Web.Http;

namespace Pizzeria.Service.Controllers
{
    [RoutePrefix("api/Home")]
    public class HomeController : ApiController
    {
        private readonly IHomeBL _homeBL;

        public HomeController(IHomeBL homeBL)
        {
            _homeBL = homeBL;
        }

        /// <summary>
        /// Get All Food
        /// </summary>
        /// <returns></returns>
        [Route("Food")]
        [HttpGet]
        public IHttpActionResult GetAllFood()
        {
            var food = _homeBL.GetAllFood();
            return Ok(food);
        }
    }
}
