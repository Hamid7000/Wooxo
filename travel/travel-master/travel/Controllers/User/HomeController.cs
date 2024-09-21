using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using travel.Models;

namespace travel.Controllers.User
{
    public class HomeController : Controller
    {
        private readonly mydb db;
        public HomeController(mydb _db)
        {
            this.db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditProfile(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult EditProfile(int user_id, string user_name, string user_email)
        {
            var user = db.Users.Find(user_id);
            if (user == null)
            {
                return NotFound();
            }

            // Update only the fields that should be editable
            user.user_name = user_name;
            user.user_email = user_email;

            db.Users.Update(user);
            db.SaveChanges();

            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult ChangePassword(string currentPassword, string newPassword, string renewPassword)
        {
            // Retrieve the user ID from the session
            var userId = HttpContext.Session.GetInt32("user_id");

            // Ensure the user ID is available
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            // Fetch the user from the database
            var user = db.Users.FirstOrDefault(u => u.user_id == userId.Value);
            if (user == null)
            {
                return NotFound(); // Handle user not found
            }

            // Initialize a dictionary to hold error messages
            var errors = new Dictionary<string, string>();

            // Check if the current password is correct
            if (user.password != currentPassword)
            {
                errors["currentPassword"] = "Current password is incorrect.";
            }

            // Check if the new passwords match
            if (newPassword != renewPassword)
            {
                errors["renewPassword"] = "New passwords do not match.";
            }

            // Return errors if any exist
            if (errors.Any())
            {
                return BadRequest(errors);
            }

            // Update the user's password
            user.password = newPassword;

            db.Users.Update(user);
            db.SaveChanges();

            return Json(new { success = true });
        }

    }
}
