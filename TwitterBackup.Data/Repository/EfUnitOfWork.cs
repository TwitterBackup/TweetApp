using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TwitterBackup.Data.Repository
{
    public class EfUnitOfWork: IUnitOfWork
    {
        private readonly TwitterDbContext context;

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
