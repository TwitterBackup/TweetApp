﻿using System.Collections.Generic;

namespace TwitterBackup.Infrastructure.Providers.Contracts
{
    public interface IMappingProvider
    {
        TDestination MapTo<TDestination>(object source);

        IQueryable<TDestination> ProjectTo<TSource, TDestination>(IQueryable<TSource> source);

        IEnumerable<TDestination> ProjectTo<TSource, TDestination>(IEnumerable<TSource> source);
    }
}
