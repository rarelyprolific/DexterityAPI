using System.Collections.Generic;

namespace DexterityAPI.Data.Interfaces
{
    using Domain;

    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User AddUser(User newUser);
        int EditUser(User userToEdit);
        int ChangeUserStatus(int userId, bool isEnabled);
        int DeleteUser(User userToDelete);
    }
}
