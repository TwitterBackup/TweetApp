using System;
using System.Threading.Tasks;

namespace TwitterBackup.Data.Repository
{
    public interface IUnitOfWork
    {
        int CompleteWork();
        Task<int> CompleteWorkAsync();
    }
}
