using AutoMapper;
using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.ModelsDTO;

namespace MessageSenderAPI.AutoMapper
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<Message, MessageViewDTO>();
            CreateMap<MessageDTO, Message>();
        }
    }
}
