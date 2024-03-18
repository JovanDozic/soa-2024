using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers
{
    public class PersonProfile: Profile
    {
        public PersonProfile() {

            CreateMap<PersonDto, Person>().ReverseMap();
        }
    }
}
