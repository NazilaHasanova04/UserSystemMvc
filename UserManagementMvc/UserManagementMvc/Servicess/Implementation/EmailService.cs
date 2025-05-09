using System.Net;
using System.Net.Mail;
using UserManagementMvc.Servicess.Abstraction;

namespace UserManagementMvc.Servicess.Implementation
{
    public class EmailService : IMailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            using MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new MailAddress(_configuration["Mail:Username"], "Nazile", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Port = 587;
            smtp.Host = _configuration["Mail:Host"];
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.EnableSsl = true;

            try
            {
                await smtp.SendMailAsync(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
