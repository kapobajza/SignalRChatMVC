using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Repository.Abstract
{
    public interface IBaseRepo<T> : IDisposable
    {
        void SaveChanges();
        void Edit(T entity, T newValues);
        Task<int> SaveChangesAsync();
    }
}
