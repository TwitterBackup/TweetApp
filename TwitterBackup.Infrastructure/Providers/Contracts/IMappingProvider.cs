using System.Collections.Generic;
using System.Linq;

namespace TwitterBackup.Infrastructure.Providers.Contracts
{
    public interface IMappingProvider
    {
        TDestination MapTo<TDestination>(object source);

        IQueryable<TDestination> ProjectTo<TDestination>(IQueryable<object> source);

        IEnumerable<TDestination> ProjectTo<TDestination>(IEnumerable<object> source);
    }
}
