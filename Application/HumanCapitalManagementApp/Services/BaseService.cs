using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

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
