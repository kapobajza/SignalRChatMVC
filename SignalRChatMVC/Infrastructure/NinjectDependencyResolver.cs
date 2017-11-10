using Ninject;
using SignalRChatMVC.Domain.Repository.Abstract;
using SignalRChatMVC.Domain.Repository.Concrete;
using SignalRChatMVC.Infrastructure.Abstract;
using SignalRChatMVC.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRChatMVC.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IAuthProvider>().To<MyAuthentication>();
            _kernel.Bind<IUserRepo>().To<UserRepo>();
            _kernel.Bind<IConversationReplyRepo>().To<ConversationReplyRepo>();
            _kernel.Bind<IConversationRepo>().To<ConversationRepo>();
            _kernel.Bind<IFriendRepo>().To<FriendRepo>();
        }
    }
}