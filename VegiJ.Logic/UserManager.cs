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

        public void CreateUser(User user)
        {
            if (this.UsernameExist(user.UserName) || UserIDExist(user.ID))
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
            if (!this.UsernameExist(user.UserName) || !UserIDExist(user.ID))
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
            if (!this.UsernameExist(user.UserName) || !UserIDExist(user.ID))
            {
                throw new ArgumentException("Username do not exist!");
            }
            //if (!PasswordHash.ComparePasswords(user.Password, user.Salt, userRepository.GetById(user.ID).Password))
            //{
            //    user.Password = PasswordHash.EncryptPassword(user.Password, user.Salt);
            //}
            userRepository.Update(user);
        }
        
        // TODO: add method for admin to make admins
        // TODO: SecredQuestion & Answer, Email Veritification()

        private bool UsernameExist(string username)
        {
            return userRepository.Table.AsEnumerable().Any(u => u.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }
        private bool EmailExist(string email)
        {
            return userRepository.Table.Any(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }
        private bool UserIDExist(Guid id)
        {
            return userRepository.GetById(id) != null;
        }
    }
}
