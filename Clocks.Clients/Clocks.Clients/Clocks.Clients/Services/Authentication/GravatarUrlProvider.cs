using System;
using System.Text;

namespace Clocks.Clients.Core.Services.Authentication
{
    /// <summary>
    /// Поставщик аватарки Gravatar
    /// </summary>
    public class GravatarUrlProvider : IAvatarUrlProvider
    {
        /// <summary>
        /// Получить URL аватарки
        /// </summary>
        /// <param name="email">Почта</param>
        /// <returns>URL аватарки</returns>
        public string GetAvatarUrl(string email)
        {
            var hash = GetMd5(email);

            var uriBuilder = new UriBuilder("https://www.gravatar.com")
            {
                Path = $"avatar/{hash}",
                Query = "size=300"
            };

            return uriBuilder.ToString();
        }

        /// <summary>
        /// Получить MD5 hash из почты
        /// </summary>
        /// <param name="email">Почта</param>
        private static string GetMd5(string email)
        {
            using (var algorithm = System.Security.Cryptography.MD5.Create())
            {
                var result = algorithm.ComputeHash(Encoding.ASCII.GetBytes(email));
                var hash = BitConverter.ToString(result).Replace("-", "").ToLower();
                return hash;
            }
        }
    }
}
