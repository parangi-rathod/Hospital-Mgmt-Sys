using Service.DTO;

namespace Service.Interface
{
    public interface IEmailService
    {
        bool SendEmail(EmailDTO emailDTO);
    }
}
