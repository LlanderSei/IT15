using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Record_Villacino.Areas.Identity.Pages.Account;

public class ForgotPasswordModel : PageModel {
  private readonly UserManager<IdentityUser> _userManager;

  public ForgotPasswordModel(UserManager<IdentityUser> userManager) {
    _userManager = userManager;
  }

  [BindProperty]
  public InputModel Input { get; set; } = new();

  public class InputModel {
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
  }

  public void OnGet() {
  }

  public async Task<IActionResult> OnPostAsync() {
    if (!ModelState.IsValid) {
      return Page();
    }

    var user = await _userManager.FindByEmailAsync(Input.Email);
    if (user == null) {
      return RedirectToPage("./ForgotPasswordConfirmation");
    }

    return RedirectToPage("./ForgotPasswordConfirmation");
  }
}
