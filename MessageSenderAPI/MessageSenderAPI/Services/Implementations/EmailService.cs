using Microsoft.EntityFrameworkCore;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Diagnostics;

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
            await _context.Messages
                .Include(m => m.Owner)
                .Where(m => m.IsSend == false && m.SendDate <= DateTime.Now)
                .ForEachAsync(message =>
                {
                    SendMessageAsync(message);
                    message.IsSend = true;
                    Console.WriteLine($"Message with id:{message.Id} is send to {message.Owner.Email}");
                });
            await _context.SaveChangesAsync();
        }

        public async Task SendVerifyCodeAsync(string email, int code)
        {
            var sendMessage = new MimeMessage();
            sendMessage.From.Add(new MailboxAddress("AdminMessageService", _config.GetSection("EmailUsername").Value));
            sendMessage.To.Add(new MailboxAddress("ClientMessageService", email));
            sendMessage.Subject = "Verify code";
            sendMessage.Body = new TextPart("plain")
            {
                Text = $"Dear, {email}"
                  + Environment.NewLine
                  + $"To end registration you can enter the following code {code}"
                  + Environment.NewLine
                  + "If you got this email, but is not yours, then just ignore it."
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
               Console.WriteLine($"Verify code is not send to {email}, exception message: {ex.Message}");
            }

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
                Console.WriteLine($"Message with id:{message.Id} is not send, exception message: {ex.Message}");
            }
        }
    }
}