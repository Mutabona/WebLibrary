using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary.Contracts.Users
{
    /// <summary>
    /// Запрос на регистрацию.
    /// </summary>
    public class RegistratinRequest
    {
        /// <summary>
        /// Логин.
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Почта.
        /// </summary>
        public string Email { get; set; }
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
    }
}
