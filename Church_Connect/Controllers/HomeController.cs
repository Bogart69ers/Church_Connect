using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Church_Connect.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class HomeController : BaseController
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var roles = _croleRepo.GetAllRoles();
            ViewBag.Roles = new SelectList(roles, "RoleName", "RoleName");

            var accounts = _cuserRepo.GetAllAccounts();
            ViewBag.Accounts = accounts;
            
            var model = new User_Account();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(User_Account u)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var dbContext = new ChurchEntities())
                    {
                        var usernameParameter = new SqlParameter("@Username", u.Username);
                        var passwordParameter = new SqlParameter("@Password", u.Password);
                        var roleParameter = new SqlParameter("@Role", u.Role);

                        dbContext.Database.ExecuteSqlCommand("sp_insertUserRole @Username, @Password, @Role", usernameParameter, passwordParameter, roleParameter);
                    }


                    TempData["Msg"] = $"User {u.Username} added as {u.Role}!";
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }

            return View(u);
        }

        public ActionResult Details(int id)
        {
            return View(_cuserRepo.Get(id));
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(User_Account u)
        {
            var user = _cuserRepo.Table.Where(m => m.Username == u.Username).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("", "Username not exist");
                return View();
            }
            if (!user.Password.Equals(u.Password))
            {
                ModelState.AddModelError("", "User not Exist or Incorrect Password");
                return View(u);
            }
            FormsAuthentication.SetAuthCookie(u.Username, false);

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
  
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(_cuserRepo.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(User_Account u)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var dbContext = new ChurchEntities())
                    {
                        var userID = new SqlParameter("@userID", u.UserID);
                        var username = new SqlParameter("@Username", u.Username);
                        var password = new SqlParameter("@Password", u.Password);
                        var role = new SqlParameter("@Role", u.Role);
                        var accountStatus = new SqlParameter("@AccountStatus", u.AccountStatus);

                        dbContext.Database.ExecuteSqlCommand("sp_updateRole @userID, @Username, @Password, @Role, @AccountStatus", userID, username, password, role, accountStatus);
                    }
                    TempData["Msg"] = $"User {u.Username} updated!";

                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }

            return View(u);
        }


        // POST: Home/Delete/{id}
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _cuserRepo.Delete(id); // Accessing _cuserRepo from BaseController
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError("", $"Error: {ex.Message}");
                return RedirectToAction("Create"); // Redirect to the index page
            }
        }
        [Authorize(Roles = "User")]
        public ActionResult Dashboard()
        {
            var schedule = _scheduleType.GetAllSchedule();
            ViewBag.Roles = new SelectList(schedule, "ScheduleName", "ScheduleName");

            var Booking = _Booking.GetAllBooking();
            ViewBag.Accounts = Booking;

            var model = new Parishioner();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult adminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "User, Admin")]
        public ActionResult Profile()
        {
            return View();
        }


    }
}