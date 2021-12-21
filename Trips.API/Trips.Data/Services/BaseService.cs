using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trips.Data.Context;

namespace Trips.Data.Services
{
    public class BaseService
    {
        protected readonly TripDbContext _dbContext;
        protected readonly IMapper _mapper;

        public BaseService(TripDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
    }
}
