using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trips.Data.Models;
using Trips.Data.Models.User;

namespace Trips.Data.Inferfaces
{
    public interface IUserService
    {
        Task RegisterUserAsync(UserRegisterModel userModel);

        Task<List<UserViewModel>> GetUsersAsync();
    }
}
