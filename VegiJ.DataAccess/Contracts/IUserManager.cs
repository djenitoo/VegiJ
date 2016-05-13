namespace VegiJ.DataAccess
{
    using System;
    using System.Linq;

    public interface IUserManager
    {
        // TODO: expand user CRUD
        IQueryable<User> GetUsers();
        User GetUser(Guid id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}