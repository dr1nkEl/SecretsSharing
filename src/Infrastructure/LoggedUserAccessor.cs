using Domain;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;

namespace Infrastructure;

/// Implementation of <inheritdoc cref="ILoggedUserAccessor"/>
public class LoggedUserAccessor : ILoggedUserAccessor
{
    private readonly UserManager<User> userManager;
    private readonly IHttpContextAccessor contextAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="contextAccessor">Http context accessor.</param>
    public LoggedUserAccessor(UserManager<User> userManager, IHttpContextAccessor contextAccessor)
    {
        this.userManager = userManager;
        this.contextAccessor = contextAccessor;
    }

    /// <inheritdoc/>
    public async Task<User> GetMeAsync(CancellationToken cancellationToken)
    {
        var user = contextAccessor.HttpContext.User;
        if (user is null)
        {
            throw new UnauthorizedException(401);
        };
        return await userManager.GetUserAsync(user);
    }
}
