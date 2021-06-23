using Pizzeria.ViewModel;
using System.Collections.Generic;

namespace Pizzeria.Repositories
{
    public interface IOrderBL
    {
        int saveOrder(OrderViewModel order);
        List<string> GetSize();
        List<IngredientsViewModel> GetAllIngredients();
        List<ToppingsViewModel> GetAllToppings();
    }
}
