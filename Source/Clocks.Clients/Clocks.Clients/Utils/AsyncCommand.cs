using System;
using System.Threading.Tasks;

namespace Xamarin.Forms
{
    /// <inheritdoc />
    /// <summary>
    /// Ассинхронная команда
    /// </summary>
    public class AsyncCommand : Command
    {
        public AsyncCommand(Func<Task> execute) : base(() => execute())
        {
        }

        public AsyncCommand(Func<object, Task> execute) : base(() => execute(null))
        {
        }
    }
}
