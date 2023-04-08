using Microsoft.AspNetCore.Http;
using TicketOffice.BusinessLogic.Interfaces;

namespace TicketOffice.BusinessLogic.Services
{
    public class ImageService : IImageService
    {
        public byte[] ConvertAvatarToByteArray(HttpRequest files)
        {
            byte[] avatarByteArray = Array.Empty<byte>();

            foreach (var file in files.Form.Files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);

                    avatarByteArray = memoryStream.ToArray();
                }
            }

            return avatarByteArray;
        }
    }
}
