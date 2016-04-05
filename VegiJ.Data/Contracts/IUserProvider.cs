namespace VegiJ.Data
{
    using System;
    using System.Linq;

    public interface IUserProvider
    {
        // TODO: expand?
        IQueryable<User> GetUsers();
        User GetUser(Guid id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}