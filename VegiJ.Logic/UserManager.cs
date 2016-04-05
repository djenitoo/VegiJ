namespace VegiJ.Logic
{
    using System;
    using System.Linq;
    using VegiJ.DataAccess;
    using VegiJ.DataAccess.Contracts;

    public class UserManager : IUserProvider
    {
        private IRepository<User> userRepository;

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
            return userRepository.GetByID(id);
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
    }
}
