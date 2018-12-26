using System.Collections.Generic;

namespace Clocks.Clients.Core.Services.File
{
    /// <summary>
    /// Описывает сервис по работе с файлами
    /// </summary>
    public interface IFileService
    {
        List<string> GetEmbeddedResourceNames();

        string ReadStringFromAssemblyEmbeddedResource(string path);

        string ReadStringFromLocalAppDataFolder(string fileName);

        bool WriteStringToLocalAppDataFolder(string fileName, string textToWrite);

        bool ExistsInLocalAppDataFolder(string fileName);
    }
}
