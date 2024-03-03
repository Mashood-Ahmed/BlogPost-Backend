using Microsoft.AspNetCore.Http.HttpResults;
using Portfolio.API.Repository.Interface;

namespace Portfolio.API.Authorization
{
    //Extracts the JWT token from the request and validate it.
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);

            if (userId != null)
            {
                //attach user to context if validation success
                context.Items["User"] = await userRepository.GetUserAsync(userId.Value);
            }

            await _next(context);
        }
    }
}
