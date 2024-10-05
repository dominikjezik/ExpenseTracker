using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.API;

[ApiController]
public abstract class ApplicationApiController : ControllerBase
{
    protected Guid GetCurrentUserId()
    {
        var success = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier)!, out var userId);
        
        if (!success)
        {
            throw new Exception("User ID is not a valid GUID.");
        }
        
        return userId;
    }
}
