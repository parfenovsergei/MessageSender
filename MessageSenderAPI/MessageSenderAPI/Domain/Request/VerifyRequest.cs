namespace MessageSenderAPI.Domain.Request
{
    public class VerifyRequest
    {
        public string Email { get; set; }
        public int VerifyCode { get; set; }
    }
}
