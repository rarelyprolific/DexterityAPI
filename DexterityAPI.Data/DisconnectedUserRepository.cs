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

        public int EditUser(User userToEdit)
        {
            _context.Users.Update(userToEdit);
            return _context.SaveChanges();
        }

        public int ChangeUserStatus(int userId, bool isEnabled)
        {
            User user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            user.IsEnabled = isEnabled;
            _context.Users.Update(user);
            return _context.SaveChanges();
        }

        public int DeleteUser(User userToDelete)
        {
            _context.Users.Remove(userToDelete);
            return _context.SaveChanges();
        }
    }
}
