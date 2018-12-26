using System;
using System.IO;

namespace Clocks.Clients.Core.Extensions
{
    /// <summary>
    /// Расширения для UriBuilder
    /// </summary>
    public static class UriBuilderExtension
    {
        /// <summary>
        /// Добавить к пути
        /// </summary>
        /// <param name="builder">UriBuilder</param>
        /// <param name="pathToAdd">Путь к добавлению</param>
        public static void AppendToPath(this UriBuilder builder, string pathToAdd)
        {
            var completePath = Path.Combine(builder.Path, pathToAdd);
            builder.Path = completePath;
        }
    }
}
