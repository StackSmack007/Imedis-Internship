using AutoMapper;

namespace Services
{
    public abstract class BaseService
    {
        protected readonly IMapper mapper;
        protected BaseService(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}
