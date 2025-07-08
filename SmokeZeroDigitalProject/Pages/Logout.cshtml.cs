public class LogoutModel : PageModel
{
    public IActionResult OnPost()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("/Index");
    }
}
