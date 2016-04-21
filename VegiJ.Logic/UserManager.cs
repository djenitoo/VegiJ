namespace VegiJ.Logic
{
    using System;
    using System.Linq;
    using VegiJ.DataAccess;

    public class UserManager : IUserManager
    {
        private IRepository<User> userRepository;
        
        public UserManager(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            if (this.UserExist(user.UserName))
            {
                // TODO: throw apporitate exception in crud
                return;
            }

            userRepository.Create(user);
        }

        public void DeleteUser(User user)
        {
            if (!this.UserExist(user.UserName))
            {
                // TODO: throw apporitate exception in crud
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
                // TODO: throw apporitate exception in crud
                return;
            }
            userRepository.Update(user);
        }

        // TODO: GetLastLoginDate(), GetRegistrationDate() etc. SecredQuestion & Answer, Email Veritification() also some privilegies/roles?

        private bool UserExist(string username)
        {
            return userRepository.Table.Any(u => u.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
