namespace TicketOffice.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        bool IsLogin(string login, string password, out int id);

        bool IsRegistration(string login, string email);
    }
}
