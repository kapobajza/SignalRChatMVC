using SignalRChatMVC.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRChatMVC.Infrastructure
{
    public class MyAuthorization : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var loggedUser = MyAuthentication.UserSession;

            if(loggedUser != null)
                return;
            
            filterContext.HttpContext.Response.Redirect("/Account/Login");
        }
    }
}