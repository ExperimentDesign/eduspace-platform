using FULLSTACKFURY.EduSpace.API.IAM.Application.Internal.OutboundServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace FULLSTACKFURY.EduSpace.API.IAM.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var smtpHost = _configuration["SMTP_HOST"] ?? throw new InvalidOperationException("SMTP_HOST not configured");
            var smtpPort = int.Parse(_configuration["SMTP_PORT"] ?? "587");
            var smtpUser = _configuration["SMTP_USER"] ?? throw new InvalidOperationException("SMTP_USER not configured");
            var smtpPass = _configuration["SMTP_PASSWORD"] ?? throw new InvalidOperationException("SMTP_PASSWORD not configured");

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(smtpUser));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = $"<h3>Your code is: {body}</h3>" };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(smtpUser, smtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            
            _logger.LogInformation("Real email sent to {To}", to);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email.");
            throw;
        }
    }
}