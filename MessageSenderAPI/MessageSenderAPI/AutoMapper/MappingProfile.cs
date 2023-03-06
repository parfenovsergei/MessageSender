using AutoMapper;
using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.ModelsDTO;

namespace MessageSenderAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserRegisterDTO, User>();
            CreateMap<UserLoginDTO, User>();
            CreateMap<Message, MessageViewDTO>();
            CreateMap<MessageDTO, Message>();
        }
    }
}
