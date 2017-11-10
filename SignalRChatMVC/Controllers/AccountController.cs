using SignalRChatMVC.Domain.Entities;
using SignalRChatMVC.Infrastructure;
using SignalRChatMVC.Infrastructure.Abstract;
using SignalRChatMVC.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRChatMVC.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider _auth;
        public AccountController(IAuthProvider auth)
        {
            _auth = auth;
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            using (_auth)
            {
                if (ModelState.IsValid)
                {
                    if (_auth.Login(model.UserName, model.Password))
                        return RedirectToAction("Index", "Chat");

                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            using (_auth)
            {
                _auth.Logout();
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User model)
        {
            using (_auth)
            {
                if (ModelState.IsValid)
                {
                    if (_auth.Register(model))
                        return RedirectToAction("Index", "Chat");

                    ModelState.AddModelError("", "That username already exists!");
                    return View();
                }

                return View(model);
            }
        }
    }
}