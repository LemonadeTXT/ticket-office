using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketOffice.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize]
        public IActionResult GetProfile()
        {
            return View();
        }
    }
}
