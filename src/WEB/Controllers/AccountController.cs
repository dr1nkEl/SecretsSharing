using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB.Infrastructure.Models;

namespace WEB.Controllers;

/// <summary>
/// Account API controller.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="signInManager">Sign in manager.</param>
    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    /// <summary>
    /// POST register action.
    /// </summary>
    /// <param name="credentials">Credentials.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> Register([FromQuery] UserCredentials credentials, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = credentials.Email,
            Email = credentials.Email,
        };
        var registerResult = await userManager.CreateAsync(user, credentials.Password);

        if (!registerResult.Succeeded)
        {
            return BadRequest(registerResult.Errors);
        }

        return Ok("Registered.");
    }

    /// <summary>
    /// POST authorize action.
    /// </summary>
    /// <param name="credentials">Credentials.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> Authorize([FromForm]UserCredentials credentials, CancellationToken cancellationToken)
    {
        var signInResult = await signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, isPersistent: false, lockoutOnFailure: false);
        if (!signInResult.Succeeded)
        {
            return BadRequest("Failed to authorize. Check your credentials.");
        }
        return Ok("Authorized.");
    }

    /// <summary>
    /// POST logout action.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [Authorize]
    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult> Logout(CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();
        return Ok("Logged out.");
    }
}