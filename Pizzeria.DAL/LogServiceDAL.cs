using Pizzeria.Utilities;
using System;

namespace Pizzeria.DAL
{
    public class LogServiceDAL
    {
        #region Error

        public void LogError(Exception ex)
        {
            try
            {
                string fileName = Common.GetUniqueString();
                Common.SaveJsonToFile<Exception>(Constant.ErrorPath, fileName, Constant.TextExtension, ex);
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion
    }
}
