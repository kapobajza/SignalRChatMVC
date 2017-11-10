using SignalRChatMVC.Domain.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Repository.Abstract
{
    public abstract class BaseRepoEntity<T> : IBaseRepo<T>
    {
        protected EFContext _ctx;
        protected bool propertyChanged = false;

        public BaseRepoEntity(EFContext ctx)
        {
            _ctx = ctx;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public virtual void Edit(T entity, T newValues)
        {
            var properties1 = newValues.GetType().GetProperties();
            var properties2 = entity.GetType().GetProperties();

            if (properties1.Length == properties2.Length)
            {
                for (int i = 0; i < properties1.Length; i++)
                {
                    var value1 = properties1[i].GetValue(entity, null);
                    var value2 = properties2[i].GetValue(newValues, null);

                    if (properties1[i].Name == properties2[i].Name && value1 != value2)
                    {
                        properties1[i].SetValue(entity, value2, null);
                        propertyChanged = true;
                    }
                }
            }
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _ctx.SaveChangesAsync();
        }
    }
}
