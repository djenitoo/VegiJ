﻿namespace VegiJ.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.ModelBinding;
    using DataAccess;
    using Logic;
    using Ninject;

    public partial class SiteMaster : Ninject.Web.MasterPageBase
    {
        [Inject]
        public IUserManager UserManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<User> GetUser()
        {
            IEnumerable<User> user = null;
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string username = HttpContext.Current.User.Identity.Name;
                user = UserManager.GetUsers().AsEnumerable().Where(
                    u =>
                        string.Equals((u.UserName as string), username, StringComparison.InvariantCultureIgnoreCase));
            }

            return user;
        }
    }
}