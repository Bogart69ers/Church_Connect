using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Church_Connect.Controllers
{
    public class BaseController : Controller
    {
        public ChurchEntities _Cdb;
        public BaseRepository<User_Account> _cuserRepo;

        public BaseController()
        {
            _Cdb = new ChurchEntities();
            _cuserRepo = new BaseRepository<User_Account>();
        }
    }
}