using Microsoft.AspNetCore.Http;

namespace Application.Common.Extensions
{
    public static class FormFileExtensions
    {
        public static byte[] ToBytes(this IFormFile formFile)
        {
            using (var fs1 = formFile.OpenReadStream())
            using (var ms1 = new MemoryStream())
            {
                byte[] p1 = null;
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();
                return p1;
            }
        }
    }
}
