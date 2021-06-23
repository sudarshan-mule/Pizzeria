using Pizzeria.Utilities;
using Pizzeria.ViewModel;
using System.Collections.Generic;

namespace Pizzeria.DAL
{
    public class HomeDAL
    {
        #region Home

        /// <summary>
        /// Get All Food
        /// </summary>
        /// <returns></returns>
        public List<FoodViewModel> GetAllFood()
        {
            return Common.GetJsonFromFile<List<FoodViewModel>>(Constant.MasterDataPath, Constant.Food, Constant.TextExtension);
        }        

        #endregion
    }
}
