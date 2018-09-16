using AutoMapper;
using Culinarium.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Services.Services
{
    public class MapperManager : IMapperManager
    {
        private readonly IMapper _mapper;

        public MapperManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
