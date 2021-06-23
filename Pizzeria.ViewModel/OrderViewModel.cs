using System.Collections.Generic;

namespace Pizzeria.ViewModel
{
    public class OrderViewModel
    {
        public FoodViewModel Food { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public List<IngredientsViewModel> Ingredients { get; set; }
        public List<ToppingsViewModel> Toppings { get; set; }
        public double GrandTotal { get; set; }
    }

    public class FoodViewModel : CommonViewModel
    {
        public string Image { get; set; }
    }

    public class IngredientsViewModel: CommonViewModel
    {
        public bool IsChecked { get; set; }
    }

    public class ToppingsViewModel : CommonViewModel
    {
        public bool IsChecked { get; set; }
    }

    public class CommonViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}