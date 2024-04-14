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
        public BaseRepository<Role> _croleRepo;
        public BaseRepository<UserRole> _curoleRepo;
        public BaseRepository<ScheduleType> _scheduleType;
        public BaseRepository<Booking> _Booking;



        public BaseController()
        {
            _Cdb = new ChurchEntities();
            _cuserRepo = new BaseRepository<User_Account>();
            _croleRepo = new BaseRepository<Role>();
            _curoleRepo = new BaseRepository<UserRole>();
            _scheduleType = new BaseRepository<ScheduleType>();
            _Booking = new BaseRepository<Booking>();
        }
    }
}