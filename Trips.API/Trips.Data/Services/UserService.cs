using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trips.Data.Context;
using Trips.Data.Entities;
using Trips.Data.Inferfaces;
using Trips.Data.Models;
using Trips.Data.Models.User;

namespace Trips.Data.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(TripDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            var users = await _dbContext.Users.ToListAsync();

            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task RegisterUserAsync(UserRegisterModel userModel)
        {
            var user = _mapper.Map<User>(userModel);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
