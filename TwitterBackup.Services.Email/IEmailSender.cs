using System.Threading.Tasks;

namespace TwitterBackup.Services.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
