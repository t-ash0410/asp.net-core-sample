using System;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
  public class HealthController : Controller
  {
    public HealthController()
    {
    }

    [HttpGet]
    public IActionResult Check()
    {
      return new JsonResult(new { message = "ok" });
    }
  }
}
