using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.API;

[ApiController]
public abstract class ApplicationApiController : ControllerBase
{
    protected Guid GetCurrentUserId()
    {
        // TODO: Replace the Guid.TryParse("") with the user's ID.
        //var success = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier)!, out var userId);
        var success = Guid.TryParse("b3beaeef-f0ea-42ea-89f5-fe021e42576e", out var userId);
        
        if (!success)
        {
            throw new Exception("User ID is not a valid GUID.");
        }
        
        return userId;
    }
}
