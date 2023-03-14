using MessageSenderAPI.Domain.Enums;

namespace MessageSenderAPI.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public Role Role { get; set; }
        public bool IsVerifed { get; set; }
        public int VerifyCode { get; set; }
        public bool IsChangingPassword { get; set; }
        public DateTime CreateAndVerifyTime { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}