using Service.DTO;

namespace Service.Interface
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO emailDTO);
    }
}
