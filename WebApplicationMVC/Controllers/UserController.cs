using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationMVC.Models;
using X.PagedList;

namespace WebApplicationMVC.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext _db;

        public UserController(UserDbContext db)
        {
            _db = db;
        }

        // [HttpGet]
        public IActionResult Index(int pageNumber = 1)
        {
            int pageSize = 4;
            var onePageOfUsers = _db.Users.ToPagedList(pageNumber, pageSize);
            // ViewData["Users"] = _db.Users.ToList();
            return View(onePageOfUsers);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([Bind("Name,BirthDate")] User user)
        {
            // user.Id = 1;
            if (ModelState.IsValid)
            {
                await _db.Users.AddAsync(user);
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
        public async Task<IActionResult> Edit([Bind("Id,Name,BirthDate")] User user)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Update(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = await _db.Users.FirstOrDefaultAsync(m => m.Id == id);
            ViewData["DeleteUser"] = user;
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteId(int id)
        {
            User? user = _db.Users.FirstOrDefault(x => x.Id == id);
            if (ModelState.IsValid)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _db.Users.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
    }
}
