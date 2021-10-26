using System.IO;
using Microsoft.AspNetCore.Http;

namespace Web.Extensions.IFormFileExtensions
{
    public static class ConfigExtension
    {
		public static bool IsConfigurationFile(this IFormFile file) =>
            Path.GetExtension(file.FileName) == ".cfg";
	}
}
