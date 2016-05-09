namespace VegiJ.Web.Helpers
{
    using System;
    using System.Web.Security;
    using System.Collections.Specialized;
    using System.Collections.Generic;
    using System.Linq;
    using VegiJ.DataAccess;
    using Ninject;

    public class CustomRoleProvider : RoleProvider
    {
        private string applicationName;
        private readonly string[] _currentRoles = { "admin", "user" };

        [Inject]
        public IUserManager userManager { get; set; }

        public override string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (string.IsNullOrEmpty(name))
            {
                name = "CustomRoleProvider";
            }

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Custom Role Provider");
            }

            //Initialize the abstract base class.  
            base.Initialize(name, config);

            applicationName = GetConfigValue(config["applicationName"],
                System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            try
            {
                foreach (string username in usernames)
                {
                    var user = this.userManager.GetUsers().AsEnumerable()
                        .Where(
                            u =>
                                string.Equals((u.UserName as string), username,
                                    StringComparison.InvariantCultureIgnoreCase) == true)
                        .FirstOrDefault();

                    if (user != null)
                    {
                        if (roleNames.Length > 0)
                        {
                            foreach (var role in roleNames)
                            {
                                if (_currentRoles.Contains(role))
                                {
                                    switch (role.ToLower())
                                    {
                                        case "admin":
                                            user.IsAdmin = true;
                                            break;
                                        case "user":
                                            user.IsAdmin = false;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public override void CreateRole(string roleName)
        {
            try
            {
                //using (DataContext _db = new DataContext())
                //{
                //    Role role = new Role();
                //    role.RoleName = roleName;

                //    _db.Roles.InsertOnSubmit(role);

                //    _db.SubmitChanges();
                //}
            }
            catch
            {
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            bool ret = false;

            try
            {
                //Role role = (from r in _db.Roles
                //             where r.RoleName == roleName
                //             select r).SingleOrDefault();

                //if (roleName != null && roleName.ToLower() == "admin")
                //{
                //    _db.Roles.DeleteOnSubmit(role);

                //    _db.SubmitChanges();

                //    ret = true;
                //}
            }
            catch
            {
                //ret = false;
            }
            //}

            return true;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            var users = new List<string>();
            try
            {
                var userIsInRole = this.IsUserInRole(usernameToMatch, roleName);
                if (_currentRoles.Contains(roleName) || userIsInRole)
                {
                    switch (roleName.ToLower())
                    {
                        case "admin":
                            users = this.userManager.GetUsers().AsEnumerable()
                                .Where(u => u.IsAdmin == true).Select(u => u.UserName).ToList();
                            break;
                        case "user":
                            users = this.userManager.GetUsers().AsEnumerable().Select(u => u.UserName).ToList();
                            break;
                    }
                }
            }
            catch
            {
            }

            return users.ToArray();
        }

        public override string[] GetAllRoles()
        {
            List<string> roles = new List<string>();

            try
            {
                foreach (var role in _currentRoles)
                {
                    roles.Add(role);
                }
            }
            catch
            {
            }

            return roles.ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();

            try
            {
                var user = this.userManager.GetUsers().AsEnumerable()
                           .Where(u => string.Equals((u.UserName as string), username,
                                    StringComparison.InvariantCultureIgnoreCase) == true)
                        .FirstOrDefault();

                if (user != null)
                {
                    roles.Add("user");
                    if (user.IsAdmin)
                    {
                        roles.Add("admin");
                    }
                }
            }
            catch
            {
            }

            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            List<string> users = new List<string>();
            roleName = roleName.ToLower().Trim();

            try
            {
                if (_currentRoles.Contains(roleName))
                {
                    switch (roleName)
                    {
                        case "admin":
                            users = this.userManager.GetUsers().AsEnumerable()
                                .Where(u => u.IsAdmin == true).Select(u => u.UserName).ToList();
                            break;
                        case "user":
                            users = this.userManager.GetUsers().AsEnumerable().Select(u => u.UserName).ToList();
                            break;
                    }
                }
            }
            catch
            {
            }

            return users.ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool isValid = false;
            roleName = roleName.ToLower().Trim();

            try
            {
                var user = this.userManager.GetUsers().AsEnumerable()
                           .Where(u => string.Equals((u.UserName as string), username,
                                    StringComparison.InvariantCultureIgnoreCase) == true)
                        .FirstOrDefault();

                if (_currentRoles.Contains(roleName) || user != null)
                {
                    switch (roleName)
                    {
                        case "admin":
                            isValid = user.IsAdmin;
                            break;
                        case "user":
                            isValid = true;
                            break;
                    }
                }
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            try
            {
                foreach (string username in usernames)
                {
                    var user = this.userManager.GetUsers().AsEnumerable()
                           .Where(u => string.Equals((u.UserName as string), username,
                                    StringComparison.InvariantCultureIgnoreCase) == true)
                        .FirstOrDefault();

                    if (user != null)
                    {
                        foreach (var role in _currentRoles)
                        {
                            foreach (string roleName in roleNames)
                            {
                                if (role == roleName.ToLower().Trim() && roleName.ToLower().Trim().Equals("admin"))
                                {
                                    user.IsAdmin = false;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public override bool RoleExists(string roleName)
        {
            bool isValid = false;
            roleName = roleName.ToLower().Trim();

            if (_currentRoles.Any(r => string.Equals(r, roleName, StringComparison.InvariantCultureIgnoreCase)))
            {
                isValid = true;
            }

            return isValid;
        }

        private static string GetConfigValue(string configValue, string defaultValue)
        {
            if (string.IsNullOrEmpty(configValue))
            {
                return defaultValue;
            }

            return configValue;
        }
    }
}