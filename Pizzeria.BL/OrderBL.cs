using Pizzeria.DAL;
using Pizzeria.Repositories;
using Pizzeria.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pizzeria.BL
{
    public class OrderBL : IOrderBL
    {
        /// <summary>
        /// Save order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int saveOrder(OrderViewModel order)
        {
            ValidateOrder(order);
            CalculateGrandTotal(order);
            return new OrderDAL().SaveOrder(order);
        }

        /// <summary>
        /// Get all ingredients
        /// </summary>
        /// <returns></returns>
        public List<IngredientsViewModel> GetAllIngredients()
        {
            return new OrderDAL().GetAllIngredients();
        }

        /// <summary>
        /// Get all toppings
        /// </summary>
        /// <returns></returns>
        public List<ToppingsViewModel> GetAllToppings()
        {
            return new OrderDAL().GetAllToppings();
        }

        /// <summary>
        /// Get size
        /// </summary>
        /// <returns></returns>
        public List<string> GetSize()
        {
            return new OrderDAL().GetSize();
        }

        /// <summary>
        /// Validate order
        /// </summary>
        /// <param name="order"></param>
        private void ValidateOrder(OrderViewModel order)
        {
            string message = string.Empty;

            if (order == null)
            {
                message = "Order details not found.";
            }
            else if (order.Food == null || string.IsNullOrWhiteSpace(order.Food.Name) || order.Food.Price <= 0)
            {
                message = "Please select Food.";
            }
            else if (order.Quantity <= 0)
            {
                message = "Please select Pizza Quantity.";
            }
            else if (string.IsNullOrWhiteSpace(order.Size))
            {
                message = "Please select Pizza Size.";
            }
            else if (order.Ingredients == null || order.Ingredients.Count <= 0 || order.Ingredients.Any(x => string.IsNullOrWhiteSpace(x.Name)) ||
                order.Ingredients.Any(x => x.Price <= 0))
            {
                message = "Please add ingredients to your Pizza.";
            }
            else if (order.Toppings == null || order.Toppings.Count <= 0 || order.Toppings.Any(x => string.IsNullOrWhiteSpace(x.Name)) ||
                order.Toppings.Any(x => x.Price <= 0))
            {
                message = "Please add toppings to your Pizza.";
            }
            else if (order.GrandTotal <= 0)
            {
                message = "Unable to calculate order amount.";
            }

            if (!string.IsNullOrWhiteSpace(message))
                throw new Exception(message);
        }

        /// <summary>
        /// Calculate order amount
        /// </summary>
        /// <param name="order"></param>
        private void CalculateGrandTotal(OrderViewModel order)
        {
            double sizePrice = GetPriceBySize(order.Size, order.Food.Price);
            order.GrandTotal = order.Quantity * (sizePrice + order.Ingredients.Sum(x => x.Price) + order.Toppings.Sum(x => x.Price));
        }

        /// <summary>
        /// Calculate price by size
        /// </summary>
        /// <param name="size"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        private double GetPriceBySize(string size, double price)
        {
            switch (size.ToLower())
            {
                case "small": return price * 1;
                case "medium": return price * 1.5;
                case "large": return price * 2;
                default: return price;
            }
        }

    }
}
