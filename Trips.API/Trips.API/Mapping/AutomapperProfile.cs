using AutoMapper;
using Trips.Data.Entities;
using Trips.Data.Models;
using Trips.Data.Models.Trip;
using Trips.Data.Models.User;
using Trips.ML.API.Models;

namespace Trips.API.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Trip, TripModel>().ReverseMap();
            CreateMap<Trip, TripWithRatingModel>();

            CreateMap<Rating, RatingModel>().ReverseMap();
            CreateMap<Rating, TripRating>()
                .ForMember(r => r.Label, opt => opt.MapFrom(r => r.Value));
            

            CreateMap<User, UserRegisterModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
