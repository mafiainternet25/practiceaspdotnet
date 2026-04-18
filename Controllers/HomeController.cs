using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PracticeApi.Models;


namespace PracticeApi.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly PracticeDbContext _db;

    public HomeController(PracticeDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var users = _db.Users.ToList();
        return View(users);
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        _db.Users.Add(user);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(long? id)
    {
        var user = _db.Users.Find(id);
        if (user != null)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Edit(long? id)
    {
        var user = _db.Users.Find(id);
        return View(user);
    }

    [HttpPost]
    public IActionResult Update(User user)
    {
        _db.Users.Update(user);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}