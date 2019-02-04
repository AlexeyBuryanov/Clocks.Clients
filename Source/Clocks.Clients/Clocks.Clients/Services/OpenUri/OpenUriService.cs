using System;
using Xamarin.Forms;

namespace Clocks.Clients.Core.Services.OpenUri
{
    /// <inheritdoc />
    /// <summary>
    /// Сервис открытия Uri
    /// </summary>
    public class OpenUriService : IOpenUriService
    {
        public void OpenUri(string uri) => Device.OpenUri(new Uri(uri));
    }
}
