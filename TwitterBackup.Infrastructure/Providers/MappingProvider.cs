﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Infrastructure.Providers.Contracts;

namespace TwitterBackup.Infrastructure.Providers
{
    public class MappingProvider : IMappingProvider
    {
        private readonly IMapper mapper;

        public MappingProvider(IMapper mapper)
        {
            this.mapper = mapper;
        }


        public TDestination MapTo<TDestination>(object source)
        {
            return this.mapper.Map<TDestination>(source);
        }

        public IQueryable<TDestination> ProjectTo<TSource, TDestination>(IQueryable<TSource> source)
        {
            return source.ProjectTo<TDestination>();
        }

        public IEnumerable<TDestination> ProjectTo<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return source.AsQueryable().ProjectTo<TDestination>();
        }
    }
}
