using Domain.Models;

namespace Domain.Interfaces
{
    public interface IMailService
    {
        void SendEmail(Email email);
    }
}
