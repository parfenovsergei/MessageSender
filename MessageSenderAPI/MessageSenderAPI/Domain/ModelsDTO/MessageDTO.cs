using System.ComponentModel.DataAnnotations;

namespace MessageSenderAPI.Domain.ModelsDTO
{
    public class MessageDTO
    {
        [StringLength(100, ErrorMessage = "Max length of message theme is 100")]
        public string MessageTheme { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000, ErrorMessage = "Max length of message is 1000")]
        public string MessageBody { get; set; }
        public DateTime SendDate { get; set; }
    }
}