using Clocks.Clients.iOS.Database;
using System;
using System.IO;
using Clocks.Clients.Core.Models.Database;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosDbPath))]
namespace Clocks.Clients.iOS.Database
{
    public class IosDbPath : IPath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", sqliteFilename);
        }
    }
}