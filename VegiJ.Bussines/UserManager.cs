namespace VegiJ.Business
{
    using System;
    using System.Linq;
    using VegiJ.Data;

    public class UserManager : IUserProvider
    {
        private User user;

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }


        // TODO: GetUser(), AddUser(), DeleteUser(), UpdateUser(), GetLastLoginDate(), GetRegistrationDate(),
        // SecredQuestion & Answer, Email Veritification() also some privilegies/roles?
        // implement UserStore??

    }
}
