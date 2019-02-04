namespace Clocks.Clients.Core.Models.Database
{
    public interface IPath
    {
        /// <summary>
        /// Через метод GetDatabasePath мы будем получать из разных ОС путь к базе данных
        /// </summary>
        string GetDatabasePath(string filename);
    }
}