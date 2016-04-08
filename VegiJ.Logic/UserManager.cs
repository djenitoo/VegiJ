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
            userRepository.Create(user);
        }

        public void DeleteUser(User user)
        {
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
            userRepository.Update(user);
        }

        // TODO: ? GetLastLoginDate(), GetRegistrationDate(),
        // SecredQuestion & Answer, Email Veritification() also some privilegies/roles?

        // TODO: Create NInject class
    }
}
