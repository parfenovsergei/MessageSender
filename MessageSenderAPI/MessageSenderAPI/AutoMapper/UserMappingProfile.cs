using AutoMapper;
using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.ModelsDTO;

namespace MessageSenderAPI.AutoMapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<UserAuthDTO, User>();    
        }
    }
}
