using Clocks.Clients.Core.Models.Database;
using Clocks.Clients.Droid.Database;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDbPath))]
namespace Clocks.Clients.Droid.Database
{
    public class AndroidDbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
