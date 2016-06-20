using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VegiJ.Web.MVC.Areas.Administration.Models
{
    using System.Data.Entity;
    using DataAccess;

    public class UserServices
    {
        public IUserManager UserManager { get; set; }

        public UserServices(IUserManager uManager)
        {
            this.UserManager = uManager;
        }

        public IEnumerable<UserItemViewModel> Read()
        {
            return UserManager.GetUsers().Select(user => new UserItemViewModel
            {
                ID = user.ID,
                UserName = user.UserName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                FirstName = user.FirstName,
                Password = "",
                ConfirmPassword = "",
                GenderID = user.GenderID,
                Gender = user.Gender.Name,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin
            });
        }

        public void Create(UserItemViewModel model)
        {
            var entity = new User(model.UserName, model.Password, model.Email, model.BirthDate.ToString(), model.GenderID.ToString());

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.IsAdmin = model.IsAdmin;

            if (entity.Gender != null)
            {
                entity.Gender = null;
            }

            UserManager.CreateUser(entity);

            model.ID = entity.ID;
        }

        public void Update(UserItemViewModel model)
        {
            var entity = UserManager.GetUser(model.ID);
            if (entity != null)
            {
                entity.UserName = model.UserName;
                entity.Email = model.Email;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.IsAdmin = model.IsAdmin;
                entity.BirthDate = model.BirthDate;
                entity.Gender = null;
                entity.GenderID = model.GenderID;
                
                if (!string.IsNullOrEmpty(model.Password))
                {
                    entity.Password = PasswordHash.EncryptPassword(model.Password, entity.Salt);
                }

                try
                {
                    UserManager.UpdateUser(entity);
                    model.Gender = entity.Gender.Name;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("There was problem updating the user.");
                }
            }

        }

        public void Destroy(UserItemViewModel model)
        {
            var entity = UserManager.GetUser(model.ID);

            if (entity != null)
            {
                UserManager.DeleteUser(entity);
            }
        }
    }
}