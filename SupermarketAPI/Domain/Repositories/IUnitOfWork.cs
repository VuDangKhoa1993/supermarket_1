using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteTask();
    }
}
