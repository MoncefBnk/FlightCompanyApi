using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace FlightCompanyApi.Models
{
    public class BasicAuthenticationAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private const string Realm = "My Realm";

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // If the Authorization header is empty or null
            // then return Unauthorized
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var authenticationHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
            if (authenticationHeaderValue.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
            {
                // Get the authentication token from the request header
                string authenticationToken = authenticationHeaderValue.Parameter;
                // Decode the string
                string decodedAuthenticationToken = Encoding.UTF8.GetString(
                    Convert.FromBase64String(authenticationToken));
                // Convert the string into a string array
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                // First element of the array is the username
                string username = usernamePasswordArray[0];
                // Second element of the array is the password
                string password = usernamePasswordArray[1];
                // call the login method to check the username and password
                if (UserValidate.Login(username, password))
                {
                    var identity = new System.Security.Principal.GenericIdentity(username);
                    var principal = new System.Security.Principal.GenericPrincipal(identity, null);
                    context.HttpContext.User = principal;
                    return;
                }
            }

            context.Result = new UnauthorizedResult();
        }
    }
}


