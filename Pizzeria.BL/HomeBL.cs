using Pizzeria.DAL;
using Pizzeria.Repositories;
using Pizzeria.ViewModel;
using System.Collections.Generic;

namespace Pizzeria.BL
{
    public class HomeBL : IHomeBL
    {
        /// <summary>
        /// Get all Food
        /// </summary>
        /// <returns></returns>
        public List<FoodViewModel> GetAllFood()
        {
            return new HomeDAL().GetAllFood();
        }
    }
}
