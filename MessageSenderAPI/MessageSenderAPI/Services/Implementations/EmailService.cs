using Microsoft.EntityFrameworkCore;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Services.Interfaces;

namespace MessageSenderAPI.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public EmailService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task CheckToSendMessagesAsync()
        {
            var messagesToSend = await _context.Messages
                .Where(m => m.IsSend == false && m.SendDate <= DateTime.Now)
                .ToListAsync();
            if (messagesToSend != null)
            {
                foreach (var message in messagesToSend)
                {
                    await SendMessage(message);
                    message.IsSend = true;
                    Console.WriteLine($"Message with id:{message.Id} is send to {message.Owner.Email}");
                }
                await _context.SaveChangesAsync();
            }
            else
                Console.WriteLine("No messages to send :(");
        }

        private async Task SendMessageAsync(Message message)
        {
            var sendMessage = new MimeMessage();
            sendMessage.From.Add(new MailboxAddress("AdminMessageService", _config.GetSection("EmailUsername").Value));
            sendMessage.To.Add(new MailboxAddress("ClientMessageService", message.Owner.Email));
            sendMessage.Subject = message.MessageTheme;
            sendMessage.Body = new TextPart("plain")
            {
                Text = message.MessageBody
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(
                        _config.GetSection("EmailHost").Value,
                        465,
                        SecureSocketOptions.SslOnConnect);
                    await client.AuthenticateAsync(
                        _config.GetSection("EmailUsername").Value,
                        _config.GetSection("EmailPassword").Value);
                    await client.SendAsync(sendMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}