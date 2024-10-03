using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API;

[ApiController]
public abstract class ApplicationApiController : ControllerBase
{
    protected Guid GetCurrentUserId()
    {
        // TODO: Replace the Guid.TryParse("") with the user's ID.
        //var success = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier)!, out var userId);
        var success = Guid.TryParse("86f45ed1-fed9-4fef-bdc7-a8b7596a88bb", out var userId);
        
        if (!success)
        {
            throw new Exception("User ID is not a valid GUID.");
        }
        
        return userId;
    }
}
