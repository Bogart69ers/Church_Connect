using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Church_Connect;

namespace Church_Connect
{
    public class BaseRepository<T> :IBaseRepository<T>
           where T : class
    {
        private DbContext _cdb;
        public DbSet<T> Table;

        public BaseRepository()
        {
            _cdb = new ChurchEntities();
            Table = _cdb.Set<T>();
        }
        public T Get(object id)
        {
            return Table.Find(id);
        }

        public List<T> GetAll()
        {
            return Table.ToList();
        }
        public List<Role> GetAllRoles()
        {
            return Table.Cast<Role>().ToList();
        }
        public List<ScheduleType> GetAllSchedule()
        {
            return Table.Cast<ScheduleType>().ToList();
        }
        public List<Booking> GetAllBooking()
        {
            return Table.Cast<Booking>().ToList();
        }
        public List<User_Account> GetAllAccounts()
        {
            return Table.Cast<User_Account>().ToList();
        }

        public ErrorCode Create(T t)
        {
            try
            {
                Table.Add(t);
                _cdb.SaveChanges();
                return ErrorCode.Success;
            }
            catch (Exception ex)
            {

                return ErrorCode.Error;
            }
        }

        public ErrorCode Delete(object id)
        {
            try
            {
                var obj = Get(id);
                Table.Remove(obj);
                _cdb.SaveChanges();

                return ErrorCode.Success;
            }
            catch (Exception ex)
            {

                return ErrorCode.Error;
            }
        }

        public ErrorCode Update(object id, T t)
        {
            try
            {
                var oldObj = Get(id);
                _cdb.Entry(oldObj).CurrentValues.SetValues(t);
                _cdb.SaveChanges();

                return ErrorCode.Success;
            }
            catch (Exception ex)
            {

                return ErrorCode.Error;
            }
        }        

    }
}