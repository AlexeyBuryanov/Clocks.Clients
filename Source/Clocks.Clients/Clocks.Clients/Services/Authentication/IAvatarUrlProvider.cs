namespace Clocks.Clients.Core.Services.Authentication
{
    /// <summary>
    /// Описывает провайдера аватарки
    /// </summary>
    public interface IAvatarUrlProvider
    {
        string GetAvatarUrl(string email);
    }
}
