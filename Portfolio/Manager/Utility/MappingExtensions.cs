using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Utility
{
    public static class MappingExtensions
    {
        private static IMapper _mapper;

        // Configure once at application start
        // DI cannot inject dependencies into a static constructor.
        // so using "Configure" method  allows you to pass the DI-created IMapper instance manually to the static class.
        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }


        // Generic mapping extension
        public static TDestination ToMap<TDestination>(this Object source)
        {
            if(_mapper == null)
            {
                throw new Exception("Mapper is not configured. Call MappingExtensions.Configure(IMapper) at startup.");
            }
            return _mapper.Map<TDestination>(source);
        }



    }
}
