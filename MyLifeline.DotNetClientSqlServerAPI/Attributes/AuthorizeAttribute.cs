using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public interface IValidateBearerToken
{
    bool Validate(string bearer);
}

public class ValidateBearerToken : IValidateBearerToken
{
    string token = string.Empty;
    public ValidateBearerToken() 
    {
        token = Environment.GetEnvironmentVariable("MLLTOKEN");
    }
    public bool Validate(string bearer) => (bearer.Equals($"Bearer {token}"));
}

public class AuthorizeAttribute : TypeFilterAttribute
{
    public AuthorizeAttribute(): base(typeof(AuthorizeActionFilter)){}
}

public class AuthorizeActionFilter : IAsyncActionFilter
{
    private readonly IValidateBearerToken _authToken;
    public AuthorizeActionFilter(IValidateBearerToken authToken)
    {
        _authToken = authToken;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        const string AUTHKEY = "authorization";
        var headers = context.HttpContext.Request.Headers;
        if (headers.ContainsKey(AUTHKEY))
        {
            bool isAuthorized = _authToken.Validate(headers[AUTHKEY]);
            if (!isAuthorized)
                context.Result = new UnauthorizedResult();
            else
                await next();
        }
        else
            context.Result = new UnauthorizedResult();
    }
}