using Microsoft.AspNetCore.Mvc;
using travel.Models;
namespace travel.Controllers.Admin

{
    public class loginController : Controller
    {
        private readonly mydb db;
        private readonly IWebHostEnvironment env;
        public loginController(mydb db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult dashboard()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }
        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Models.User l, IFormFile profile_pic)
        {
            var check = db.Users.Where(a => a.user_email == l.user_email).FirstOrDefault();
            if (check != null)
            {

                TempData["ErrorMsg"] = "Please Write Another Email Because This Email Already Exists";
            }
            else
            {
                string pro = Path.Combine(env.WebRootPath, "profileImage", Path.GetFileName(profile_pic.FileName));
                profile_pic.CopyTo(new FileStream(pro, FileMode.Create));
                l.profile_pic = profile_pic.FileName;

                db.Users.Add(l);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Login");



            }
            return View();
        }
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(Models.User l)
        {
            var query = db.Users.Where(a => a.user_email == l.user_email && a.password == l.password).FirstOrDefault();

            if (query != null)
            {
                // Store user_name and user_id in session
                HttpContext.Session.SetString("username", query.user_name.ToString());
                HttpContext.Session.SetInt32("user_id", query.user_id);  // Storing user_id in session

                if (query.role_type == 1)
                {
                    return RedirectToAction("dashboard", "login");
                }

                if (query.role_type == 0)
                {
                    ViewBag.role_type = 0;
                    ViewBag.user_name = query.user_name; // Passing user name to the view
                    return RedirectToAction("index", "home");
                }
            }
            else
            {
                TempData["invalid"] = "Invalid Email or Password";
            }

            return View();
        }

        public IActionResult logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("index", "home");
        }
    }
}
