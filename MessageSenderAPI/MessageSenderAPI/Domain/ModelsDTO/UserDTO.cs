using MessageSenderAPI.Domain.Enums;

namespace MessageSenderAPI.Domain.ModelsDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}