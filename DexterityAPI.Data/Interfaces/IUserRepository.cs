using System.Collections.Generic;

namespace DexterityAPI.Data.Interfaces
{
    using Domain;

    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User AddUser(User newUser);
        User EditUser(User userToEdit);
        User EnableUser(int userId);
        User DisableUser(int userId);
        int DeleteUser(int userId);
    }
}
