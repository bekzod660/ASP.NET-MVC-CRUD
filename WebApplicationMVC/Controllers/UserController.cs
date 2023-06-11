using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext _db;

        //public UserController(UserDbContext db)
        //{
        //    _db = db;
        //}

        // [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,BirthDate")] User user)
        {
            if (ModelState.IsValid)
            {
                await _db.AddAsync(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    //  ViewData["Users"] = _db.Users.ToList();
        //    return View(await _db.Users.ToListAsync());
        //}

        [HttpPost]
        public async Task<IActionResult> Update([Bind("Id,Name,BirthDate")] User user)
        {
            if (ModelState.IsValid)
            {
                _db.Update(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == id);
            if (ModelState.IsValid)
            {
                _db.Remove(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
