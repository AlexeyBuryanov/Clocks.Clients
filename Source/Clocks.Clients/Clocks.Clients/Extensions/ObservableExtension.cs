using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Clocks.Clients.Core.Extensions
{
    /// <summary>
    /// Расширения для IEnumerable
    /// </summary>
    public static class ObservableExtension
    {
        /// <summary>
        /// Конвертировать коллекцию IEnumerable в ObservableCollection
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="source">Коллекция</param>
        /// <returns>ObservableCollection</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            var collection = new ObservableCollection<T>();

            foreach (var item in source)
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}