using AutoMapper;

namespace BusinessLayer.Services.Common
{
    public abstract class ServiceBase
    {
        protected readonly IMapper mapper;

        protected ServiceBase(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}
