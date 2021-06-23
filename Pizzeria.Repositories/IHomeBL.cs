using Pizzeria.ViewModel;
using System.Collections.Generic;

namespace Pizzeria.Repositories
{
    public interface IHomeBL
    {
        List<FoodViewModel> GetAllFood();
    }
}
