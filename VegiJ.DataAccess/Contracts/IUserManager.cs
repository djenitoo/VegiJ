namespace VegiJ.DataAccess
{
    using System;
    using System.Linq;

    public interface IUserManager
    {
        // TODO: expand?
        IQueryable<User> GetUsers();
        User GetUser(Guid id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}