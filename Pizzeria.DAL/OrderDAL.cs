using Pizzeria.Utilities;
using Pizzeria.ViewModel;
using System;
using System.Collections.Generic;

namespace Pizzeria.DAL
{
    public class OrderDAL
    {
        #region Order

        /// <summary>
        /// Save Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int SaveOrder(OrderViewModel order)
        {
            int orderId;
            string fileName = Common.GetUniqueString();
            Common.SaveJsonToFile<OrderViewModel>(Constant.OrderPath, fileName, Constant.TextExtension, order);
            orderId = new Random().Next();
            return orderId;
        }

        /// <summary>
        /// Get size
        /// </summary>
        /// <returns></returns>
        public List<string> GetSize()
        {
            return Common.GetJsonFromFile<List<string>>(Constant.MasterDataPath, Constant.Size, Constant.TextExtension);
        }

        /// <summary>
        /// Get all ingredients
        /// </summary>
        /// <returns></returns>
        public List<IngredientsViewModel> GetAllIngredients()
        {
            return Common.GetJsonFromFile<List<IngredientsViewModel>>(Constant.MasterDataPath, Constant.Ingredients, Constant.TextExtension);
        }

        /// <summary>
        /// Get all toppings
        /// </summary>
        /// <returns></returns>
        public List<ToppingsViewModel> GetAllToppings()
        {
            return Common.GetJsonFromFile<List<ToppingsViewModel>>(Constant.MasterDataPath, Constant.Toppings, Constant.TextExtension);
        }

        #endregion
    }
}