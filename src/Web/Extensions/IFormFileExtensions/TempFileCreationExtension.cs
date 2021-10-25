using System.IO;
using Microsoft.AspNetCore.Http;

namespace Web.Extensions.IFormFileExtensions
{
    public static class TempFileCreationExtension
    {
		public static string CreateAndGetTempFilePath(this IFormFile file)
        {
			var extension = Path.GetExtension(file.FileName);
			var tempFilePath = Path.GetTempFileName();
            var filePath = Path.ChangeExtension(tempFilePath, extension);

            using (var stream = File.Create(filePath))
            file.CopyTo(stream);

            return filePath;
        }
	}
}
