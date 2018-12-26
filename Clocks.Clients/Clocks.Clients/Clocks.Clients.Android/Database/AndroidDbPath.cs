using Clocks.Clients.Droid.Database;
using System;
using System.IO;
using Clocks.Clients.Core.Models.Database;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDbPath))]
namespace Clocks.Clients.Droid.Database
{
    public class AndroidDbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
        }
    }
}