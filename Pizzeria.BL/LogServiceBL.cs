using Pizzeria.DAL;
using System;

namespace Pizzeria.BL
{
    public class LogServiceBL
    {
        public void LogError(Exception ex)
        {
            new LogServiceDAL().LogError(ex);
        }
    }
}
