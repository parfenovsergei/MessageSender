namespace MessageSenderAPI.Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageTheme { get; set; }
        public string MessageBody { get; set; }
        public User Owner { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsSend { get; set; }
    }
}