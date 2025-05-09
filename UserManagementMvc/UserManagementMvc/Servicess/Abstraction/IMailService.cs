namespace UserManagementMvc.Servicess.Abstraction
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
    }
}
