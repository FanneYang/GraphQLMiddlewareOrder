using HotChocolate.Resolvers;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookPost
{

    public class CustomMiddleware<TEntity>
    {
        protected readonly FieldDelegate _next;
        protected readonly ILogger<CustomMiddleware<TEntity>> _logger;

        public CustomMiddleware(FieldDelegate next, ILogger<CustomMiddleware<TEntity>> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            await _next(context).ConfigureAwait(false);
            if (context.Result is IQueryable<TEntity> q && context.Result != null)
            {
                var res = q.ToList();
                _logger.LogInformation($"There are {res.Count()} records");
                context.Result = res;
            }
        }
    }
}
