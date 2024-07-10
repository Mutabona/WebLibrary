using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary.Contracts.Users
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Почта.
        /// </summary>
        public string Email { get; set; }
    }
}
