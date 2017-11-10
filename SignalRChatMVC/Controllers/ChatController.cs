using SignalRChatMVC.Domain.DTO;
using SignalRChatMVC.Domain.Entities;
using SignalRChatMVC.Domain.Repository.Abstract;
using SignalRChatMVC.Infrastructure;
using SignalRChatMVC.Infrastructure.Concrete;
using SignalRChatMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRChatMVC.Controllers
{
    [MyAuthorization]
    public class ChatController : Controller
    {
        private IUserRepo _userRepo;
        private IConversationReplyRepo _conversationReplyRepo;
        private IFriendRepo _friendRepo;

        public ChatController(IUserRepo userRepo, IConversationReplyRepo conversationReplyRepo, IFriendRepo friendRepo)
        {
            _userRepo = userRepo;
            _conversationReplyRepo = conversationReplyRepo;
            _friendRepo = friendRepo;
        }

        public ActionResult Index()
        {
            User myUser;
            var model = new VMChatIndex();

            if (MyAuthentication.UserSession != null)
            {
                myUser = MyAuthentication.UserSession;

                model = new VMChatIndex()
                {
                    ChatName = "Global",
                    UserName = myUser.UserName
                };
            }

            return View(model);
        }
        
        public ActionResult LoadMessages()
        {
            using (_conversationReplyRepo)
            {
                return Json(_conversationReplyRepo.GetAllMessages("Global"));
            }
        }
    }
}