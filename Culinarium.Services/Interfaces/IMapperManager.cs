using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Services.Interfaces
{
    public interface IMapperManager
    {
        TDestination Map<TDestination>(object source);
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
