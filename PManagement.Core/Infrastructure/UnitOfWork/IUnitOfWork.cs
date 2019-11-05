using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PManagement.Core.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
        bool CommitSync();
        void CleanUp();
    }
}
