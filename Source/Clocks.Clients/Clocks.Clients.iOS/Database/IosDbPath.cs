using Clocks.Clients.Core.Models.Database;
using Clocks.Clients.iOS.Database;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosDbPath))]
namespace Clocks.Clients.iOS.Database
{
    public class IosDbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = Path.Combine(docFolder, "..", "Library");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}
