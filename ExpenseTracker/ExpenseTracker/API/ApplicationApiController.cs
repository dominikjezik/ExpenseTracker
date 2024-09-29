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
        var success = Guid.TryParse("d41b8cf4-766d-4192-9860-08752a296bfc", out var userId);
        
        if (!success)
        {
            throw new Exception("User ID is not a valid GUID.");
        }
        
        return userId;
    }
}
