using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Repository.Abstract
{
    public interface IRepo<T> : IDisposable
    {
        void Save();
        Task<int> SaveAsync();
        bool Add(T entity);
        void Edit(T entity, T newValues);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
    }
}
