using Microsoft.AspNetCore.Http;

namespace TicketOffice.BusinessLogic.Interfaces
{
    public interface IImageService
    {
        byte[] ConvertAvatarToByteArray(HttpRequest files);
    }
}
