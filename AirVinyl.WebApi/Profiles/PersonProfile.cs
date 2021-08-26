using AirVinyl.Entities;
using AirVinyl.WebApi.Models;
using AutoMapper;

namespace AirVinyl.WebApi.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonForPostModel, Person>();
        }
    }
}
