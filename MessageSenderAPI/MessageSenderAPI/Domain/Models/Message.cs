namespace MessageSenderAPI.Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageTheme { get; set; }
        public string MessageBody { get; set; }
        public virtual User User { get; set; }
        public DateTime SendDate { get; set; }
    }
}