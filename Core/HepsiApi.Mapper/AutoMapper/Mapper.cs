using AutoMapper;
using AutoMapper.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Mapper.AutoMapper
{
    public class Mapper : Application.Interfaces.AutoMapper.IMapper
    {
        public static List<TypePair> typePairs = new();

        private IMapper MapperContainer;

        public TDestination Map<TDestination, Tsource>(Tsource source, string? ignore = null)
        {
            Config<TDestination, Tsource>(5, ignore);
            return MapperContainer.Map<Tsource, TDestination>(source);
        }

        public IList<TDestination> Map<TDestination, Tsource>(IList<Tsource> source, string? ignore = null)
        {
            Config<TDestination, Tsource>(5, ignore);
            return MapperContainer.Map<IList<Tsource>, IList<TDestination>>(source);
        }

        public TDestination Map<TDestination>(object source, string? ignore = null)
        {
            Config<TDestination, object>(5, ignore);
            
            return MapperContainer.Map<object, TDestination>(source);
        }

        public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
        {
            
            Config<TDestination, IList<object>>(5, ignore);
            return MapperContainer.Map<IList<TDestination>>(source);

        }

        protected void Config<TDestination,TSource>(int depth = 5 , string ? ignore = null)
        {
            var typePair = new TypePair(typeof(TSource), typeof(TDestination));

            if (typePairs.Any(x => x.DestinationType == typePair.DestinationType && x.SourceType == typePair.SourceType) && ignore is null)
            {
                return;
            }

            typePairs.Add(typePair);
            var config = new MapperConfiguration(cfg =>
            {
               foreach(var item in typePairs)
                {
                    if(ignore is not null)
                        cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ForMember(ignore , x => x.Ignore()).ReverseMap();
                    else 
                        cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ReverseMap();
                }
            });

            MapperContainer = config.CreateMapper();

        }
    }
}
