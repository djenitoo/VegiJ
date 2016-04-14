namespace VegiJ.Logic
{
    using System;
    using System.Linq;
    using VegiJ.DataAccess;

    public class UserManager : IUserProvider
    {
        private IRepository<User> userRepository;

        // TODO: make db's with dictionary and keys
        // Dictionaty(<TKey><TDictionary>) - TKey like User, TDictionary - another dictionary;
        public UserManager(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            if (this.UserExist(user.UserName))
            {
                // TODO: throw exception
                return;
            }

            userRepository.Create(user);
        }

        public void DeleteUser(User user)
        {
            if (!this.UserExist(user.UserName))
            {
                // TODO: throw exception
                return;
            }

            userRepository.Delete(user.ID);
        }

        public User GetUser(Guid id)
        {
            return userRepository.GetById(id);
        }

        public IQueryable<User> GetUsers()
        {
            return userRepository.Table;
        }

        public void UpdateUser(User user)
        {
            if (!this.UserExist(user.UserName))
            {
                // TODO: throw exception
                return;
            }
            user.ModifiedDate = DateTime.Now;
            userRepository.Update(user);
        }

        // TODO: ? GetLastLoginDate(), GetRegistrationDate() etc. SecredQuestion & Answer, Email Veritification() also some privilegies/roles?

        private bool UserExist(string username)
        {
            return userRepository.Table.Any(u => u.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
