using Pizzeria.BL;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Pizzeria.Service.Filters
{
    public class AppExceptionLogger : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            await Task.Run(() => new LogServiceBL().LogError(actionExecutedContext.Exception));
        }
    }
}