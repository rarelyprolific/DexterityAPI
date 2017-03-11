using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DexterityAPI.Data
{
    using Interfaces;
    using Domain;

    public class DisconnectedUserRepository : IUserRepository
    {
        private DexterityContext _context;

        public DisconnectedUserRepository(DexterityContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User AddUser(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        public User EditUser(User userToEdit)
        {
            _context.Users.Update(userToEdit);
            _context.SaveChanges();

            return userToEdit;
        }

        public User EnableUser(int userId)
        {
            User userToEnable = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            userToEnable.IsEnabled = true;
            _context.Users.Update(userToEnable);
            _context.SaveChanges();

            return userToEnable;
        }

        public User DisableUser(int userId)
        {
            User userToDisable = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            userToDisable.IsEnabled = false;
            _context.Users.Update(userToDisable);
            _context.SaveChanges();

            return userToDisable;
        }

        public int DeleteUser(int userId)
        {
            User userToDelete = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            _context.Users.Remove(userToDelete);
            return _context.SaveChanges();
        }
    }
}
