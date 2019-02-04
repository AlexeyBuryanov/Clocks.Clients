namespace Clocks.Clients.Core.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        public string Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Эл. почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gravatar url
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}
