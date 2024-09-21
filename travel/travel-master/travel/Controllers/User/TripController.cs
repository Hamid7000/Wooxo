using Microsoft.AspNetCore.Mvc;
using travel.Models;

namespace travel.Controllers.User
{


    public class TripController : Controller


    {
        private readonly mydb db;
        public TripController(mydb _db)
        {
            this.db = _db;
        }
        public IActionResult Trip()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Trip(trip t)
        {
            // Retrieve user_id from session
            int? userId = HttpContext.Session.GetInt32("user_id");

            if (userId == null)
            {
                // Optionally redirect to login if the user is not logged in
                return RedirectToAction("Login", "Login");
            }

            
            
                // Set the UserId in the trip object
                t.UserId = userId.Value;

                // Add the trip to the database and save changes
                db.Trips.Add(t);
                db.SaveChanges();

                // Redirect to a success page (or back to the home page)
                return RedirectToAction("Index", "Home");
            
           
        }

        public IActionResult UserTrips()
        {
            var a = db.Trips.ToList();
            return View(a);
        }


        public IActionResult Edit(int id)
        {
            var b = db.Trips.Find(id);
            return View(b);
        }
        [HttpPost]
        public IActionResult Edit(trip d, int id)
        {
            var dbrow = db.Trips.Find(id);

            dbrow.Destination = d.Destination;
            dbrow.StartDate = d.StartDate;
            dbrow.EndDate = d.EndDate;
            dbrow.TotalBudget = d.TotalBudget;
            dbrow.RemainingBudget = d.RemainingBudget;
            db.SaveChanges();
            return RedirectToAction("UserTrips");
        }

        public IActionResult delete(int id)
        {
            var row = db.Trips.Find(id);
            if (row != null)
            {
                db.Trips.Remove(row);
                db.SaveChanges();
            }
            return RedirectToAction("UserTrips");
        }



    }
}
