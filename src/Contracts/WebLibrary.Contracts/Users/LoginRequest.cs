using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary.Contracts.Users
{
    /// <summary>
    /// Запрос авторизации.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
