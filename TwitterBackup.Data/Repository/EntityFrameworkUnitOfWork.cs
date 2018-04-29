using System.Threading.Tasks;

namespace TwitterBackup.Data.Repository
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly TwitterDbContext context;
        //private readonly IRepository<TEntity> repository;

        public EntityFrameworkUnitOfWork(TwitterDbContext context)
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
