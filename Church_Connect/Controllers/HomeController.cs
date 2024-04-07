using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var roles = _croleRepo.GetAllRoles();
            ViewBag.Roles = new SelectList(roles, "RoleName", "RoleName");

            var model = new User_Account();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(User_Account u, string selectedRole, UserRole r)
        {
            if (ModelState.IsValid)
            {
                int roleId;

                if (selectedRole == "User")
                {
                    roleId = 1; 
                }
                else if (selectedRole == "Admin")
                {
                    roleId = 2; 
                }
                else
                {
                    return RedirectToAction("Error"); 
                }

                
                r.roleId = roleId;

                
                _curoleRepo.Create(r);

               
                u.RoleId = roleId;

                _cuserRepo.Create(u);

                return RedirectToAction("Index");
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
    }
}