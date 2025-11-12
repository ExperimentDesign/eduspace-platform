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
            var sendGridApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY")
                ?? _configuration["SENDGRID_API_KEY"]
                ?? throw new InvalidOperationException("SENDGRID_API_KEY not configured");

            var fromEmail = Environment.GetEnvironmentVariable("SENDGRID_FROM_EMAIL")
                ?? _configuration["SENDGRID_FROM_EMAIL"]
                ?? throw new InvalidOperationException("SENDGRID_FROM_EMAIL not configured");

            var fromName = Environment.GetEnvironmentVariable("SENDGRID_FROM_NAME")
                ?? _configuration["SENDGRID_FROM_NAME"]
                ?? "EduSpace Platform";

            var smtpHost = _configuration["SMTP_HOST"] ?? "smtp.sendgrid.net";
            var smtpPort = int.Parse(_configuration["SMTP_PORT"] ?? "587");

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromName, fromEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = $"<h3>Your code is: {body}</h3>" };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("apikey", sendGridApiKey);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation("Email sent to {To} via SendGrid", to);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email via SendGrid");
            throw;
        }
    }
}