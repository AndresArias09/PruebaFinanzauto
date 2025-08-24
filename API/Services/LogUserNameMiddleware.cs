using Serilog.Context;

namespace EjemploApi.API.Services
{
    public class LogUserNameMiddleware
    {
        private readonly RequestDelegate next;

        public LogUserNameMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            if(context.User.Identity is not null)
            {
                LogContext.PushProperty("IsAuthenticated", context.User.Identity.IsAuthenticated);

                if (context.User.Identity.IsAuthenticated)
                {
                    LogContext.PushProperty("UserName", context.User.Identity.Name);
                }
                else
                {
                    LogContext.PushProperty("UserName", "-");
                }
            }

            return next(context);
        }
    }
}
