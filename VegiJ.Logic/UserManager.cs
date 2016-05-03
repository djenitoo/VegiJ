namespace VegiJ.Logic
{
    using System;
    using System.Linq;
    using VegiJ.DataAccess;
    // TODO: throw apporitate exception in cruds
    // TODO: validate for existing username/email and response for successful/unsusecessful operation
    public class UserManager : IUserManager
    {
        private IRepository<User> userRepository;
        
        public UserManager(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            if (this.UsernameExist(user.UserName))
            {
                throw new ArgumentException("Username already exist!");
            }
            if (this.EmailExist(user.Email))
            {
                throw new ArgumentException("Email already exist!");
            }

            userRepository.Create(user);
        }

        public void DeleteUser(User user)
        {
            if (!this.UsernameExist(user.UserName))
            {
                throw new ArgumentException("Username do not exist!");
            }

            userRepository.Delete(user.ID);
        }

        public User GetUser(Guid id)
        {
            var result = userRepository.GetById(id);
            if (result == null)
            {
                throw new ArgumentException("User with given ID does not exist!");
            }

            return result;
        }

        public IQueryable<User> GetUsers()
        {
            return userRepository.Table;
        }

        public void UpdateUser(User user)
        {
            if (!this.UsernameExist(user.UserName))
            {
                throw new ArgumentException("Username do not exist!");
            }

            userRepository.Update(user);
        }

        // TODO: add method for admin to make admins
        // TODO: SecredQuestion & Answer, Email Veritification()? Also some privilegies/roles?

        private bool UsernameExist(string username)
        {
            return userRepository.Table.Any(u => u.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }
        private bool EmailExist(string email)
        {
            return userRepository.Table.Any(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
