namespace MessageSenderAPI.Domain.ModelsDTO
{
    public class UserVerifyDTO
    {
        public string Email { get; set; }
        public int VerifyCode { get; set; }
    }
}
