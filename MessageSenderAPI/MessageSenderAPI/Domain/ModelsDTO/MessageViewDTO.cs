using MessageSenderAPI.Domain.Models;

namespace MessageSenderAPI.Domain.ModelsDTO
{
    public class MessageViewDTO
    {
        public int Id { get; set; }
        public string MessageTheme { get; set; }
        public string MessageBody { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsSend { get; set; }
    }
}
