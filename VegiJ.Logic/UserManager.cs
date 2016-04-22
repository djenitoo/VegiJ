namespace VegiJ.Logic
{
    using System;
    using System.Linq;
    using VegiJ.DataAccess;
    // TODO: throw apporitate exception in cruds
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
                return;
            }

            userRepository.Create(user);
        }

        public void DeleteUser(User user)
        {
            if (!this.UserExist(user.UserName))
            {
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
                return;
            }
            userRepository.Update(user);
        }

        // TODO: SecredQuestion & Answer, Email Veritification()? Also some privilegies/roles?

        private bool UserExist(string username)
        {
            return userRepository.Table.Any(u => u.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
