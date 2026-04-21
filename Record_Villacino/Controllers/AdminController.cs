using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Record_Villacino.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller {
  public IActionResult Dashboard() {
    return View();
  }
}
