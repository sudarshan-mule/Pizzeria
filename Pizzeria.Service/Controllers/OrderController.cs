using Pizzeria.BL;
using Pizzeria.Repositories;
using Pizzeria.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pizzeria.Service.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        private readonly IOrderBL _orderBL;

        public OrderController(IOrderBL orderBL)
        {
            _orderBL = orderBL;
        }

        /// <summary>
        /// Save Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult SaveOrder(OrderViewModel order)
        {
            int orderId = _orderBL.saveOrder(order);
            return Json(orderId);
        }

        /// <summary>
        /// Get all ingredients
        /// </summary>
        /// <returns></returns>
        [Route("Ingredients")]
        [HttpGet]
        public IHttpActionResult GetAllIngredients()
        {
            var ingredients = _orderBL.GetAllIngredients();
            return Ok(ingredients);
        }

        /// <summary>
        /// Get all toppings
        /// </summary>
        /// <returns></returns>
        [Route("Toppings")]
        [HttpGet]
        public IHttpActionResult GetAllToppings()
        {
            var toppings = _orderBL.GetAllToppings();
            return Ok(toppings);
        }

        /// <summary>
        /// Get all size
        /// </summary>
        /// <returns></returns>
        [Route("Size")]
        [HttpGet]
        public IHttpActionResult GetSize()
        {
            var size = _orderBL.GetSize();
            return Ok(size);
        }
    }
}
