using System.Threading.Tasks;

namespace Snacks.Services.Email.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string nameTo, string emailTo, string subject, string content);
    }
}
