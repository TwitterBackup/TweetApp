using System.Threading.Tasks;

namespace TwitterBackup.Data.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly TwitterDbContext context;
        //private readonly IRepository<TEntity> repository;

        public EfUnitOfWork(TwitterDbContext context)
        {
            this.context = context;
        }

        public int CompleteWork()
        {
            return this.context.SaveChanges();
        }

        public Task<int> CompleteWorkAsync()
        {
            return this.context.SaveChangesAsync();
        }
    }
}
