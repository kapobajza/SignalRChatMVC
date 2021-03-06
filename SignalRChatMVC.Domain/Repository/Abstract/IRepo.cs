﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChatMVC.Domain.Repository.Abstract
{
    public interface IRepo<T>
    {
        bool Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
    }
}
