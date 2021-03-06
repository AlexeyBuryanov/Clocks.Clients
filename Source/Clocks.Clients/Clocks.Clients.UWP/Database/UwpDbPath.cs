using Clocks.Clients.Core.Models.Database;
using Clocks.Clients.UWP.Database;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(UwpDbPath))]
namespace Clocks.Clients.UWP.Database
{
    public class UwpDbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            var path = ApplicationData.Current.LocalFolder.Path;
            return Path.Combine(path, filename);
        }
    }
}
