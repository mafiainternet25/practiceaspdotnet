using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using PracticeApi.Models;

namespace PracticeApi.Controllers;

public class AuthController : Controller
{
    private readonly PracticeDbContext _db;
    public AuthController(PracticeDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user == null)
        {
            ViewBag.Error = "sai tai khoan hoac mat khau";
            return View();
        }

        HttpContext.Session.SetString("username", user.Username ?? string.Empty);
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Auth");
    }

}

